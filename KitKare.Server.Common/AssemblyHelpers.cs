using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KitKare.Server.Common
{
    public static class AssemblyHelpers
    {
        public static string GetDirectoryForAssembly(Assembly assembly)
        {
            var assemblyLocation = assembly.CodeBase;
            var location = new UriBuilder(assemblyLocation);
            var path = Uri.UnescapeDataString(location.Path);
            path = path.Substring(0, path.IndexOf("KitKare.Server"));
            var directory = Path.GetDirectoryName(path);
            return directory;
        }
    }
}
