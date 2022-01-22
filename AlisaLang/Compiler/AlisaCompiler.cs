using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using AlisaLang.Parser.AST;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AlisaLang.Compiler
{
    public class AlisaCompiler
    {
        private readonly List<TreeElement> _treeElements;
        private readonly string _fileName;

        public AlisaCompiler(List<TreeElement> treeElements, string fileName)
        {
            _treeElements = treeElements;
            _fileName = fileName;
        }

        public void Compile()
        {
            var source = GetSource();
            var tree = CSharpSyntaxTree.ParseText(source, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.LatestMajor));

            var pathToAlisaLangLibrary = $"{AppDomain.CurrentDomain.BaseDirectory}/AlisaLang.dll";
            var fileNameWithoutExtension = _fileName.Replace(".alisa", string.Empty);
            var pathToOut = AppDomain.CurrentDomain.BaseDirectory + "/out/"; 
            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System")).Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.CSharp")).Location),
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(DynamicObject).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(pathToAlisaLangLibrary),
            };
            
            var options = new CSharpCompilationOptions(OutputKind.ConsoleApplication)
                .WithPlatform(Platform.AnyCpu)
                .WithOutputKind(OutputKind.ConsoleApplication);

            var compilation = CSharpCompilation.Create(
                fileNameWithoutExtension,
                syntaxTrees: new[] { tree },
                references: references,
                options: options);

            
            var result = compilation.Emit(pathToOut  + fileNameWithoutExtension + ".exe");
            if (!result.Success)
            {
                var failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (var diagnostic in failures)
                {
                    Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                }
            }
            
            File.Copy(pathToAlisaLangLibrary, pathToOut + "AlisaLang.dll");
            File.WriteAllText(pathToOut + fileNameWithoutExtension + ".runtimeconfig.json", GenerateRuntimeConfig());
        }


        private string GenerateRuntimeConfig()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(
                    stream,
                    new JsonWriterOptions() {Indented = true}
                ))
                {
                    writer.WriteStartObject();
                    writer.WriteStartObject("runtimeOptions");
                    writer.WriteString("tfm", "net5.0");
                    writer.WriteStartObject("framework");
                    writer.WriteString("name", "Microsoft.NETCore.App");
                    writer.WriteString("version", "5.0.0");
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        private string GetSource()
        {
            var builder = new StringBuilder("using System;using AlisaLang.Libs;namespace AlisaLangPresent{class Program{");
            foreach (var treeElement in _treeElements)
            {
                builder.AppendLine(treeElement.GetSource());
            }

            builder.AppendLine("}}");

            return builder.ToString();
        }
    }
}