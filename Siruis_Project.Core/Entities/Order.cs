using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Entities
{
    public class Order:BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }//optional
        public string Govern { get; set; }// requeird
        public bool?  Ads { get; set; }

        public int? Animation { get; set; }

        public bool? Branding { get; set; }

        public bool? CopyWriting { get; set; }
        public DateTime DateCreated { get; set; }= DateTime.Now;

        public int? Design {  get; set; }
        public bool? DigitalCamaign { get; set; }
        
        public bool? Moderation { get; set; }
        public int? ModerationHour { get; set; }
        public bool? Photography { get; set; }

        public Plan plan { get; set; }

        public int? Platform { get; set; }
        public int? Reels { get; set; }

        public Status? status { get; set; }=Status.pending;

        public int? Stories { get; set; }

        public bool? voiceOver { get; set; }










    }

  
}
