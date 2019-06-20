using ExampleMappingCLI.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExampleMappingCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var csproj = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(csproj))
            {
                Console.WriteLine("You must run exmap within a project directory");
                return;
            }
            var exmappath = Path.Combine(Path.GetTempPath(), "exmap");
            if(Directory.Exists(exmappath))
            {
                Directory.Delete(exmappath, true);
            }
            Directory.CreateDirectory(exmappath);
            string tempBuildDir = Path.Combine(exmappath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempBuildDir);
            var process = Process.Start("dotnet", $"build -c Debug {csproj} -o {tempBuildDir}");
            process.WaitForExit();
            var dlls = Directory.GetFiles(tempBuildDir, "*.dll");
            foreach (var dll in dlls.AsParallel())
            {
                Assembly a = null;
                try
                {
                    a = Assembly.LoadFile(dll);
                }
                catch
                {
                    continue;
                }
                Console.WriteLine($"Starting searchin in {dll}");
                var mappedClasses = a
                .GetTypes()
                .Where(t => t.IsDefined(typeof(MapFromClass)));

                foreach (var item in mappedClasses)
                {
                    Console.WriteLine($"Mapping class {item.Name}");
                    string template = @"
namespace [NAMESPACE]
{
    public static class [RESULT]_Mapper
    {
        public static [RESULT] Map([VALUE] value)
        {
            [BODY]
        }
    }
}
";
                    StringBuilder methodBuilder
                        = new StringBuilder(template);
                    methodBuilder = methodBuilder.Replace("[NAMESPACE]", item.Namespace);
                    methodBuilder = methodBuilder.Replace("[RESULT]", item.Name);
                    var fromAttribute = item.GetCustomAttribute<MapFromClass>();
                    methodBuilder = methodBuilder.Replace("[VALUE]", fromAttribute.MappingType.Name);

                    var fromProperties = fromAttribute
                        .MappingType
                        .GetRuntimeProperties()
                        .Where(m => m.GetMethod != null);
                    StringBuilder bodyBuilder = new StringBuilder();
                    bodyBuilder.AppendLine($"var result = new {item.Name}();");
                    foreach (var prop in item.GetRuntimeProperties().Where(m => m.SetMethod != null))
                    {
                        var conjointProp = fromProperties.FirstOrDefault(p => p.Name == prop.Name
                            && p.PropertyType == prop.PropertyType);
                        if (conjointProp != null)
                        {
                            bodyBuilder.AppendLine($"result.{prop.Name} = value.{prop.Name};");
                        }
                    }
                    bodyBuilder.AppendLine("return result;");
                    methodBuilder.Replace("[BODY]", bodyBuilder.ToString());

                    File.WriteAllText($"./{item.Name}_Mapper.cs", methodBuilder.ToString());
                }
            }
        }
    }
}
