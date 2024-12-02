﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Entities
{
    public class Client:BaseEntity
    {
        public string Name { get; set; }
        public string PictureUrl{ get; set; }

        public List<Portofolio>? portofolios { get; set; }

    }
}
