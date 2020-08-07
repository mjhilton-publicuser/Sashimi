using System;

namespace Sashimi.Server.Contracts.ActionHandlers
{
    public interface IScriptActionOverride
    {
        ActionOverrideResult ShouldOverride(DeploymentTargetType deploymentTargetType, ScriptSyntax syntax);
    }
}