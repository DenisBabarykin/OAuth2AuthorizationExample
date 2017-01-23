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
            bool result = true;
            using (var context = new UserContext())
            {
                var passHash = HashWorker.GetHashString(pass);
                result = context.Clients.Any(c => c.ClientId == clientId) 
                    && context.Users.Any(u => u.Login == userLogin && u.PassHash == passHash);
            }
            return result;
        } 

        /// <summary>Creates new token entity in db and returns it's code</summary>
        public string CreateToken(string clientId, string redirectUri, string userLogin)
        {
            string code = "";
            using (var context = new UserContext())
            {
                var token = context.Tokens.Add(Token.CreateNewToken(redirectUri, clientId, userLogin));
                context.SaveChanges();
                code = token.Code;
            }
            return code;
        }
    }
}
