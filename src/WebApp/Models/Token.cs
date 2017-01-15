﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Code = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            AuthorizationToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            RefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            Issued = false;
            Refreshed = false;
        }

        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string AuthorizationToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime AuthTokenExpiration { get; set; }

        public string RedirectedUri { get; set; }

        public bool Issued { get; set; }

        public bool Refreshed { get; set; }

        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}