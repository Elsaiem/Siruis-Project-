﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.ClientDto
{
    public record ClientUpdateReq
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string PictureUrl { get; init; }
    }

}
