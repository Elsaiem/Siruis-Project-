using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.ContactDto
{
    public record ContactAddReq
    (
        string Name,
        string Email,
        string Subject,
        string Message

        );
}
