using System;
using System.Collections.Generic;
using ComputerGraphics.converter.@struct;
using ComputerGraphics.converter.writer;
using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.config;
using ComputerGraphics.renderer.rays;
using ComputerGraphics.renderer.reader;
using ComputerGraphics.renderer.@struct;
using ComputerGraphics.renderer.util;

namespace ComputerGraphics.renderer.scene
{
    public class Scene
    {
        private ICameraProvider _cameraProvider;
        private IRaysProvider _raysProvider;
        private ISceneConfigProvider _configProvider;
        private IObjReader _objReader;
        private IImageWriter _imageWriter;
        private Triangle[] _triangles;

        public Scene(ICameraProvider cameraProvider, IRaysProvider raysProvider, ISceneConfigProvider configProvider,
            IObjReader reader, IImageWriter writer)
        {
            _cameraProvider = cameraProvider;
            _configProvider = configProvider;
            _raysProvider = raysProvider;
            _objReader = reader;
            _imageWriter = writer;
        }

        public void LoadObj(string pathToFile)
        {
            _triangles = _objReader.Read(pathToFile);
        }

        // TODO: refactor from bool to some struct
        public bool[,] Draw(int width, int height)
        {
            var matrix = new bool[width, height];
            var camera = _cameraProvider.Get();
            var config = _configProvider.Get();
            var rays = _raysProvider.Get(width, height, config.Fov);
            
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var ray = rays[i, j];
                    foreach (var triangle in _triangles)
                    {
                        //Console.WriteLine(triangle.Item1.X + " " + triangle.Item1.Y + " " + triangle.Item1.Z);
                        matrix[i, j] = RayIntersection.WithTriangle(camera.Position, ray, triangle);
                    }
                }
            }

            return matrix;
        }
        
        // TODO: refactor    
        public void WriteScene(int width, int height, string writeTo)
        {
            var matrix = Draw(width, height);
            var pixels = new List<RGBA>();
            foreach (var b in matrix)
            {
                pixels.Add(b ? 
                    new RGBA {Alpha = 255, Blue = 0, Green = 0, Red = 0} 
                    : new RGBA {Alpha = 255, Blue = 255, Green = 255, Red = 255});
            }
            _imageWriter.Write(writeTo, pixels, width, height);
        }
    }
}