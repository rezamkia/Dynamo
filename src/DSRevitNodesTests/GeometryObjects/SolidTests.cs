﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using DSRevitNodes.Elements;
using DSRevitNodes.GeometryObjects;
using NUnit.Framework;

namespace DSRevitNodesTests.GeometryObjects
{
    [TestFixture]
    public class SolidTests
    {
        private string geomDir;

        public string TestGeometryDirectory
        {
            get
            {
                var assDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().FullName);
                geomDir = Path.Combine(assDir, @"test_geometry");
                if (!Directory.Exists(geomDir))
                {
                    Directory.CreateDirectory(geomDir);
                }
                return geomDir;
            }    
        }

        [Test]
        public void ByExtrusion_ValidArgs()
        {
            var crvs = UnitRectangle();

            // construct the curveloop
            var curveloop = DSCurveLoop.ByCurves(crvs.ToArray());

            var dir = Vector.ByCoordinates(0, 0, 1);
            var dist = 5;
            var extrusion = DSSolid.ByExtrusion(curveloop, dir, dist);

            Assert.NotNull(extrusion);
            Assert.AreEqual(2.5, extrusion.Volume, 0.01);
            Assert.AreEqual(5 + 5 + 0.5 + 0.5 + Math.Sqrt(2) * 5, extrusion.SurfaceArea, 0.01);
        }

        [Test]
        public void ByRevolve_ValidArgs()
        {
            //create a unit rectangle in the world XY plane
            var crvs = UnitRectangle();

            var origin = Point.ByCoordinates(0, 0, 0);
            var x = Vector.ByCoordinates(1, 0, 0);
            var y = Vector.ByCoordinates(0, 1, 0);
            var z = Vector.ByCoordinates(0, 0, 1);

            var cs = CoordinateSystem.ByOriginVectors(origin, x, y, z);

            var planeCs = CoordinateSystem.ByOriginVectors(origin, x, z, y);
            var transCrvs = crvs.Select(crv => crv.Transform(CoordinateSystem.WCS, planeCs)).Cast<Curve>().ToList();

            var revolve = DSSolid.ByRevolve(transCrvs, cs, 0, 3.14);

            var package = new RenderPackage(); 
            revolve.Tessellate(package);

            var modelPath = Path.Combine(TestGeometryDirectory, @"Solid_ByRevolve.obj");
            if(File.Exists(modelPath))
                File.Delete(modelPath);
            WriteToOBJ(modelPath, new List<RenderPackage>(){package});
            
            Assert.NotNull(revolve);
        }

        private static List<Curve> UnitRectangle()
        {
            // construct a unit rectangle
            var l1 = Line.ByStartPointEndPoint(
                Point.ByCoordinates(0, 0, 0),
                Point.ByCoordinates(1, 0, 0)
            );

            var l2 = Line.ByStartPointEndPoint(
                Point.ByCoordinates(1, 0, 0),
                Point.ByCoordinates(1, 1, 0)
            );

            var l3 = Line.ByStartPointEndPoint(
                Point.ByCoordinates(1, 1, 0),
                Point.ByCoordinates(0.0, 1, 0)
            );

            var l4 = Line.ByStartPointEndPoint(
                Point.ByCoordinates(0.0, 1, 0),
                Point.ByCoordinates(0.0, 0, 0)
            );

            var crvs = new List<Curve>{l1,l2,l3,l4};
            return crvs;
        }

        private static void WriteToOBJ(string path, List<RenderPackage> packages)
        {
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (var package in packages)
                {
                    for (int i = 0; i < package.TriangleVertices.Count; i += 3)
                    {
                        tw.WriteLine(string.Format("v {0} {1} {2}", package.TriangleVertices[i], package.TriangleVertices[i+1], package.TriangleVertices[i+2]));
                    }

                    int count = 1;
                    for (int i = 0; i < (package.TriangleVertices.Count/3)/3; i ++)
                    {
                        tw.WriteLine(string.Format("f {0} {1} {2}", count, count + 1, count + 2));
                        count += 3;
                    }
                }
            }
        }
    }

    public class RenderPackage : IRenderPackage
    {
        public List<double> TriangleVertices { get; set; }
        public List<double> PointVertices { get; set; }
        public List<double> PointVertexNormals { get; set; }
        public List<double> TriangleVertexNormals { get; set; }
        public List<byte> PointVertexColors { get; set; }
        public List<byte> TriangleVertexColors { get; set; }
        public List<double> LineStripVertices { get; set; }
        public int LineStripVertexCount { get; set; }
        public List<byte> LineStripVertexColors { get; set; }

        public RenderPackage()
        {
            TriangleVertices = new List<double>();
            PointVertices = new List<double>();
            PointVertexNormals = new List<double>();
            TriangleVertexNormals = new List<double>();
            PointVertexColors = new List<byte>();
            TriangleVertexColors = new List<byte>();
            LineStripVertices = new List<double>();
        }

        public void PushPointVertex(double x, double y, double z)
        {
            PointVertices.Add(x);
            PointVertices.Add(y);
            PointVertices.Add(z);
        }

        public void PushPointVertexColor(byte red, byte green, byte blue, byte alpha)
        {
            PointVertexColors.Add(red);
            PointVertexColors.Add(green);
            PointVertexColors.Add(blue);
            PointVertexColors.Add(alpha);
        }

        public void PushTriangleVertex(double x, double y, double z)
        {
            TriangleVertices.Add(x);
            TriangleVertices.Add(y);
            TriangleVertices.Add(z);
        }

        public void PushTriangleVertexNormal(double x, double y, double z)
        {
            TriangleVertexNormals.Add(x);
            TriangleVertexNormals.Add(y);
            TriangleVertexNormals.Add(z);
        }

        public void PushTriangleVertexColor(byte red, byte green, byte blue, byte alpha)
        {
            TriangleVertexColors.Add(red);
            TriangleVertexColors.Add(green);
            TriangleVertexColors.Add(blue);
            TriangleVertexColors.Add(alpha);
        }

        public void PushLineStripVertex(double x, double y, double z)
        {
            TriangleVertices.Add(x);
            TriangleVertices.Add(y);
            TriangleVertices.Add(z);
        }

        public void PushLineStripVertexCount(int n)
        {
            LineStripVertexCount = n;
        }

        public void PushLineStripVertexColor(byte red, byte green, byte blue, byte alpha)
        {
            LineStripVertexColors.Add(red);
            LineStripVertexColors.Add(green);
            LineStripVertexColors.Add(blue);
            LineStripVertexColors.Add(alpha);
        }

        public IntPtr NativeRenderPackage { get; private set; }
    }
}