using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WebForms;

namespace Framework.Report
{
    public class ReporteHelper
    {
        public static byte[] RenderReport(ReportTypeEnum type, Dictionary<string, object> dataSources, string reportePathCompleto, string[] columnasMostrar,
                                                 IDictionary<string, string> parametros, IDictionary<string, string> imagenes,
                                                 decimal? pageWidth, decimal? pageHeight, decimal? marginTop, decimal? marginLeft, decimal? marginRight, decimal? marginBottom, out string mimeType)
        {
            var localReport = new LocalReport();

            ReporteLoadReportDefinition(localReport, reportePathCompleto, imagenes);

            localReport.DataSources.Clear();

            foreach (var dataSourcesName in dataSources.Keys)
            {
                var reporteDataSource = new ReportDataSource(dataSourcesName, dataSources[dataSourcesName]);
                localReport.DataSources.Add(reporteDataSource);
            }

            //Imagenes externas
            localReport.EnableExternalImages = true;

            if (columnasMostrar.Any())
            {
                var sb = new StringBuilder();
                foreach (string columna in columnasMostrar)
                {
                    sb.AppendFormat("#{0}#", columna.Trim());
                }
                parametros.Add("Columnas", sb.ToString());
            }

            foreach (var clave in parametros.Keys)
            {
                var param = new ReportParameter();
                param.Name = clave;
                param.Values.Add(parametros[clave]);
                localReport.SetParameters(param);
            }

            string outputFormat = OutputFormat(type);
            string reporteType = ReportType(type);
            string encoding;
            string fileNameExtension;

            StringBuilder deviceInfo = new StringBuilder();

            deviceInfo.AppendFormat("<DeviceInfo>");
            deviceInfo.AppendFormat("<OutputFormat>{0}</OutputFormat>", outputFormat);

            
            if (pageWidth.HasValue)
            {
                deviceInfo.AppendFormat("  <PageWidth>{0}cm</PageWidth>", pageWidth);
            }

            if (pageHeight.HasValue)
            {
                deviceInfo.AppendFormat("  <PageHeight>{0}cm</PageHeight>", pageHeight);
            }

            if (marginTop.HasValue)
            {
                deviceInfo.AppendFormat("  <MarginTop>{0}cm</MarginTop>", marginTop);
            }

            if (marginLeft.HasValue)
            {
                deviceInfo.AppendFormat("  <MarginLeft>{0}cm</MarginLeft>", marginLeft);
            }

            if (marginRight.HasValue)
            {
                deviceInfo.AppendFormat("  <MarginRight>{0}cm</MarginRight>", marginRight);
            }

            if (marginBottom.HasValue)
            {
                deviceInfo.AppendFormat("  <MarginBottom>{0}cm</MarginBottom>", marginBottom);
            }

            deviceInfo.AppendLine("<Encoding>UTF-8</Encoding>");
            deviceInfo.AppendFormat("</DeviceInfo>");

            Warning[] warnings;
            string[] streams;

            localReport.Refresh();

            //Render the report
            return localReport.Render(
                reporteType,
                deviceInfo.ToString(),
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
        }

        /// <summary>
        /// Renderizar un reporte a un type especifico
        /// </summary>
        /// <param name="dataSourceName"></param>
        /// <param name="dataSource"></param>
        /// <param name="reportePathCompleto">Path completo para cargar el reporte desde el servidor</param>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static byte[] RenderReport(ReportTypeEnum type, Dictionary<string, object> dataSources, string reportePathCompleto, string[] columnasMostrar,
                                                    IDictionary<string, string> parametros, IDictionary<string, string> imagenes, out string mimeType)
        {
            return RenderReport(type, dataSources, reportePathCompleto, columnasMostrar, parametros, imagenes, null, null, null, null, null, null, out mimeType);
        }

        public static byte[] RenderReport(ReportTypeEnum type, string dataSourceName, object dataSource, string reportePathCompleto,
            string[] columnasMostrar, IDictionary<string, string> parametros, out string mimeType)
        {

            var dataSources = new Dictionary<string, object>();
            dataSources.Add(dataSourceName, dataSource);

            return RenderReport(type, dataSources, reportePathCompleto, columnasMostrar, parametros, null, out mimeType);
        }

        public static byte[] RenderReport(ReportTypeEnum type, string dataSourceName, object dataSource, string reportePathCompleto, string[] columnasMostrar, out string mimeType)
        {
            return RenderReport(type, dataSourceName, dataSource, reportePathCompleto, columnasMostrar, new Dictionary<string, string>(), out mimeType);
        }

        public static byte[] RenderReport(ReportTypeEnum type, string dataSourceName, object dataSource, string reportePathCompleto, IDictionary<string, string> parametros, out string mimeType)
        {
            return RenderReport(type, dataSourceName, dataSource, reportePathCompleto, new string[0], parametros, out mimeType);
        }

        /// <summary>
        /// Renderizar un reporte a un type especifico
        /// </summary>
        /// <param name="dataSourceName"></param>
        /// <param name="dataSource"></param>
        /// <param name="reportePathCompleto">Path completo para cargar el reporte desde el servidor</param>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static byte[] RenderReport(ReportTypeEnum type, string dataSourceName, object dataSource, string reportePathCompleto, out string mimeType)
        {
            return RenderReport(type, dataSourceName, dataSource, reportePathCompleto, new string[0], new Dictionary<string, string>(), out mimeType);
        }

