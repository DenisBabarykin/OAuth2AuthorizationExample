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
                result = context.Clients.Any(c => c.ClientId == clientId) 
                    && context.Users.Any(u => u.Login == userLogin && u.PassHash == HashWorker.GetHashString(pass));
            }
            return result;
        } 

        public void CreateToken(string clientId, string redirectUri, string userLogin)
        {
            using (var context = new UserContext())
            {
                context.Tokens.Add(Token.CreateNewToken(redirectUri, clientId, userLogin));
            }
        }
    }
}
