using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADODesigner.Core.API
{
    public class ExtensionLoader
    {
        private ScriptEngine scriptEngine;
        ScriptScope scriptScope;
        public List<Extension> Extensions { get; set; }
        public ExtensionLoader()
        {
            scriptEngine = Python.CreateEngine();
            scriptScope = scriptEngine.CreateScope();
            Extensions = new List<Extension>();
        }
        public void AddExtension(string path)
        {
            scriptEngine.ExecuteFile(path, scriptScope);

            dynamic mainClass = scriptScope.GetVariable("main");
            dynamic main = scriptEngine.Operations.CreateInstance(mainClass);
            MessageBox.Show(main.author);
        }
    }
}
