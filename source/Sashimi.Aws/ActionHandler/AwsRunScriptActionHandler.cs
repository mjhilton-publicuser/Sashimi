﻿using System;
using Octopus.Diagnostics;
using Sashimi.Server.Contracts;
using Sashimi.Server.Contracts.ActionHandlers;
using Sashimi.Server.Contracts.Calamari;

namespace Sashimi.Aws.ActionHandler
{
    /// <summary>
    /// The action handler that prepares a Calamari script execution with
    /// the path set to include the AWS CLI and having AWS credentials
    /// set in the common environment variable paths.
    /// </summary>
    public class AwsRunScriptActionHandler : IActionHandler
    {
        readonly ILog log;

        public AwsRunScriptActionHandler(ILog log)
        {
            this.log = log;
        }

        public string Id => AwsActionTypes.RunScript;
        public string Name => "Run an AWS CLI Script";
        public string Description => "Runs a custom script with AWS credentials and the AWS CLI provided";
        public string Keywords => string.Empty;
        public bool ShowInStepTemplatePickerUI => true;
        public bool WhenInAChildStepRunInTheContextOfTheTargetMachine => true;
        public bool CanRunOnDeploymentTarget => false;
        public ActionHandlerCategory[] Categories => new[] {ActionHandlerCategory.BuiltInStep, AwsConstants.AwsActionHandlerCategory, ActionHandlerCategory.Script,};

        public IActionHandlerResult Execute(IActionHandlerContext context)
        {
            if (context.Variables.GetFlag(KnownVariables.UseRawScript))
                throw new InvalidOperationException("AWS steps do not support raw scripts");

            var builder = context
                .CalamariCommand(CalamariFlavour.CalamariAws, KnownCalamariCommands.RunScript)
                .WithArgument("extensions", CalamariExtensions.Aws)
                .WithVariable(KnownVariables.Action.Script.Syntax, context.Variables.Get(KnownVariables.Action.Script.Syntax, ScriptSyntax.PowerShell.ToString()))
                .WithAwsTools(context, log);

            var isInPackage = KnownVariableValues.Action.Script.ScriptSource.Package.Equals(context.Variables.Get(KnownVariables.Action.Script.ScriptSource), StringComparison.OrdinalIgnoreCase);
            if (isInPackage)
            {
                builder.WithStagedPackageArgument();
            }

            return builder.Execute();
        }
    }
}