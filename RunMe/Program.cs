using Contracts;
using PhantomJsPdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunMe
{
    class Program
    {
        static void Main(string[] args)
        {
            IPdfProducer producer = new PhantomJSPdfProducer();

            string inputHtml = null;

            using (var sr = new StreamReader("input.html"))
            {
                inputHtml = sr.ReadToEnd();
            }

            var pdf = producer.ConvertToPdf(inputHtml);

            Process.Start(pdf.LocalPath);
        }
    }
}
