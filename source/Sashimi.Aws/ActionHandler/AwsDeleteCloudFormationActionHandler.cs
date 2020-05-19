﻿using Calamari.Aws;
using Sashimi.Server.Contracts;
using Sashimi.Server.Contracts.ActionHandlers;
using Sashimi.Server.Contracts.Calamari;

namespace Sashimi.Aws.ActionHandler
{
    /// <summary>
    /// The action handler that prepares a Calamari script execution with
    /// the path set to include the AWS CLI and having AWS credentials
    /// set in the common environment variable paths. It then goes on to
    /// deploy a cloud formation template.
    /// </summary>
    public class AwsDeleteCloudFormationActionHandler : IActionHandler
    {
        public string Id => AwsActionTypes.DeleteCloudFormation;
        public string Name => "Delete an AWS CloudFormation stack";
        public string Description => "Deletes an existing AWS CloudFormation stack";
        public string Keywords => null;
        public bool ShowInStepTemplatePickerUI => true;
        public bool WhenInAChildStepRunInTheContextOfTheTargetMachine => false;
        public bool CanRunOnDeploymentTarget => false;
        public ActionHandlerCategory[] Categories => new[] { ActionHandlerCategory.BuiltInStep, AwsConstants.AwsActionHandlerCategory };

        public IActionHandlerResult Execute(IActionHandlerContext context)
        {
            var builder = context.CalamariCommand(CalamariFlavour.CalamariAws, AwsKnownVariables.Commands.DeleteAwsCloudformation);

            builder.WithArgument("waitForCompletion", context.Variables.GetFlag(AwsSpecialVariables.Action.Aws.WaitForCompletion, false).ToString());
            builder.WithArgument("stackName", context.Variables.Get(AwsSpecialVariables.Action.Aws.CloudFormation.StackName, ""));
            builder.WithArgument("extensions", CalamariExtensions.Aws);

            return builder.Execute();
        }
    }
}