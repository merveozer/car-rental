﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
   public class RentalPeriod
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength (50)]
        public string Name { get; set; }
    }
}
