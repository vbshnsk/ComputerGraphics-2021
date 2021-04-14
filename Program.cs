using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using ComputerGraphics.converter;
using ComputerGraphics.converter.writer;
using ComputerGraphics.di;
using ComputerGraphics.renderer;
using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.config;
using ComputerGraphics.renderer.kdtree;
using ComputerGraphics.renderer.rays;
using ComputerGraphics.renderer.reader;
using ComputerGraphics.renderer.scene;
using ComputerGraphics.renderer.@struct;

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
            // var parser = new Parser(settings =>
            // {
            //     settings.AutoHelp = false;
            //     settings.AutoVersion = false;
            // });
            // parser.ParseArguments<Options>(args)
            //     .WithParsed(Run)
            //     .WithNotParsed(ParsingError);
            var container = new Container();
            container.Register<IImageWriter, BmpImageWriter>();
            container.Register<ICameraProvider, StaticCameraProvider>();
            container.Register<IRaysProvider, PerspectiveRayProvider>();
            container.Register<ISceneConfigProvider, StaticSceneConfigProvider>();
            container.Register<IObjReader, ObjParserObjReader>();
            //container.Register<IObjReader, DummyObjReader>();

            container.Register<Scene, Scene>();

            var scene = container.Get<Scene>();
            scene.LoadObj("/Users/vbshnsk/Desktop/school/comp-graphics/ComputerGraphics/cow.obj");
            scene.WriteScene("/Users/vbshnsk/Desktop/school/comp-graphics/ComputerGraphics/cow.bmp");
            Console.Write('\n');
            var tree = new KDTree<Triangle>(container.Get<IObjReader>().Read("/Users/vbshnsk/Desktop/school/comp-graphics/ComputerGraphics/diamond.obj"));
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