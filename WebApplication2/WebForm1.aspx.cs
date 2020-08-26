
using Skybrud.Social.OAuth;
using Skybrud.Social.Twitter;
using Skybrud.Social.Twitter.OAuth;
using Skybrud.Social.Twitter.Objects;
using Skybrud.Social.Twitter.Options.Account;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize a new OAuth client with information about your app
            TwitterOAuthClient oauth = new TwitterOAuthClient
            {
                ConsumerKey = "1RA07SxkMPDrnTf5wx4fNQU9v",
                ConsumerSecret = "zsalItDzjWcAg3KWEQPv7UeVCRwR2neLQD0dihlKZ6WYJDnNA7",
                Callback = "https://b7b326cf8125.ngrok.io/WebForm1.aspx"
            };

            if (Request.QueryString["do"] == "login")
            {

                // Get a request token from the Twitter API
                OAuthRequestToken token = oauth.GetRequestToken();

                // Save the token information to the session so we can grab it later
                Session[token.Token] = token;

                // Redirect the user to the authentication page at Twitter.com
                Response.Redirect(token.AuthorizeUrl);

            }
            else if (Request.QueryString["oauth_token"] != null)
            {

                // Get OAuth parameters from the query string
                string oAuthToken = Request.QueryString["oauth_token"];
                string oAuthVerifier = Request.QueryString["oauth_verifier"];

                // Grab the request token from the session
                OAuthRequestToken token = Session[oAuthToken] as OAuthRequestToken;

                if (token == null)
                {

                   // Content.Text = "<p>An error occured. Timeout?</p>";

                }
                else
                {

                    // Some information for development purposes
               //     Content.Text += "<p>Request Token: " + token.Token + "</p>";
              //      Content.Text += "<p>Request Token Secret: " + token.TokenSecret + "</p>";

                    // Update the OAuth client with information from the request token
                    oauth.Token = token.Token;
                    oauth.TokenSecret = token.TokenSecret;

                    try
                    {

                        // Obtain an access token from the request token and OAuth verifier
                        OAuthAccessToken accessToken = oauth.GetAccessToken(oAuthVerifier);

                        // Update the OAuth client with the access token and access token secret
                        oauth.Token = accessToken.Token;
                        oauth.TokenSecret = accessToken.TokenSecret;

                        TwitterVerifyCrendetialsOptions options = new TwitterVerifyCrendetialsOptions
                        {
                            IncludeEmail = true
                        };
                        // Initialize a new TwitterService instance based on the OAuth client
                        TwitterService service = TwitterService.CreateFromOAuthClient(oauth);

                        // Get information about the authenticated user
                        var user = service.Account.VerifyCredentials(options);
                        

                        // Some information for development purposes
                      //  Content.Text += "<b>Hi " + (user.Name ?? user.ScreenName) + "</b> (" + user.Id + ")<br />";
                      //  Content.Text += "<p>Access Token: " + accessToken.Token + "</p>";
                      //  Content.Text += "<p>Access Token Secret: " + accessToken.TokenSecret + "</p>";

                    }
                    catch (Exception ex)
                    {

                       // Content.Text += "<pre style=\"color: red;\">" + ex.GetType().FullName + ": " + ex.Message + "\r\n\r\n" + ex.StackTrace + "</pre>";

                    }

                }

            }
            else if (Request.QueryString["denied"] != null)
            {

                // Get OAuth parameters from the query string
                string oAuthToken = Request.QueryString["denied"];

                // Remove the request token from the session
                Session.Remove(oAuthToken);

                // Write some output for the user
              ///  Content.Text += "<p>It seems that you cancelled the login!</p>";/
              /// / Content.Text += "<p><a href=\"OAuth.aspx?do=login\">Try again?</a></p>";

            }
            else
            {

             //   Content.Text += "<p><a href=\"OAuth.aspx?do=login\">Login with Twitter</a></p>";

            }

        }
    }
}
