using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;

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
            var parser = new Parser(settings =>
            {
                settings.AutoHelp = false;
                settings.AutoVersion = false;
            });
            parser.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(ParsingError);
        }

        static void Run(Options options)
        {
            try
            {
                var converter = new ImageConverter(options.Source, options.Target);
                var targetPath = options.TargetPath ??
                    options.Source.Substring(0, options.Source.LastIndexOf('.')) + '.' + options.Target;
                converter.Convert(targetPath);
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