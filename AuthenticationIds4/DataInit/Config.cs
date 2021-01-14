using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationIds4.DataInit
{
    public static class Config
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        /// <summary>
        /// Api范围
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
            };

        /// <summary>
        /// 客户端信息
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },// 客户端模式
                 
                new Client
                {
                    ClientId = "WinformclientId",
                    ClientName = "WinformclientName",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("AAAAAAEF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    AllowedScopes = { "scope1" }
                } //密码模式

            };
    }
}
