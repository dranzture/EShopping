﻿namespace OrchestratorService.Core.Dtos.Inventory;

public class CreatedInventoryResultDto : BaseDto<Guid>
{
    public string Message { get; set; } 
}