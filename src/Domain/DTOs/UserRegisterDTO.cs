using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserRegisterDTO
    {
        [Display (Name = "Adı")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadı")]
        public string LastName { get; set; }

        [Display(Name = "E-Posta Adresi")]

        public string EMail { get; set; }

        [Display(Name = "Parola")]
        [DataType(DataType.Password)] //inputa type=password demek yerine doğrudan buradan da data annotations ile belirtebilirsin. 
                                     //Html de direkt tipi password alıyor o zaman.

        public string Password { get; set; }
    }
}
