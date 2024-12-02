using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Entities
{
    public class TaskMember:BaseEntity
    {
       
        public int TeamMember_Id { get; set; }

        public TeamMember? teamMember { get; set; }
        public string TaskType { get; set; }

        public string TaskDesc { get; set; }

        public DateTime?  DateEnd { get; set; }




    }
}
