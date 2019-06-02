using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Projeto.Presentation.Reports
{
    public class ReportUtil
    {
        public static byte[] GetPdfFile(string conteudo)
        {

            byte[] pdf = null;

            MemoryStream ms = new MemoryStream();
            TextReader reader = new StringReader(conteudo.ToString());

            Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            HTMLWorker html = new HTMLWorker(doc);

            doc.Open();
            html.StartDocument();
            html.Parse(reader);
            html.EndDocument();
            html.Close();
            doc.Close();

            return ms.ToArray();


        }

    }
}