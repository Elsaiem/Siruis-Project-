using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Entities
{
    public class TeamMember:BaseEntity
    {
        public string TeamName { get; set; }
        public string JobTitle { get; set; }
        public List<TaskMember>? tasks { get; set; }


    }
}
