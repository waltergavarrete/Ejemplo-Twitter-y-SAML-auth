using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static WebApplication2.Saml;

namespace WebApplication2
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var samlEndpoint = "http://saml-provider-that-we-use.com/login/";

            var request = new AuthRequest(
                "http://www.myapp.com", //TODO: put your app's "unique ID" here
                "http://www.myapp.com/SamlConsume" //TODO: put Assertion Consumer URL (where the provider should redirect users after authenticating)
                );
            Response.Redirect(request.GetRedirectUrl(samlEndpoint));
            //redirect the user to the SAML provider
        }
    }
}