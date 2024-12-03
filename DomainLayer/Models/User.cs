﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Work> Works { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
