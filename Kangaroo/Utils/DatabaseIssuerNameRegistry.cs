using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;
using Kangaroo.Infrastructure;
using Kangaroo.Models;

namespace Kangaroo.Utils
{
    public class DatabaseIssuerNameRegistry : ValidatingIssuerNameRegistry
    {
        static Session session = new Session();

        public DatabaseIssuerNameRegistry()
        {
            session = new Session();
        }
        public static bool ContainsTenant(string tenantId)
        {
            return session.GetQueryable<Tenant>()
                .Where(tenant => tenant.Id == tenantId)
                .Any();
        }

        public static bool ContainsKey(string thumbprint)
        {
            return session.GetQueryable<IssuingAuthorityKey>()
                .Where(key => key.Id == thumbprint)
                .Any();
        }

        public static void RefreshKeys(string metadataLocation)
        {
            IssuingAuthority issuingAuthority = ValidatingIssuerNameRegistry.GetIssuingAuthority(metadataLocation);

            bool newKeys = false;
            bool refreshTenant = false;
            foreach (string thumbprint in issuingAuthority.Thumbprints)
            {
                if (!ContainsKey(thumbprint))
                {
                    newKeys = true;
                    refreshTenant = true;
                    break;
                }
            }

            foreach (string issuer in issuingAuthority.Issuers)
            {
                if (!ContainsTenant(GetIssuerId(issuer)))
                {
                    refreshTenant = true;
                    break;
                }
            }

            if (newKeys || refreshTenant)
            {

                if (newKeys)
                {
                    session.RemoveBatch<IssuingAuthorityKey>(session.GetQueryable<IssuingAuthorityKey>().Select(i => i.Id).ToList());
                    foreach (string thumbprint in issuingAuthority.Thumbprints)
                    {
                        session.Add(new IssuingAuthorityKey { Id = thumbprint });
                    }
                }

                if (refreshTenant)
                {
                    foreach (string issuer in issuingAuthority.Issuers)
                    {
                        string issuerId = GetIssuerId(issuer);
                        if (!ContainsTenant(issuerId))
                        {
                            session.Add(new Tenant { Id = issuerId });
                        }
                    }
                }
            }
        }

        private static string GetIssuerId(string issuer)
        {
            return issuer.TrimEnd('/').Split('/').Last();
        }

        protected override bool IsThumbprintValid(string thumbprint, string issuer)
        {
            return ContainsTenant(GetIssuerId(issuer))
                && ContainsKey(thumbprint);
        }
    }
}
