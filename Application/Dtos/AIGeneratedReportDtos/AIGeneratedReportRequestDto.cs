﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Application.Dtos.AIGeneratedReportDtos
{
    public class AIGeneratedReportRequestDto
    {
        public int CarId { get; set; }
        public string Prompt { get; set; }
    }

}
