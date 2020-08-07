﻿using System;

namespace Sashimi.Server.Contracts.ActionHandlers
{
    public interface IContributeToPackageDeployment
    {
        ActionContributionResult Contribute(DeploymentTargetType deploymentTargetType, IActionHandlerContext context);
    }
}