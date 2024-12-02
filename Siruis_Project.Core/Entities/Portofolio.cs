using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Entities
{
    public class Portofolio:BaseEntity
    {
       
        public int CLient_Id { get; set; }

        public string Img_Url { get; set; }
       
        public int Industry_Id { get; set; } 

        public string Description { get; set; }

        public string? Url { get; set; }//Optional
    
        public Client? client { get; set; }

        public Industry? industry { get; set; }

        public Types type { get; set; } 


    }
}
