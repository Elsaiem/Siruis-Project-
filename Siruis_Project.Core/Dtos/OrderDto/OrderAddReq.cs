using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.OrderDto
{
    public record OrderAddReq
        (
           string Name,
           string Phone,
           string? Address ,//optional
           string Govern,// requeird
           Plan plan,
           bool? Ads ,
           int? Animation ,
           bool? Branding ,
           bool? CopyWriting,
           int? Design,
           bool? DigitalCamaign ,
           bool? Moderation,
           int? ModerationHour ,
           bool? Photography ,
           int? Platform,
           int? Reels,
           Status? status ,
           int? Stories ,
           bool? voiceOver

         );
    
}
