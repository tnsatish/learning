using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace TestNetCore
{
    class ValidateAzureToken
    {
        public static void Test()
        {
            string azureToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJleHAiOjE1OTg2MDAzODcsIm5iZiI6MTU5ODU5Njc4NywidmVyIjoiMS4wIiwiaXNzIjoiaHR0cHM6Ly94b21lYjJjLmIyY2xvZ2luLmNvbS9jMmMzYzgzMy1jNTk5LTQ4NzMtYTRmMi1jMTVlNjAzMTg3YTkvdjIuMC8iLCJzdWIiOiJlYmY4MmUxOS1mNGEwLTRlYzUtOWNhMi04OGJiZTk1OWE2YmIiLCJhdWQiOiJjZTVlZDAxYy02ZTg3LTQyMTktYTVhOS04M2M5OWIwYzNjNDciLCJub25jZSI6IjlhYWY1NmE0LWUyOTktNGU0ZS04NGVmLWUzMTMzZDc1M2RlNyIsImlhdCI6MTU5ODU5Njc4NywiYXV0aF90aW1lIjoxNTk4NTk2Nzg3LCJvaWQiOiJlYmY4MmUxOS1mNGEwLTRlYzUtOWNhMi04OGJiZTk1OWE2YmIiLCJuYW1lIjoidW5rbm93biIsImVtYWlscyI6WyJuYWdhc2F0aXNoLnRpcnV2ZWVkdWxhQHhvbWUuY29tIl0sInRmcCI6IkIyQ18xX3hvbWVsb2dpeF9zaWdudXBfc2lnbmluIn0.aJL8Tu6w03SnJIceL1FA69LjX871jZlmPbbejLPAqk29yQJZEFCeEud-5bOZVrQ8l5zBiA78mj14ZOz41nXMSbFppoZzJL1Ji-XA5RpwBU9IfIo3ioIblY8oCNsC74m30Fk5OOgXjjVsCOa4SMxs1gQuaMNERtw07fYSRSDclM5zpJsVxSp6lytIlbpqeH7KS8n_OpBVmfKCuPazhD0PdpdLxtOQTwAJvkZec8IVTN0SBNniAq3gVnq8MWsl6OckfcOGMLxCEI5oTsX4fQaS7XSWO-z63QiwQqmh2HTna7059evrlZ_7fg9H6f5Xli9l5_deSK1b4gVIPM-TlYgMqQ";
            var result = ValidateToken(azureToken);
            Console.WriteLine(result);
        }

        private static bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            var readToken = (JwtSecurityToken)tokenHandler.ReadToken(authToken);

            foreach(var claim in readToken.Claims)
            {
                Console.WriteLine(claim.Type + " - " + claim.Value);
            }

            Console.WriteLine(readToken.Id);
            Console.WriteLine(readToken.Issuer);
            Console.WriteLine(readToken.ValidFrom);
            Console.WriteLine(readToken.ValidTo);
            Console.WriteLine(readToken.ToString());
            Console.WriteLine(readToken.SecurityKey?.KeyId);
            //Console.WriteLine(readToken.SigningKey?.KeyId);

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            foreach (var claim in principal.Claims)
            {
                Console.WriteLine(claim.Type + " - " + claim.Value);
            }
            return true;
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            string key = "{\"kid\":\"X5eXk4xyojNFum1kl2Ytv8dlNP4-c57dO6QGTVBwaNk\",\"nbf\":1493763266,\"use\":\"sig\",\"kty\":\"RSA\",\"e\":\"AQAB\",\"n\":\"tVKUtcx_n9rt5afY_2WFNvU6PlFMggCatsZ3l4RjKxH0jgdLq6CScb0P3ZGXYbPzXvmmLiWZizpb-h0qup5jznOvOr-Dhw9908584BSgC83YacjWNqEK3urxhyE2jWjwRm2N95WGgb5mzE5XmZIvkvyXnn7X8dvgFPF5QwIngGsDG8LyHuJWlaDhr_EPLMW4wHvH0zZCuRMARIJmmqiMy3VD4ftq4nS5s8vJL0pVSrkuNojtokp84AtkADCDU_BUhrc2sIgfnvZ03koCQRoZmWiHu86SuJZYkDFstVTVSR0hiXudFlfQ2rOhPlpObmku68lXw-7V-P7jwrQRFfQVXw\"}";

            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //rsa.ImportParameters(
            //  new RSAParameters()
            //  {
            //      Modulus = Convert.FromBase64Url("tVKUtcx_n9rt5afY_2WFNvU6PlFMggCatsZ3l4RjKxH0jgdLq6CScb0P3ZGXYbPzXvmmLiWZizpb-h0qup5jznOvOr-Dhw9908584BSgC83YacjWNqEK3urxhyE2jWjwRm2N95WGgb5mzE5XmZIvkvyXnn7X8dvgFPF5QwIngGsDG8LyHuJWlaDhr_EPLMW4wHvH0zZCuRMARIJmmqiMy3VD4ftq4nS5s8vJL0pVSrkuNojtokp84AtkADCDU_BUhrc2sIgfnvZ03koCQRoZmWiHu86SuJZYkDFstVTVSR0hiXudFlfQ2rOhPlpObmku68lXw-7V-P7jwrQRFfQVXw"),
            //      Exponent = Convert.FromBase64Url("AQAB")
            //  });


            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                //ValidIssuer = "Sample",
                ValidAudience = "ce5ed01c-6e87-4219-a5a9-83c99b0c3c47",
                ValidIssuers = new[] { "https://xomeb2c.b2clogin.com/xomeb2c.onmicrosoft.com/B2C_1_xomelogix_signup_signin/" },
                //ValidAudiences = new[] { "", "" }
                //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // The same key as the one that generate the token
                //IssuerSigningKey = new RsaSecurityKey(rsa)
            };
        }

    }
}
