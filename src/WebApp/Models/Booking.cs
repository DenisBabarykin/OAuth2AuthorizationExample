﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Booking
    {
        public Booking()
        {
            InitId();
        }

        [Key]
        public Guid Id { get; set; }

        public int RoomId { get; set; }

        public string UserLogin { get; set; }

        public DateTime SettlementDate { get; set; }

        public int DaysCount { get; set; }

        public string AdditionalRequests { get; set; }

        [NotMapped]
        public Bill Bill { get; set; }

        public void InitId()
        {
            if (Id == new Guid())
                Id = Guid.NewGuid();
        }
    }
}
