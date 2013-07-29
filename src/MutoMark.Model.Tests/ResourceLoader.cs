using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MutoMark.Model.Tests
{
    static class ResourceLoader
    {
        public static string GetResourceString(string resName)
        {
            var name = string.Format("MutoMark.Model.Tests.{0}", resName);
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);

            try
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
    }
}
