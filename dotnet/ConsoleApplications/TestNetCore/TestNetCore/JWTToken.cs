using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace TestNetCore
{
    class JWTToken
    {
        public static RSAParameters GetRandomKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    return rsa.ExportParameters(true);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static void GenerateToken()
        {
            var handler = new JwtSecurityTokenHandler();

            //create symmetrickey
            //var buffer = new byte[64];
            //using (var random = new RNGCryptoServiceProvider())
            //{
            //    random.GetBytes(buffer);
            //}
            //var secretString = Convert.ToBase64String(buffer);

            RSAParameters keyParams = GetRandomKey();
            Console.WriteLine(JsonSerializer.Serialize(keyParams));

            RSAToken rsaToken = new RSAToken()
            {
                D = Convert.ToBase64String(keyParams.D),
                DP = Convert.ToBase64String(keyParams.DP),
                DQ = Convert.ToBase64String(keyParams.DQ),
                Exponent = Convert.ToBase64String(keyParams.Exponent),
                InverseQ = Convert.ToBase64String(keyParams.InverseQ),
                Modulus = Convert.ToBase64String(keyParams.Modulus),
                P = Convert.ToBase64String(keyParams.P),
                Q = Convert.ToBase64String(keyParams.Q)
            };
            Console.WriteLine(JsonSerializer.Serialize(rsaToken));
            //RSA rsa = RSA.Create();


            // Create the key, and a set of token options to record signing credentials 
            // using that key, along with the other parameters we will need in the 
            // token controlller.
            var key = new RsaSecurityKey(keyParams);

            //var signingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha512Digest);
            var signingCredential = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
            //create jwt
            var token = handler.CreateJwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                expires: DateTime.UtcNow.AddSeconds(10),
                subject: new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, "User"),
                    new Claim("Permissions", "Approve,Admin")
                }),
                signingCredentials: signingCredential);


            //validate jwt
            var tokenString = handler.WriteToken(token);
            SecurityToken validatedToken;
            var param = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.FromMinutes(1),
                ValidIssuer = "issuer",
                ValidAudience = "audience",
                IssuerSigningKey = key,
            };
            var claims = handler.ValidateToken(tokenString, param, out validatedToken);
            Console.WriteLine(claims);

            foreach(var claim in claims.Claims)
            {
                Console.WriteLine(claim.Value);
            }

        }
    }
}
