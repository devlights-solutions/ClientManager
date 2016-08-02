using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Framework.Report
{
    public class ReportFactory
    {
        /// <summary>
        /// Ancho de pagina en cm para el reporte
        /// </summary>
        public decimal? PageWidth { get; private set; }

        /// <summary>
        /// Alto de pagina en cm para el reportes
        /// </summary>
        public decimal? PageHeight { get; private set; }

        /// <summary>
        /// Margin Superioren cm de pagina para el reporte
        /// </summary>
        public decimal? MarginTop { get; private set; }

        /// <summary>
        /// Margin Izquierdo en cm de pagina para el reporte
        /// </summary>
        public decimal? MarginLeft { get; private set; }

        /// <summary>
        /// Margin Derecho en cm de pagina para el reporte
        /// </summary>
        public decimal? MarginRight { get; private set; }

        /// <summary>
        /// Margin Inferior en cm de pagina para el reporte
        /// </summary>
        public decimal? MarginBottom { get; private set; }
        
        /// <summary>
        /// Path fisico completo del reporte .rdlc
        /// </summary>
        public string PathCompleto { get; private set; }

        /// <summary>
        /// Listado de Origenes de Datos con sus respectivas claves
        /// </summary>
        public Dictionary<string, object> DataSources { get; private set; }

        /// <summary>
        /// Listado de Parametros y valores
        /// </summary>
        public Dictionary<string, string> Parametros { get; private set; }

        /// <summary>
        /// Listado de Imagenes como parametros
        /// </summary>
        public Dictionary<string, string> Imagenes { get; private set; }

        public List<string> ColumnasMostrar { get; private set; }

        /// <summary>
        /// Tipo Mime del parametro.
        /// Solo util despues de renderizar
        /// </summary>
        public String MimeType { get; private set; }

        public ReportFactory() {
            //Inicializando
            this.DataSources = new Dictionary<string, object>();
            this.Parametros = new Dictionary<string, string>();
            this.Imagenes = new Dictionary<string, string>();
            this.ColumnasMostrar = new List<string>();
        }

        public ReportFactory SetPathCompleto(string pathCompleto)
        {
            this.PathCompleto = pathCompleto;
            return this;
        }

        public ReportFactory SetDataSource(Dictionary<string, object> dataSources)
        {
            //Asignando valores a los dataSources existentes en la coleccion
            foreach (var clave in dataSources.Keys)
            {
                SetDataSource(clave, dataSources[clave]);
            }

            return this;
        }

        public ReportFactory SetDimencines(decimal pageWidth, decimal pageHeight, decimal marginTop, decimal marginLeft, decimal marginRight, decimal marginBottom)
        {
            this.PageWidth = pageWidth;
            this.PageHeight = pageHeight;
            this.MarginTop = marginTop;
            this.MarginLeft = marginLeft;
            this.MarginRight = marginRight;
            this.MarginBottom = marginBottom;
            return this;
        }

        /// <summary>
        /// Asignando un datasource a la coleccion
        /// Si el datasource ya existe se sobreescribe
        /// </summary>
        /// <param name="dataSourceName">Nombre del datasource</param>
        /// <param name="datasource">Contenido del datasource</param>
        /// <returns></returns>
        public ReportFactory SetDataSource(string dataSourceName, object datasource)
        {
            if (String.IsNullOrEmpty(dataSourceName))
            {
                throw new ApplicationException("Debe especificar un nombre para el datasource");
            }

            //Verificar si existe un parametro con el mismo nombre ya cargado
            if (this.DataSources.ContainsKey(dataSourceName))
            {
                this.DataSources[dataSourceName] = datasource;
            }
            else
            {
                this.DataSources.Add(dataSourceName, datasource);
            }

            return this;
        }

        /// <summary>
        /// Asignar listado de parametros a un reporte
        /// Si el parametro ya existe se sobreescribe
        /// </summary>
        /// <param name="param">Diccionario de parametros</param>
        /// <returns></returns>
        public ReportFactory SetParametro(Dictionary<string, string> param)
        {
            //Asignando valores a los parametros existentes en la coleccion
            foreach (var clave in param.Keys)
            {
                SetParametro(clave, param[clave]);
            }

            return this;
        }

        /// <summary>
        /// Asignar un parametro al reporte
        /// Si el parametro ya existe se sobreescribe
        /// </summary>
        /// <param name="clave">Nombre del parametro (no se puede repetir o se sobreescribe</param>
        /// <param name="valor">Valor del parametro</param>
        /// <returns></returns>
        public ReportFactory SetParametro(string clave, string valor) {

            if (String.IsNullOrEmpty(clave))
            {
                throw new ApplicationException("No se puede enviar un parametro sin nombre al reporte");
            }

            //Verificar si existe un parametro con el mismo nombre ya cargado
            if (this.Parametros.ContainsKey(clave))
            {
                this.Parametros[clave] = valor;
            }
            else
            {
                this.Parametros.Add(clave, valor);
            }

            return this;
        }
        
        public ReportFactory SetImagen(Dictionary<string, string> imagenes)
        {
            //Asignando valores a de las imagenes existentes en la coleccion
            foreach (var clave in imagenes.Keys)
            {
                SetParametro(clave, imagenes[clave]);
            }

            return this;
        }

        public ReportFactory SetImagen(string clave, string pathImagen)
        {

            if (String.IsNullOrEmpty(pathImagen))
            {
                throw new ApplicationException("No se puede asignar un path de imagen vacio");
            }

            if (!File.Exists(pathImagen))
            {
                throw new ApplicationException(String.Format("No existe el path de la imagen", pathImagen));
            }

            var bitmap = new Bitmap(pathImagen);
            string imagenEnBase64;
            //TODO vericar formato de imagen 
            ImageFormat format = ImageFormat.Png;
            string formatExtension = "png";

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, format);
                imagenEnBase64 = Convert.ToBase64String(ms.ToArray());
            }

            //Verificar si existe una imagen con el mismo nombre ya cargada
            if (this.Imagenes.ContainsKey(clave))
            {
                this.Imagenes[clave] = imagenEnBase64;
            }
            else
            {
                this.Imagenes.Add(clave, imagenEnBase64);
            }

            return this;
        }

        /// <summary>
        /// Agregar un listado de columnas a mostrar
        /// </summary>
        /// <param name="columnasAMostrar"></param>
        /// <returns></returns>
        /// 
        public ReportFactory SetColumnaAMostrar(List<string> columnasAMostrar)
        {

            foreach (var columna in columnasAMostrar)
            {
                SetColumnaAMostrar(columna);
            }

            return this;
        }

        public ReportFactory SetColumnaAMostrar(string[] columnasAMostrar)
        {
            SetColumnaAMostrar(columnasAMostrar.ToList());

            return this;
        }

        /// <summary>
        /// Agregar una columna para visualizar
        /// </summary>
        /// <param name="columna"></param>
        /// <returns></returns>
        public ReportFactory SetColumnaAMostrar(string columna)
        {
            
            //Verificar si existe un parametro con el mismo nombre ya cargado
            if (!this.ColumnasMostrar.Contains(columna))
            {
                this.ColumnasMostrar.Add(columna);
            }

            return this;
        }

        /// <summary>
        /// Setear columnas a partir de una cadena separa por coma
        /// </summary>
        /// <param name="cadenaSeparaPorComa"></param>
        /// <returns></returns>
        public ReportFactory SetColumnaAMostrarDesdeCsv(string cadenaSeparaPorComa)
        {
            var columnas = cadenaSeparaPorComa.Split(',');
            return SetColumnaAMostrar(columnas);
        }

        #region Render

        public byte[] Renderizar(ReportTypeEnum tipo) {

            string mimeTypeOut = string.Empty;
            byte[] archivo = ReporteHelper.RenderReport(
                tipo,
                this.DataSources,
                this.PathCompleto.ToString(),
                this.ColumnasMostrar.ToArray(),
                this.Parametros,
                this.Imagenes, this.PageWidth, this.PageHeight, this.MarginTop, this.MarginLeft, this.MarginRight,
                this.MarginBottom,
                out mimeTypeOut);

            this.MimeType = mimeTypeOut;
            return archivo;
        }

        #endregion




    }
}
