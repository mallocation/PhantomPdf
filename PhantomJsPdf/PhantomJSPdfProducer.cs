using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhantomJsPdf
{
    public class PhantomJSPdfProducer : IPdfProducer
    {
        public Uri ConvertToPdf(string html)
        {
            var inputFile = Guid.NewGuid().ToString() + ".html";
            var outputFile = Guid.NewGuid().ToString() + ".pdf";

            using (var fs = File.OpenWrite(inputFile))
            using (var sw = new StreamWriter(fs))
                sw.Write(html);

            using (var p = new Process())
            {
                p.StartInfo.FileName = "phantomjs.exe";
                p.StartInfo.Arguments = string.Format("rasterize.js {0} {1} Letter", inputFile, outputFile);
                p.StartInfo.UseShellExecute = false;
                p.Start();
                p.WaitForExit();
            }

            File.Delete(inputFile);

            return new Uri(Path.GetFullPath(outputFile));
        }
    }
}
