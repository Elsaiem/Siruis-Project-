using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.TaskDto
{ 
    public record TaskAddReq
    (
          int TeamMember_Id,
          string TaskType,
          string TaskDesc,
          DateTime? DateEnd
        );
}
