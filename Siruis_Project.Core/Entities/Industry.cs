using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Entities
{
    public class Industry:BaseEntity
    {
        public string Indust_Name { get; set; }

        public List<Portofolio>? Portofolios { get; set; }



    }
}
