using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace SSO
{
    public class IdentityServer4Config
    {
        /// <summary>
        /// 定义可获取的用户信息的资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
            };
        }

        /// <summary>
        /// 定义 api 资源的逻辑名称及简要描述
        /// </summary>
        /// <returns>api 资源声明</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                // api name 很重要，用来指定资源的逻辑名称，用于定义资源
                new ApiResource("WebSite1", "WebSite1 描述"),
                new ApiResource("WebSite2", "WebSite2 描述"),
            };
        }

        /// <summary>
        /// 定义客户端
        /// </summary>
        /// <returns>用户声明</returns>
        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    // 客户端 id 很重要，用于定义用户，可以是任何有意义的字符串
                    ClientId = "WebSite1",
                    Description = "WebSite1 描述",

                    // 设置授权类型
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false, // 跳过同意页面

                    ClientSecrets =
                    {
                        // 设置用于验证的密码，并进行简单的加密
                        new Secret("secret".Sha256()),
                    },

                    RedirectUris = { "http://localhost:5001/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5001/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true, // offline_access，增加授权访问

                    // AccessToken 过期时间
                    //AccessTokenLifetime = 3600,

                    // 设置授权成功后，可访问的资源，里面的值必须是在 IdentityResource 或  ApiResource 定义了的资源名称
                    AllowedScopes =
                    {
                        "WebSite1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                    },
                },
                new Client
                {
                    // 客户端 id 很重要，用于定义用户，可以是任何有意义的字符串
                    ClientId = "WebSite2",
                    Description = "WebSite2 描述",

                    // 设置授权类型
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false, // 跳过同意页面

                    ClientSecrets =
                    {
                        // 设置用于验证的密码，并进行简单的加密
                        new Secret("secret".Sha256()),
                    },

                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true, // offline_access，增加授权访问

                    // AccessToken 过期时间
                    //AccessTokenLifetime = 3600,

                    // 设置授权成功后，可访问的资源，里面的值必须是在 IdentityResource 或  ApiResource 定义了的资源名称
                    AllowedScopes =
                    {
                        "WebSite2",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                    },
                },
            };
        }

        /// <summary>
        /// 定义用户
        /// </summary>
        /// <returns>用户</returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.PhoneNumber, "123456789"),
                        new Claim(JwtClaimTypes.PhoneNumberVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.PhoneNumber, "123456789"),
                        new Claim(JwtClaimTypes.PhoneNumberVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")
                    }
                }
            };
        }
    }
}
