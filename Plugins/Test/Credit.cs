using Microsoft.Xrm.Sdk;
using System;

namespace SamplePlugins
{
    public class AccountCreditLimitPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));


            if (context.MessageName != "Update" ||
                context.PrimaryEntityName != "account" ||
                context.Stage != 20)
                return;

            if (!context.InputParameters.Contains("Target") ||
                !(context.InputParameters["Target"] is Entity))
                return;

            if (!context.PreEntityImages.Contains("PreImage"))
                return;

            Entity target = (Entity)context.InputParameters["Target"];
            Entity preImage = context.PreEntityImages["PreImage"];


            decimal oldLimit =
                preImage.GetAttributeValue<Money>("creditlimit")?.Value ?? 0;

  
            decimal newLimit =
                target.GetAttributeValue<Money>("creditlimit")?.Value ?? oldLimit;

            if (newLimit < oldLimit)
            {
                throw new InvalidPluginExecutionException(
                    "Reducing credit limit is not allowed."
                );
            }
        }
    }

    public class AccountCreditLimitPostPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Run only on Account Update (Post-Operation)
            if (context.MessageName != "Update" ||
                context.PrimaryEntityName != "account" ||
                context.Stage != 40)
                return;

            // Ensure Post-Image exists
            if (!context.PostEntityImages.Contains("PostImage"))
                return;

            Entity postImage = context.PostEntityImages["PostImage"];

            decimal creditLimit =
                postImage.GetAttributeValue<Money>("creditlimit")?.Value ?? 0;

            var serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(
                    typeof(IOrganizationServiceFactory));

            var service =
                serviceFactory.CreateOrganizationService(context.UserId);

            // Create Audit Note (Timeline entry)
            Entity note = new Entity("annotation");
            note["subject"] = "Credit Limit Updated";
            note["notetext"] =
                $"Credit limit successfully updated to {creditLimit}.";
            note["objectid"] =
                new EntityReference("account", context.PrimaryEntityId);

            service.Create(note);
        }
    }
}
