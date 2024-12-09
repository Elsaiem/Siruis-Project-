using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.TaskDto
{
    public record TaskUpdateReq
    {
        public int Id { get; set; }
        public int TeamMember_Id { get; init; }
        public string TaskType { get; init; }
        public string TaskDesc { get; init; }
        public DateTime? DateEnd { get; init; }


    }
}
