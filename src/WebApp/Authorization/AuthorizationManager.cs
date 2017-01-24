using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class AuthorizationManager
    {
        public bool CorrectCredentials(string clientId, string userLogin, string pass)
        {
            using (var context = new UserContext())
            {
                var passHash = HashWorker.GetHashString(pass);
                return context.Clients.Any(c => c.ClientId == clientId) 
                    && context.Users.Any(u => u.Login == userLogin && u.PassHash == passHash);
            }
        } 

        /// <summary>Creates new token entity in db and returns it's code</summary>
        public string GetCode(string clientId, string redirectUri, string userLogin)
        {
            using (var context = new UserContext())
            {
                var token = context.Tokens.Add(Token.CreateNewToken(redirectUri, clientId, userLogin));
                context.SaveChanges();
                return token.Code;
            }
        }

        /// <summary>Returns token if all credentials are ok and null if credentials are wrong</summary>
        public Token GetToken(string code, string clientId, string clientSecret)
        {
            using (var context = new UserContext())
            {
                var token = context.Tokens.FirstOrDefault(t => t.Code == code && t.ClientId == clientId && t.Client.ClientSecret == clientSecret);
                if (token != null)
                {
                    token.Issued = true;
                    context.SaveChanges();
                }
                return token;
            }
        }

        public Token RefreshToken(string refreshToken, string clientId, string clientSecret)
        {
            using (var context = new UserContext())
            {
                var token = context.Tokens.FirstOrDefault(t => t.RefreshToken == refreshToken && t.ClientId == clientId && t.Client.ClientSecret == clientSecret);
                if (token != null && !token.Refreshed)
                {
                    token.Refreshed = true;
                    var refreshedToken = context.Tokens.Add(Token.CreateRefreshedToken(token));
                    context.SaveChanges();
                    return refreshedToken;
                }
                else
                    return null;
            }
        }
    }
}
