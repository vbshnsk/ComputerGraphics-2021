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
        public bool[,] Draw()
        {
            var camera = _cameraProvider.Get();
            var config = _configProvider.Get();
            var matrix = new bool[config.Width, config.Height];

            for (int x = 0; x < config.Width; x++)
            {
                for (int y = 0; y < config.Height; y++)
                {
                    var ray = _raysProvider.Get(x, y);
                    foreach (var triangle in _triangles)
                    {
                        if (RayIntersection.WithTriangle(camera.Position, ray, triangle))
                        {
                            matrix[y, x] = true;
                        }   
                    }
                }
            }
            
            return matrix;
        }
        
        // TODO: refactor    
        public void WriteScene(string writeTo)
        {
            var matrix = Draw();
            var config = _configProvider.Get();
            var pixels = new List<RGBA>();
            foreach (var b in matrix)
            {
                pixels.Add(b ? 
                    new RGBA {Alpha = 255, Blue = 0, Green = 0, Red = 0} 
                    : new RGBA {Alpha = 255, Blue = 255, Green = 255, Red = 255});
            }
            _imageWriter.Write(writeTo, pixels, config.Width, config.Height);
        }
    }
}