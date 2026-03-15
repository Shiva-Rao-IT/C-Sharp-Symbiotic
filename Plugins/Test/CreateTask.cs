using Microsoft.Xrm.Sdk;
using System;
using System.Text.RegularExpressions;

namespace test
{
    public class CreateTask : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            var tracing = (ITracingService)
                serviceProvider.GetService(typeof(ITracingService));

            if (context.MessageName != "Create" ||
                context.PrimaryEntityName != "account")
                return;

            try
            {
                var serviceFactory = (IOrganizationServiceFactory)
                    serviceProvider.GetService(typeof(IOrganizationServiceFactory));

                var service = serviceFactory.CreateOrganizationService(context.UserId);

                Entity task = new Entity("task");
                task["subject"] = "Send e-mail to the new customer.";
                task["description"] = "Follow up with the customer.";
                task["scheduledstart"] = DateTime.Now.AddDays(7);
                task["scheduledend"] = DateTime.Now.AddDays(7);
                task["ownerid"] =
                    new EntityReference("systemuser", context.UserId);

                task["regardingobjectid"] =
                    new EntityReference("account", context.PrimaryEntityId);

                service.Create(task);
            }
            catch (Exception ex)
            {
                tracing.Trace("CreateTask error: {0}", ex.ToString());
                throw;
            }
        }
    }
        public class AccountMobilePreValidationPlugin : IPlugin
        {
            public void Execute(IServiceProvider serviceProvider)
            {
                var context = (IPluginExecutionContext)
                    serviceProvider.GetService(typeof(IPluginExecutionContext));

                // Run only on Account Create
                if (context.MessageName != "Create" ||
                    context.PrimaryEntityName != "account")
                    return;

                if (!context.InputParameters.Contains("Target") ||
                    !(context.InputParameters["Target"] is Entity))
                    return;

                Entity account = (Entity)context.InputParameters["Target"];

                // 🔴 Validation 1: Mobile number must exist
                if (!account.Contains("telephone1"))
                {
                    throw new InvalidPluginExecutionException(
                        "Mobile number is mandatory to create an Account."
                    );
                }

                string mobile = account["telephone1"].ToString();

                // 🔴 Validation 2: Mobile number format (10 digits)
                if (!IsValidMobile(mobile))
                {
                    throw new InvalidPluginExecutionException(
                        "Please enter a valid 10-digit mobile number."
                    );
                }
            }

            private bool IsValidMobile(string mobile)
            {
                // Accepts only 10 digits
                return Regex.IsMatch(mobile, @"^[0-9]{10}$");
            }
        }


    public class AccountPreOperationPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Run only on Account Create or Update
            if ((context.MessageName != "Create" &&
                 context.MessageName != "Update") ||
                context.PrimaryEntityName != "account")
                return;

            if (!context.InputParameters.Contains("Target") ||
                !(context.InputParameters["Target"] is Entity))
                return;

            Entity account = (Entity)context.InputParameters["Target"];

            // 🔹 Set default category if not provided
            if (!account.Contains("accountcategorycode"))
            {
                // Example OptionSet value (Customer = 1)
                account["accountcategorycode"] =
                    new OptionSetValue(1);
            }
        }
    }
}
