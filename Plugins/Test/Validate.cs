using Microsoft.Xrm.Sdk;
using System;
using System.Text.RegularExpressions;

namespace validation
{
    public class AccountPreValidationPlugin : IPlugin
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

            // 🔴 Validation 1: Email must exist
            if (!account.Contains("emailaddress1"))
            {
                throw new InvalidPluginExecutionException(
                    "Email address is mandatory to create an Account."
                );
            }

            string email = account["emailaddress1"].ToString();

            // 🔴 Validation 2: Email format check
            if (!IsValidEmail(email))
            {
                throw new InvalidPluginExecutionException(
                    "Please enter a valid email address."
                );
            }
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
            );
        }
    }
}
