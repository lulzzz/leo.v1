using System;
using System.Collections.Generic;

namespace Leo.Vendors.Plaid.Client
{
    public class Institution
    {
        public Uri AccountLocked { get; set; }

        public Uri AccountSetup { get; set; }

        public IEnumerable<InstitutionField> Fields { get; set; }

        public Uri ForgottenPassword { get; set; }

        public string Id { get; set; }

        public int MyProperty { get; set; }

        public string Name { get; set; }

        public string NameBreak { get; set; }

        public IDictionary<Products, bool> Products { get; set; }

        public string Type { get; set; }

        public string Video { get; set; }
    }
}