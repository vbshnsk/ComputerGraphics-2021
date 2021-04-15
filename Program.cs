using System;
using System.Collections.Generic;
using System.Diagnostics;
using CommandLine;
using ComputerGraphics.converter;
using ComputerGraphics.converter.writer;
using ComputerGraphics.di;
using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.config;
using ComputerGraphics.renderer.container;
using ComputerGraphics.renderer.lightning;
using ComputerGraphics.renderer.rays;
using ComputerGraphics.renderer.reader;
using ComputerGraphics.renderer.scene;

namespace ComputerGraphics
{
    class Program
    {
        public class Options
        {
            [Option("source", Required = true)] public string Source { get; set; }
            [Option("goal-format", Required = true)] public string Target { get; set; }
            [Option("output", Required = false)] public string TargetPath { get; set; }
        }  
        
        static void Main(string[] args)
        {
            var container = new Container();
            container.Register<IImageWriter, BmpImageWriter>();
            container.Register<ICameraProvider, StaticCameraProvider>();
            container.Register<IRayProvider, PerspectiveRayProvider>();
            container.Register<ISceneConfigProvider, StaticSceneConfigProvider>();
            container.Register<IObjReader, ObjParserObjReader>();
            container.Register<ILightningSourceProvider, StaticPointLightningSourceProvider>();
            container.Register<IObjectContainer, KdTreeObjectContainer>();
            container.Register<IScene, Scene>();

            var scene = container.Get<IScene>();
            var s = Stopwatch.StartNew();
            scene.WriteScene("/Users/vbshnsk/Desktop/school/comp-graphics/ComputerGraphics/dragon3.obj",
                "/Users/vbshnsk/Desktop/school/comp-graphics/ComputerGraphics/dragon3.bmp");
            s.Stop();
            Console.WriteLine(s.Elapsed.TotalSeconds);
            Console.Write('\n');
        }

        static void Run(Options options)
        {
            try
            {
                var converter = ImageConverterFactory.Get(options.Source, options.Target);
                var targetPath = options.TargetPath ??
                    options.Source.Substring(0, options.Source.LastIndexOf('.')) + '.' + options.Target;
                converter.Convert(options.Source, targetPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void ParsingError(IEnumerable<Error> e)
        {
            Console.WriteLine("Error parsing arguments, please check your values.");
        }
    }
}