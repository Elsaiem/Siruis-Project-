using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Entities
{
    public class Contact :BaseEntity
    {
        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="please insert correct Email")]

        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }




    }
}
