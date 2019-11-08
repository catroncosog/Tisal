using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services.Protocols;

namespace WebService1
{
    /// <summary>
    /// Clase que representa al objeto SoapHeader del WS Seguro.
    /// </summary>
    public class AuthenticationHeader : SoapHeader
    {
        /// <summary>
        /// Token de seguridad.
        /// </summary>
        public string Token { get; set; }
    }
}