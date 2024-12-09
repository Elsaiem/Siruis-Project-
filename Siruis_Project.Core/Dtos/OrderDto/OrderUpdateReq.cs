using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.OrderDto
{
    public record OrderUpdateReq
    {
        public int Id { get; init; }
        public  string Name { get; init; }
        public string Phone { get; init; }
        public string? Address { get; init; } //optional
        public string Govern  { get; init; }// requeird
        public Plan plan { get; init; }
        public bool? Ads { get; init; }
        public int? Animation { get; init; }
        public bool? Branding { get; init; }
        public bool? CopyWriting { get; init; }
        public int? Design { get; init; }
        public bool? DigitalCamaign { get; init; }
        public bool? Moderation { get; init; }
        public int? ModerationHour { get; init; }
        public bool? Photography { get; init; }
        public int? Platform { get; init; }
        public int? Reels { get; init; }
        public Status? status{ get; init; }
        public int? Stories{ get; init; }
        public bool? voiceOver{ get; init; }


    }
   
}
