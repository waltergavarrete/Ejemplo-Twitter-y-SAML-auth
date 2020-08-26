using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static WebApplication2.Saml;

namespace WebApplication2
{
    public partial class callbacksaml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. TODO: specify the certificate that your SAML provider gave you
            string samlCertificate = @"-----BEGIN CERTIFICATE-----
BLAHBLAHBLAHBLAHBLAHBLAHBLAHBLAHBLAHBLAHBLAHBLAH123543==
-----END CERTIFICATE-----";

            // 2. Let's read the data - SAML providers usually POST it into the "SAMLResponse" var
            Saml.Response samlResponse = new Response(samlCertificate, Request.Form["SAMLResponse"]);
            string username = "";
            // 3. We're done!
            if (samlResponse.IsValid())
                username = samlResponse.GetNameID();
        }
    }
}