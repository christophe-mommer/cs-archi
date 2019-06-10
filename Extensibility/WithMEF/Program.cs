using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;

namespace Extensibility
{
    class Program
    {
        [Import]
        private static IEnumerable<IShapePlugin> ShapePlugins
        {
            get; set;
        }
        static void Main(string[] args)
        {
            var squareSide = new Side(5);
            var square = new Shape(Enumerable.Repeat(squareSide, 4).ToArray());

            var assembly = typeof(Program).Assembly;
            var assemblies = new[] { assembly };
            var configuration = new ContainerConfiguration()
                .WithAssembly(assembly);
            using (var container = configuration.CreateContainer())
            {
                ShapePlugins = container.GetExports<IShapePlugin>();
            }

            var plugins = new List<(byte, IShapePlugin)>();
            byte i = 1;
            foreach (var item in ShapePlugins)
            {
                plugins.Add((i, item));
                i++;
            }

            Console.WriteLine("Select the plugin you want to apply");

            foreach (var item in plugins)
            {
                Console.WriteLine($"{item.Item1} - {item.Item2.GetName()}");
            }

            var choice = Console.ReadLine();

            var plugin = plugins.FirstOrDefault(p => p.Item1.ToString() == choice);

            if (plugin.Item2 != null)
            {
                plugin.Item2.DoSomethingWithShape(square);
            }
        }
    }
}