        /// <summary>
        /// Carga la definicion del reporte cambiando namesapces y secciones 
        /// </summary>
        /// <param name="localReport">Instancia de LocalReport</param>
        /// <param name="reportePathCompleto">Path del reporte completo para acceder</param>
        public static void ReporteLoadReportDefinition(LocalReport localReport, string reportePathCompleto, IDictionary<string, string> imagenes)
        {
            string strReport = System.IO.File.ReadAllText(reportePathCompleto, System.Text.Encoding.Default);
            if (strReport.Contains("http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition"))
            {
                strReport =
                    strReport.Replace(
                        "<Report xmlns:rd=\"http://schemas.microsoft.com/SQLServer/reporting/reportdesigner\" xmlns:cl=\"http://schemas.microsoft.com/sqlserver/reporting/2010/01/componentdefinition\" xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition\">",
                        "<Report xmlns:rd=\"http://schemas.microsoft.com/SQLServer/reporting/reportdesigner\" xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition\">");
                strReport =
                    strReport.Replace("<ReportSections>", "").Replace("<ReportSection>", "").Replace(
                        "</ReportSection>", "").Replace("</ReportSections>", "");
            }

            if (imagenes != null)
            {
                foreach (var imagenNombre in imagenes.Keys)
                {
                    strReport = CambiarEmbeddedImages(strReport, imagenNombre, imagenes[imagenNombre]);
                }
            }

            byte[] bytReport = System.Text.Encoding.UTF8.GetBytes(strReport);
            localReport.LoadReportDefinition(new MemoryStream(bytReport));
        }

        public static string OutputFormat(ReportTypeEnum type)
        {
            string typeCadena = string.Empty;
            switch (type)
            {
                case ReportTypeEnum.Pdf:
                    typeCadena = "PDF";
                    break;
                case ReportTypeEnum.Excel:
                    typeCadena = "Excel";
                    break;
                case ReportTypeEnum.Word:
                    typeCadena = "Word";
                    break;
                case ReportTypeEnum.Imagen:
                    typeCadena = "Image";
                    break;
                case ReportTypeEnum.PNG:
                    typeCadena = "PNG";
                    break;
            }

            return typeCadena;
        }

        public static string ReportType(ReportTypeEnum type)
        {
            string typeCadena = string.Empty;
            switch (type)
            {
                case ReportTypeEnum.Pdf:
                    typeCadena = "PDF";
                    break;
                case ReportTypeEnum.Excel:
                    typeCadena = "Excel";
                    break;
                case ReportTypeEnum.Word:
                    typeCadena = "Word";
                    break;
                case ReportTypeEnum.Imagen:
                    typeCadena = "Image";
                    break;
                case ReportTypeEnum.PNG:
                    typeCadena = "Image";
                    break;
            }

            return typeCadena;
        }

        private static string CambiarEmbeddedImages(string xml, string imagenNombre, string imagenBase64)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlNamespaceManager nsMgr = new XmlNamespaceManager(doc.NameTable);
                nsMgr.AddNamespace("ns", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");

                var node = doc.SelectSingleNode("/ns:Report/ns:EmbeddedImages", nsMgr);
                if (node != null)
                {
                    //Recorriendo todas las imagenes EmbeddedImages
                    foreach (XmlNode imagen in node.ChildNodes)
                    {
                        //Preguntar si el nombre de la imagen es (atributo Name)
                        if (imagen.Attributes["Name"].Value.ToLower() == imagenNombre.ToLower())
                        {
                            //Modificando el valor
                            foreach (XmlNode elem in imagen.ChildNodes)
                            {
                                //if (elem.Name == "MIMEType")
                                //{
                                //    elem.InnerText = valor;
                                //}

                                if (elem.Name == "ImageData")
                                {
                                    elem.InnerText = imagenBase64;
                                }
                            }
                        }

                    }
                }

                StringWriter sw = new StringWriter();
                XmlTextWriter xtw = new XmlTextWriter(sw);
                doc.WriteTo(xtw);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
                /*
                 * Possible Exceptions:
                 *  System.ArgumentException
                 *  System.ArgumentNullException
                 *  System.InvalidOperationException
                 *  System.IO.DirectoryNotFoundException
                 *  System.IO.FileNotFoundException
                 *  System.IO.IOException
                 *  System.IO.PathTooLongException
                 *  System.NotSupportedException
                 *  System.Security.SecurityException
                 *  System.UnauthorizedAccessException
                 *  System.UriFormatException
                 *  System.Xml.XmlException
                 *  System.Xml.XPath.XPathException
                */
            }
        }

        public static byte[] MergePdfs(IEnumerable<byte[]> inputFiles)
        {
            MemoryStream outputStream = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, outputStream);
            document.Open();
            PdfContentByte content = writer.DirectContent;

            foreach (byte[] input in inputFiles)
            {
                PdfReader reader = new PdfReader(input);
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    document.SetPageSize(reader.GetPageSizeWithRotation(i));
                    document.NewPage();
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    int rotation = reader.GetPageRotation(i);
                    if (rotation == 90 || rotation == 270)
                    {
                        content.AddTemplate(page, 0, -1f, 1f, 0, 0,
                            reader.GetPageSizeWithRotation(i).Height);
                    }
                    else
                    {
                        content.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    }
                }
            }

            document.Close();

            return outputStream.ToArray();
        }
    }
}
