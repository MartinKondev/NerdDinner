using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.Linq;
namespace NerdDinner.Models
{
    public partial class Dinner
    {
        public bool IsUserRegistered(string userName)
        {
            return RSVPs.Any(r => r.AttendeeName.Equals(userName,
            StringComparison.InvariantCultureIgnoreCase));
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
            {
                throw new ApplicationException("Rule violation prevent saving");
            }
        }

        public bool IsValid
        {
            get
            {
                return (!GetRuleViolations().Any());
            }
        }

        private IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (string.IsNullOrEmpty(Title))
            {
                yield return new RuleViolation("Title required", "Title");
            }
            if (string.IsNullOrEmpty(Description))
            {
                yield return new RuleViolation("Description required", "Description");
            }
            if (string.IsNullOrEmpty(HostedBy))
            {
                yield return new RuleViolation("HostedBy required", "HostedBy");
            }
            if (string.IsNullOrEmpty(Address))
            {
                yield return new RuleViolation("Address required", "Address");
            }
            if (string.IsNullOrEmpty(Country))
            {
                yield return new RuleViolation("Country required", "Country");
            }
            //if (!PhoneValidator.IsValidNumber(ContactPhone, Country))
            //{
            //    yield return new RuleViolation("Phone# does not match country", ContactPhone);
            //}
        }
    }

    internal class PhoneValidator
    {
        //public static bool IsValidNumber(string contactPhone, string country)
        //{
            
        //}
    }

    public class RuleViolation
    {
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }

        public RuleViolation(string errorMesage)
        {
            ErrorMessage = errorMesage;
        }
        public RuleViolation(string errorMesage, string propertyName)
        {
            ErrorMessage = errorMesage;
            PropertyName = propertyName;
        }
    }
}