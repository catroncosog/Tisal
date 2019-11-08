using System;
using System.Configuration;
using System.Web;
using System.Xml;
using log4net;

namespace WebService1
{
    /// <summary>
    /// Clase que autoriza cada Request validando que venga con un Token
    /// </summary>
    public class Authorization : IHttpModule
    {
        private string ServiceName;
        private string ServiceToken;

        //Se inicializa el objeto de log4net
        ILog log = LogManager.GetLogger("log");

        /// <summary>
        /// Inicialización de los Handler
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            //Carga la cfg de log4net desde web.config
            log4net.Config.XmlConfigurator.Configure();
            context.AuthenticateRequest += new EventHandler(this.OnEnter);
        }

        /// <summary>
        /// Captura todos los requests al levantar el WS o al entrar a un Web Method
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnEnter(object source, EventArgs e)
        {
            var tagToken = "Token";

            var app = (HttpApplication)source;
            var context = app.Context;
            var httpStream = context.Request.InputStream;
            var posStream = httpStream.Position;

            if (context.Request.ContentLength.Equals(0))
            {
                return;
            }
            else
            {
                if (context.Request.ContentType.ToLower().Equals("application/x-www-form-urlencoded"))
                    return;
                else
                {
                    if (context.Request.ServerVariables["HTTP_SOAP_CONTENTTYPE"] != null)
                    {
                        if (context.Request.ServerVariables["HTTP_SOAP_CONTENTTYPE"].ToLower().Equals("application/soap"))
                        {
                            tagToken = "tis:Token";
                        }
                    }
                }

                if (context.Request.ServerVariables["HTTP_SOAPACTION"] == null)
                {
                    DenyAccess(app, TipoError.Sin_SOAPAction);
                    return;
                }

                // Escribe log, es necesario declarar la variable 'log' al inicio de la clase y cargar la configuración en el método 'INIT'
                log.Info("Request recibido - Método: " + context.Request.ServerVariables["HTTP_SOAPACTION"] + " - " + app.Request.UserHostAddress);
            }

            #region Valida Service Name

            //Valida Service Name
            ServiceName = ConfigurationManager.AppSettings.Get("serviceName");
            if (string.IsNullOrEmpty(ServiceName))
            {
                DenyAccess(app, TipoError.Sin_ServiceName);
                return;
            }
            else
            {
                ServiceToken = ConfigurationManager.AppSettings.Get(ServiceName);
                if (string.IsNullOrEmpty(ServiceToken))
                {
                    DenyAccess(app, TipoError.Sin_ServiceToken);
                    return;
                }
            }

            #endregion

            // Llega un Web Method con Soap header
            var dom = new XmlDocument();
            string token = string.Empty;

            dom.Load(httpStream);
            httpStream.Position = posStream;

            if (dom.GetElementsByTagName(tagToken).Item(0) == null)
            {
                DenyAccess(app, TipoError.Sin_Token);
                return;
            }
            else
            {
                token = dom.GetElementsByTagName(tagToken).Item(0).InnerText;

                if (string.IsNullOrEmpty(token))
                {
                    DenyAccess(app, TipoError.Sin_Token);
                    return;
                }
                else
                {
                    if (!ValidaToken(token))
                    {
                        DenyAccess(app, TipoError.Token_inválido);
                        return;
                    }
                    else
                    {
                        log.Info("Acceso OK - " + app.Request.ServerVariables["HTTP_SOAPACTION"] + " - " + app.Request.UserHostAddress);
                    }
                }
            }
        }

        /// <summary>
        /// Error de acceso denegado
        /// </summary>
        /// <param name="app"></param>
        /// <param name="tipo">
        ///- 1 sin token
        ///- 2 token inválido
        ///- 3 sin SOAPAction
        ///- 4 Sin AppSetting serviceName
        ///- 5 Sin AppSetting serviceToken
        ///</param>
        private void DenyAccess(HttpApplication app, TipoError tipo)
        {
            string tipoError = string.Empty;
            switch (tipo)
            {
                case TipoError.Sin_Token:
                    tipoError = "Sin Token";
                    break;
                case TipoError.Token_inválido:
                    tipoError = "Token inválido";
                    break;
                case TipoError.Sin_SOAPAction:
                    tipoError = "Sin SOAPAction";
                    break;
                case TipoError.Sin_ServiceName:
                    tipoError = "Sin AppSetting serviceName";
                    break;
                case TipoError.Sin_ServiceToken:
                    tipoError = "Sin AppSetting serviceToken";
                    break;
            }

            log.Error("Error de Acceso [" + tipoError + "] - " + app.Request.ServerVariables["HTTP_SOAPACTION"] + " - " + app.Request.UserHostAddress);

            app.Response.StatusCode = 401;
            app.Response.StatusDescription = "Acceso Denegado";

            // Mensaje que se ve en pantalla
            app.Response.Write("401 Acceso Denegado");
            app.CompleteRequest();
        }

        /// <summary>
        /// Valida Token entrante con el del web.config
        /// </summary>
        /// <param name="tokenAuth"></param>
        /// <returns></returns>
        private bool ValidaToken(string tokenAuth)
        {
            if (string.IsNullOrEmpty(tokenAuth))
                return false;

            return tokenAuth.Equals(ServiceToken);
        }

        public void Dispose()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal enum TipoError : byte
    {
        /// <summary>
        /// 
        /// </summary>
        Sin_Token = 1,
        /// <summary>
        /// 
        /// </summary>
        Token_inválido = 2,
        /// <summary>
        /// 
        /// </summary>
        Sin_SOAPAction = 3,
        /// <summary>
        /// 
        /// </summary>
        Sin_ServiceName = 4,
        /// <summary>
        /// 
        /// </summary>
        Sin_ServiceToken = 5
    }
}