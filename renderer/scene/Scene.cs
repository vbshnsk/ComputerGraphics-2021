using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComputerGraphics.converter.@struct;
using ComputerGraphics.converter.writer;
using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.config;
using ComputerGraphics.renderer.container;
using ComputerGraphics.renderer.lightning;
using ComputerGraphics.renderer.rays;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.scene
{
    public class Scene : IScene
    {
        private ICameraProvider _cameraProvider;
        private IRayProvider _rayProvider;
        private ISceneConfigProvider _configProvider;
        private IImageWriter _imageWriter;
        private IObjectContainer _objContainer;
        private ILightningSourceProvider _lightningProvider;

        public Scene(ICameraProvider cameraProvider, IRayProvider rayProvider, ISceneConfigProvider configProvider,
            IObjectContainer container, IImageWriter writer, ILightningSourceProvider lightningProvider)
        {
            _cameraProvider = cameraProvider;
            _configProvider = configProvider;
            _rayProvider = rayProvider;
            _objContainer = container;
            _imageWriter = writer;
            _lightningProvider = lightningProvider;
        }

        private void LoadObj(string pathToFile)
        {
            _objContainer.LoadObj(pathToFile);
        }
        
        private RGBA[,] Draw()
        {
            var camera = _cameraProvider.Get();
            var config = _configProvider.Get();
            var matrix = new RGBA[config.Height, config.Width];
            var lightning = _lightningProvider.Get();
            Console.WriteLine("Started render");
            Parallel.For(0, config.Width, x =>
            {
                Parallel.For(0, config.Height, y =>
                {
                    var ray = _rayProvider.Get(x, y);
                    var lastHitDistance = float.MaxValue;
                    var allHit = _objContainer.GetObjectsByRay(ray);
                    
                    Parallel.ForEach(allHit, obj =>
                    {
                        if (obj.HitBy(ray, camera.Position))
                        {
                            var hitDistance = (camera.Position - obj.MidPoint).Length;
                            
                            if (lastHitDistance > hitDistance)
                            {
                                lastHitDistance = hitDistance;
                                matrix[y, x] = lightning.Illuminate(obj);
                            }
                        }
                    });
                });
            });
            Console.WriteLine("Finished render");

            return matrix;
        }
        
        public void WriteScene(string source, string writeTo)
        {
            LoadObj(source);
            var matrix = Draw();
            var config = _configProvider.Get();
            var pixels = new List<RGBA>();
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var tmp = new List<RGBA>();
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    tmp.Add(matrix[i, j]);
                }

                tmp.Reverse();
                pixels.InsertRange(0, tmp);
            }
            _imageWriter.Write(writeTo, pixels, config.Width, config.Height);
        }
    }
}