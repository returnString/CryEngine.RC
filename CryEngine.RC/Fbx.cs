using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using ManagedFbx;

namespace CryEngine.RC
{
	/// <summary>
	/// Provides utilities for the conversion of FBX meshes into interim formats processed by the CryENGINE resource compiler;
	/// </summary>
	public static class FbxConverter
	{
		/// <summary>
		/// Saves a specified FBX mesh to a CryENGINE-style Collada file.
		/// </summary>
		/// <param name="mesh">The mesh to save.</param>
		/// <param name="outputFile">The name of the .dae file to which the mesh will be saved.</param>
		/// <param name="name">The name of the mesh to be used internally for the interim Collada file.</param>
		public static void ToCollada(Mesh mesh, FileInfo outputFile)
		{
			if(outputFile == null)
				throw new ArgumentNullException("outputFile");

			if(mesh == null)
				throw new ArgumentNullException("mesh");

			var name = outputFile.Name.Replace(outputFile.Extension, "");

			// We use ToString on doubles, which uses the current culture; the EU etc uses commas, not periods
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			// Should ideally be done in the 3D package but we can't depend on that, so do some rough triangulation
			if(!mesh.HasOnlyTriangles)
			{
				Logging.Write("Quads/ngons found in mesh, performing automatic triangulation.");
				mesh = mesh.Triangulate();
			}

			var polygons = mesh.Polygons.ToList();

			#region Root Setup
			var doc = new XDocument();
			var root = new XElement("collada");
			doc.Add(root);
			#endregion

			#region Materials

			var matIds = mesh.MaterialIDs;
			var materials = (from mat in matIds.Distinct()
							 select new { Id = mat, Name = string.Format("{0}__{1}__{2}__{3}", name, mat + 1, "sub" + (mat + 1), "physDefault") }).ToList();

			// If a scene is exported without ids/materials, it will by default have no material info whatsoever
			// For simplicity's sake, we just add a dummy material in those cases to simplify processing later
			if(materials.Count == 0)
				materials.Add(new { Id = 0, Name = string.Format("{0}__{1}__{2}__{3}", name, 1, "sub", "physDefault") });

			var materialLib = new XElement("library_materials");

			foreach(var material in materials)
				materialLib.Add(new XElement("material", new XAttribute("id", material.Name), new XAttribute("name", material.Name), new XElement("instance_effect")));

			root.Add(materialLib);
			#endregion

			#region Geometry Base
			var geomRoot = new XElement("library_geometries");
			root.Add(geomRoot);

			var geom = new XElement("geometry", new XAttribute("id", name));
			geomRoot.Add(geom);

			var meshElement = new XElement("mesh");
			geom.Add(meshElement);

			#endregion

			#region Vertices
			var verts = mesh.Vertices;

			var vertString = new StringBuilder();
			foreach(var vertex in verts)
				vertString.AppendFormat("{0} {1} {2} ", Math.Round(vertex.X, 6), Math.Round(vertex.Y, 6), Math.Round(vertex.Z, 6));

			meshElement.Add
			(
				new XElement("source", new XAttribute("id", name + "-positions"),
					// Each array uses each vertex component separately and defines the stride later
					new XElement("float_array", new XAttribute("count", verts.Length * 3), new XAttribute("id", name + "-positions-array"), vertString.ToString()),
					new XElement("technique_common",
						new XElement("accessor", new XAttribute("count", verts.Length), new XAttribute("source", "#" + name + "-positions-array"), new XAttribute("stride", 3),
							new XElement("param", new XAttribute("name", "X"), new XAttribute("type", "float")),
							new XElement("param", new XAttribute("name", "Y"), new XAttribute("type", "float")),
							new XElement("param", new XAttribute("name", "Z"), new XAttribute("type", "float")))))
			);
			#endregion

			#region Normals
			var normals = new int[polygons.Count, 3];

			var normalString = new StringBuilder();
			for(var i = 0; i < polygons.Count; i++)
			{
				for(var j = 0; j < 3; j++)
				{
					normals[i, j] = i * 3 + j;
					var normal = mesh.GetVertexNormal(i, j);
					normalString.AppendFormat("{0} {1} {2} ", Math.Round(normal.X, 6), Math.Round(normal.Y, 6), Math.Round(normal.Z, 6));
				}
			}

			meshElement.Add
			(
				new XElement("source", new XAttribute("id", name + "-normals"),
					new XElement("float_array", new XAttribute("count", normals.Length * 3), new XAttribute("id", name + "-normals-array"), normalString.ToString()),
					new XElement("technique_common",
						new XElement("accessor", new XAttribute("count", normals.Length), new XAttribute("source", "#" + name + "-normals-array"), new XAttribute("stride", 3),
							new XElement("param", new XAttribute("name", "X"), new XAttribute("type", "float")),
							new XElement("param", new XAttribute("name", "Y"), new XAttribute("type", "float")),
							new XElement("param", new XAttribute("name", "Z"), new XAttribute("type", "float")))))
			);
			#endregion

			#region Coords
			var coords = mesh.TextureCoords;

			var coordString = new StringBuilder();
			// Round these or suffer painful consequences
			foreach(var coord in coords)
				coordString.AppendFormat("{0} {1} ", Math.Round(coord.X, 6), Math.Round(coord.Y, 6));

			meshElement.Add
			(
				new XElement("source", new XAttribute("id", name + "-coords"),
					new XElement("float_array", new XAttribute("count", coords.Length * 2), new XAttribute("id", name + "-coords-array"), coordString.ToString()),
					new XElement("technique_common",
						new XElement("accessor", new XAttribute("count", coords.Length), new XAttribute("source", "#" + name + "-coords-array"), new XAttribute("stride", 2),
							new XElement("param", new XAttribute("name", "S"), new XAttribute("type", "float")),
							new XElement("param", new XAttribute("name", "T"), new XAttribute("type", "float")))))
			);
			#endregion

			#region Vertex Colours
			var colours = mesh.VertexColours;
			var usesColours = colours.Length != 0;
			
			if(usesColours)
			{
				var colourString = new StringBuilder();

				for(var i = 0; i < verts.Length; i++)
				{
					var colour = colours[i];
					colourString.Append(colours.Length == 0 ? "0 0 0 0 " : string.Format("{0} {1} {2} {3} ",
						Math.Round(colour.R, 6), Math.Round(colour.G, 6), Math.Round(colour.B, 6), Math.Round(colour.A, 6)));
				}

				meshElement.Add
				(
					new XElement("source", new XAttribute("id", name + "-colours"),
						new XElement("float_array", new XAttribute("count", colours.Length * 4), new XAttribute("id", name + "-colours-array"), colourString.ToString()),
						new XElement("technique_common",
							new XElement("accessor", new XAttribute("count", colours.Length), new XAttribute("source", "#" + name + "-colours-array"), new XAttribute("stride", 4),
								new XElement("param", new XAttribute("name", "R"), new XAttribute("type", "float")),
								new XElement("param", new XAttribute("name", "G"), new XAttribute("type", "float")),
								new XElement("param", new XAttribute("name", "B"), new XAttribute("type", "float")),
								new XElement("param", new XAttribute("name", "A"), new XAttribute("type", "float")))))
				);
			}
			#endregion

			#region Polylist Setup
			meshElement.Add
			(
				new XElement("vertices", new XAttribute("id", name + "-vertices"),
					new XElement("input", new XAttribute("semantic", "POSITION"), new XAttribute("source", "#" + name + "-positions")))
			);

			var usesSubmats = matIds.Length > 1;

			foreach(var material in materials)
			{
				var vcount = new StringBuilder();
				var polies = new List<int>();

				if(usesSubmats)
				{
					for(var i = 0; i < polygons.Count; i++)
					{
						if(matIds[i] == material.Id)
							polies.Add(i);
					}
				}

				// Here, we create the polylist's reference array
				// It uses a stride of four, the 1st referencing a vertex, the 2nd a normal, the 3rd a texture coordinate and the 4th a vertex colour
				var pstring = new StringBuilder();
				var count = usesSubmats ? polies.Count : polygons.Count;

				for(var i = 0; i < count; i++)
				{
					// The count will always be three as we enforce triangulation
					vcount.Append("3 ");

					var index = usesSubmats ? polies[i] : i;

					for(var j = 0; j < 3; j++)
					{
						// Vertex
						pstring.Append(" " + polygons[index].Indices[j]);

						// Normal
						pstring.Append(" " + normals[index, j]);

						// Tex coord
						pstring.Append(" " + mesh.GetUVIndex(index, j));

						// Vertex colour
						pstring.Append(" " + (usesColours ? polygons[index].Indices[j] : 0));
					}
				}

				meshElement.Add
				(
					new XElement("polylist", new XAttribute("count", count), new XAttribute("material", "#" + material.Name),
						new XElement("input", new XAttribute("offset", 0), new XAttribute("semantic", "VERTEX"), new XAttribute("source", "#" + name + "-vertices")),
						new XElement("input", new XAttribute("offset", 1), new XAttribute("semantic", "NORMAL"), new XAttribute("source", "#" + name + "-normals")),
						new XElement("input", new XAttribute("offset", 2), new XAttribute("semantic", "TEXCOORD"), new XAttribute("source", "#" + name + "-coords")),
						new XElement("input", new XAttribute("offset", 3), new XAttribute("semantic", "COLOR"), new XAttribute("source", "#" + name + "-colours")),
						new XElement("vcount", vcount),
						new XElement("p", pstring))
				);
			}
			#endregion

			#region Scene Library
			var sceneMats = new List<XElement>(matIds.Length);
			foreach(var material in materials)
				sceneMats.Add(new XElement("instance_material", new XAttribute("symbol", material.Name), new XAttribute("target", "#" + material.Name)));

			var sceneLib =
			new XElement("library_visual_scenes",
				new XElement("visual_scene", new XAttribute("id", "scene"), new XAttribute("name", "scene"),
					new XElement("node", new XAttribute("id", "CryExportNode_" + name),
						new XElement("node", new XAttribute("id", name),
							new XElement("translate", new XAttribute("sid", "translation"), "0 0 0"),
							new XElement("rotate", new XAttribute("sid", "rotation_Z"), "0 0 1 0.0"),
							new XElement("rotate", new XAttribute("sid", "rotation_Y"), "0 1 0 0.0"),
							new XElement("rotate", new XAttribute("sid", "rotation_X"), "1 0 0 0.0"),
							new XElement("scale", new XAttribute("sid", "scale"), "1.0 1.0 1.0"),
							new XElement("instance_geometry", new XAttribute("url", "#" + name),
								new XElement("bind_material",
									new XElement("technique_common", sceneMats))),
							new XElement("extra",
								new XElement("technique", new XAttribute("profile", "CryEngine"),
									new XElement("properties")))),
						new XElement("extra",
							new XElement("technique", new XAttribute("profile", "CryEngine"),
									new XElement("properties", "fileType=cgf DoNotMerge UseCustomNormals"))))));

			root.Add(sceneLib);

			var scene =
			new XElement("scene",
				new XElement("instance_visual_scene", new XAttribute("url", "#scene")));

			root.Add(scene);
			#endregion

			var parentDir = outputFile.Directory;
			if(!parentDir.Exists)
				parentDir.Create();

			using(var stream = new StreamWriter(outputFile.OpenWrite()))
				stream.Write(doc.ToString());
		}
	}
}
