﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Token
    {
        public Token()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        [Index]
        [Column(TypeName = "VARCHAR")]
        [StringLength(300)]
        public string AuthorizationToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime AuthTokenExpiration { get; set; }

        public string RedirectedUri { get; set; }

        public bool Issued { get; set; }

        public bool Active { get; set; }

        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string UserLogin { get; set; }
        public virtual User User { get; set; }

        [NotMapped]
        public bool Expired {
            get
            {
                return DateTime.Now.Subtract(AuthTokenExpiration).TotalSeconds > 0;
            }
        }

        public static Token CreateNewToken(string redirectUri, string clientId, string userId)
        {
            return new Token()
            {
                Code = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                AuthorizationToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                RefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                AuthTokenExpiration = DateTime.Now.AddDays(1),
                Issued = false,
                Active = true,
                RedirectedUri = redirectUri,
                ClientId = clientId,
                UserLogin = userId
            };
        }

        public static Token CreateRefreshedToken(Token previousToken)
        {
            return new Token()
            {
                Code = previousToken.Code,
                AuthorizationToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                RefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                AuthTokenExpiration = DateTime.Now.AddDays(1),
                Issued = true,
                Active = true,
                RedirectedUri = previousToken.RedirectedUri,
                ClientId = previousToken.ClientId,
                UserLogin = previousToken.UserLogin
            };
        }
    }
}
