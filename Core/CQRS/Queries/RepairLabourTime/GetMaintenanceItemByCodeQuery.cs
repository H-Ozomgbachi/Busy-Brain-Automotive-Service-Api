﻿using Common.Core.Models.RepairLabourTimeModels;
using MediatR;

namespace Common.Core.CQRS.Queries.RepairLabourTime
{
    public class GetMaintenanceItemByCodeQuery : IRequest<MaintenanceItemModel>
    {
        public string Code { get; set; }
    }
}
