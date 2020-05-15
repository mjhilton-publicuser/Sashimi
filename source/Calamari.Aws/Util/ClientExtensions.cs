﻿using Amazon.CloudFormation;
using Amazon.IdentityManagement;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Calamari.CloudAccounts;
using Octopus.CoreUtilities.Extensions;

namespace Calamari.Aws.Util
{
    
    
    public static class ClientExtensions
    {
        public static TConfig AsClientConfig<TConfig>(this AwsEnvironmentGeneration environment)
            where TConfig : ClientConfig, new()
        {
            return new TConfig().Tee(x =>
            {
                x.RegionEndpoint = environment.AwsRegion;
            });
        }}

    public static class ClientHelpers
    {
        public static AmazonIdentityManagementServiceClient CreateIdentityManagementServiceClient(
            AwsEnvironmentGeneration environment)
        {
            return new AmazonIdentityManagementServiceClient(environment.AwsCredentials, environment.AsClientConfig<AmazonIdentityManagementServiceConfig>());
        }
        public static AmazonSecurityTokenServiceClient CreateSecurityTokenServiceClient(
            AwsEnvironmentGeneration environment)
        {
            return new AmazonSecurityTokenServiceClient(environment.AwsCredentials, environment.AsClientConfig<AmazonSecurityTokenServiceConfig>());
        }

        public static IAmazonCloudFormation CreateCloudFormationClient(AwsEnvironmentGeneration environment)
        {
            return new AmazonCloudFormationClient(environment.AwsCredentials, 
                environment.AsClientConfig<AmazonCloudFormationConfig>());
        }
    }
}
