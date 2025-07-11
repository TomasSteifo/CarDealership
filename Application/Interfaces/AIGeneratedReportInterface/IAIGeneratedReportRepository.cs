﻿using CarDealership.Domain.Models;
using CarDealership.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarDealership.Application.Interfaces.AIGeneratedReportInterface
{
    public interface IAIGeneratedReportRepository
    {
        Task<OperationResult<AIGeneratedReport>> AddAsync(AIGeneratedReport report);
        Task<OperationResult<IEnumerable<AIGeneratedReport>>> GetAllAsync();
        Task<OperationResult<AIGeneratedReport?>> GetByIdAsync(int id);
        Task<OperationResult<bool>> DeleteAsync(int id);

    }
}
