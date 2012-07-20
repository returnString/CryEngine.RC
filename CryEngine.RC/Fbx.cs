using System;
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
		/// <param name="name">The name of the mesh to be used internally for the interim Collada file. This defaults to the file name.</param>
		public static void ToCollada(Mesh mesh, string outputFile, string name = null)
		{
			if(name == null)
				name = outputFile.Split('/', '\\').Last().Split('.').First();

			// We use ToString on doubles, which uses the current culture; the EU etc uses commas, not periods
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			// Should ideally be done in the 3D package but we can't depend on that, so do some rough triangulation
			if(!mesh.HasOnlyTriangles)
			{
				Log.Write("Quads/ngons found in mesh, performing automatic triangulation.");
				mesh = mesh.Triangulate();
			}

			var polygons = mesh.Polygons;

			// TODO: Work out how physics is done
			var materialname = name + "__1__" + "physDefault";

			#region Root Setup
			var doc = new XDocument();
			var root = new XElement("collada");
			doc.Add(root);
			#endregion

			#region Materials
			var effects =
			new XElement("library_effects",
				new XElement("effect", new XAttribute("id", name + "-1-effect"),
					new XElement("profile_COMMON",
						new XElement("technique", new XAttribute("sid", "default"),
							new XElement("phong")))));

			var material =
			new XElement("library_materials",
				new XElement("material", new XAttribute("id", materialname), new XAttribute("name", materialname),
					new XElement("instance_effect", new XAttribute("url", "#" + name + "-1-effect"))));
			root.Add(material);
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
				vertString.AppendFormat("{0} {1} {2} ", vertex.X, vertex.Y, vertex.Z);

			var posSource =
			new XElement("source", new XAttribute("id", name + "-positions"),
				// Each array uses each vertex component separately and defines the stride later
				new XElement("float_array", new XAttribute("count", verts.Length * 3), new XAttribute("id", name + "-positions-array"), vertString.ToString()),
				new XElement("technique_common",
					new XElement("accessor", new XAttribute("count", verts.Length), new XAttribute("source", "#" + name + "-positions-array"), new XAttribute("stride", 3),
						new XElement("param", new XAttribute("name", "X"), new XAttribute("type", "float")),
						new XElement("param", new XAttribute("name", "Y"), new XAttribute("type", "float")),
						new XElement("param", new XAttribute("name", "Z"), new XAttribute("type", "float")))));
			#endregion

			#region Normals
			var normals = new int[polygons.Length, 3];

			var normalString = new StringBuilder();
			for(var i = 0; i < polygons.Length; i++)
			{
				for(var j = 0; j < 3; j++)
				{
					normals[i, j] = i * 3 + j;
					var normal = mesh.GetVertexNormal(i, j);
					normalString.AppendFormat("{0} {1} {2} ", Math.Round(normal.X, 6), Math.Round(normal.Y, 6), Math.Round(normal.Z, 6));
				}
			}

			var normalSource =
			new XElement("source", new XAttribute("id", name + "-normals"),
				new XElement("float_array", new XAttribute("count", normals.Length * 3), new XAttribute("id", name + "-normals-array"), normalString.ToString()),
				new XElement("technique_common",
					new XElement("accessor", new XAttribute("count", normals.Length), new XAttribute("source", "#" + name + "-normals-array"), new XAttribute("stride", 3),
						new XElement("param", new XAttribute("name", "X"), new XAttribute("type", "float")),
						new XElement("param", new XAttribute("name", "Y"), new XAttribute("type", "float")),
						new XElement("param", new XAttribute("name", "Z"), new XAttribute("type", "float")))));
			#endregion

			#region Coords
			var coords = mesh.TextureCoords;

			var coordString = new StringBuilder();
			// Round these or suffer painful consequences
			foreach(var coord in coords)
				coordString.AppendFormat("{0} {1} ", Math.Round(coord.X, 6), Math.Round(coord.Y, 6));

			var texSource =
			new XElement("source", new XAttribute("id", name + "-coords"),
				new XElement("float_array", new XAttribute("count", coords.Length * 2), new XAttribute("id", name + "-coords-array"), coordString.ToString()),
				new XElement("technique_common",
					new XElement("accessor", new XAttribute("count", coords.Length), new XAttribute("source", "#" + name + "-coords-array"), new XAttribute("stride", 2),
						new XElement("param", new XAttribute("name", "S"), new XAttribute("type", "float")),
						new XElement("param", new XAttribute("name", "T"), new XAttribute("type", "float")))));
			#endregion

			#region Vertex Colours
			var colours = mesh.VertexColours;
			var colourString = new StringBuilder();
			var usesColours = colours.Length != 0;

			if(usesColours)
			{
				for(var i = 0; i < verts.Length; i++)
				{
					var colour = colours[i];
					colourString.Append(colours.Length == 0 ? "0 0 0 0 " : string.Format("{0} {1} {2} {3} ",
						Math.Round(colour.R, 6), Math.Round(colour.G, 6), Math.Round(colour.B, 6), Math.Round(colour.A, 6)));
				}

				var colourSource =
				new XElement("source", new XAttribute("id", name + "-colours"),
					new XElement("float_array", new XAttribute("count", colours.Length * 4), new XAttribute("id", name + "-colours-array"), colourString.ToString()),
					new XElement("technique_common",
						new XElement("accessor", new XAttribute("count", colours.Length), new XAttribute("source", "#" + name + "-colours-array"), new XAttribute("stride", 4),
							new XElement("param", new XAttribute("name", "R"), new XAttribute("type", "float")),
							new XElement("param", new XAttribute("name", "G"), new XAttribute("type", "float")),
							new XElement("param", new XAttribute("name", "B"), new XAttribute("type", "float")),
							new XElement("param", new XAttribute("name", "A"), new XAttribute("type", "float")))));

				meshElement.Add(colourSource);
			}
			#endregion

			#region Polylist Setup
			var vertexSource =
			new XElement("vertices", new XAttribute("id", name + "-vertices"),
				new XElement("input", new XAttribute("semantic", "POSITION"), new XAttribute("source", "#" + name + "-positions")));

			// The count will always be three as we enforce triangulation
			var vcount = new StringBuilder();
			foreach(var poly in polygons)
				vcount.Append("3 ");

			// Here, we create the polylist's reference array
			// It uses a stride of four, the 1st referencing a vertex, the 2nd a normal, the 3rd a texture coordinate and the 4th a vertex colour
			var pstring = new StringBuilder();
			for(var i = 0; i < polygons.Length; i++)
			{
				for(var j = 0; j < 3; j++)
				{
					// Vertex
					pstring.Append(" " + polygons[i].Indices[j]);

					// Normal
					pstring.Append(" " + normals[i, j]);

					// Tex coord
					pstring.Append(" " + mesh.GetUVIndex(i, j));

					// Vertex colour
					pstring.Append(" " + (usesColours ? polygons[i].Indices[j] : 0));
				}
			}

			var polylist =
			new XElement("polylist", new XAttribute("count", polygons.Length), new XAttribute("material", "#" + materialname),
				new XElement("input", new XAttribute("offset", 0), new XAttribute("semantic", "VERTEX"), new XAttribute("source", "#" + name + "-vertices")),
				new XElement("input", new XAttribute("offset", 1), new XAttribute("semantic", "NORMAL"), new XAttribute("source", "#" + name + "-normals")),
				new XElement("input", new XAttribute("offset", 2), new XAttribute("semantic", "TEXCOORD"), new XAttribute("source", "#" + name + "-coords")),
				new XElement("input", new XAttribute("offset", 3), new XAttribute("semantic", "COLOR"), new XAttribute("source", "#" + name + "-colours")),
				new XElement("vcount", vcount),
				new XElement("p", pstring));

			meshElement.Add(posSource);
			meshElement.Add(normalSource);
			meshElement.Add(texSource);
			meshElement.Add(vertexSource);
			meshElement.Add(polylist);
			#endregion

			#region Scene Library
			var sceneLib =
			new XElement("library_visual_scenes",
				new XElement("visual_scene", new XAttribute("id", "scene"), new XAttribute("name", "scene"),
					new XElement("node", new XAttribute("id", "CryExportNode_" + name),
						new XElement("node", new XAttribute("id", name),
							new XElement("translate", new XAttribute("sid", "translation"), "0 0 0"),
							new XElement("rotate", new XAttribute("sid", "rotation_Z"), "0 0 1 0.0"),
							new XElement("rotate", new XAttribute("sid", "rotation_Z"), "0 1 0 0.0"),
							new XElement("rotate", new XAttribute("sid", "rotation_Z"), "1 0 0 0.0"),
							new XElement("scale", new XAttribute("sid", "scale"), "1.0 1.0 1.0"),
							new XElement("instance_geometry", new XAttribute("url", "#" + name),
								new XElement("bind_material",
									new XElement("technique_common",
										new XElement("instance_material", new XAttribute("symbol", materialname), new XAttribute("target", "#" + materialname),
											new XElement("bind_vertex_input", new XAttribute("input_semantic", "TEXCOORD"), new XAttribute("input_set", 0), new XAttribute("semantic", "UVMap")))))),
							new XElement("extra",
								new XElement("technique", new XAttribute("profile", "CryEngine"),
									new XElement("properties")))),
						new XElement("extra",
							new XElement("technique", new XAttribute("profile", "CryEngine"),
									new XElement("properties", "fileType=cgf DoNotMerge"))))));

			root.Add(sceneLib);

			var scene =
			new XElement("scene",
				new XElement("instance_visual_scene", new XAttribute("url", "#scene")));

			root.Add(scene);
			#endregion

			var parentDir = Directory.GetParent(outputFile);
			if(!parentDir.Exists)
				parentDir.Create();

			File.WriteAllText(outputFile, doc.ToString());
		}
	}
}
