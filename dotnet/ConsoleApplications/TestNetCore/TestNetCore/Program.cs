using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TestNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //JWTToken.GenerateToken();
            //JWTToken1.ValidateJWTToken();
            //ValidateAzureToken.Test();
            test();
        }

        private static void test()
        {
            string x = "[{\"vendorID\":108686,\"chessScore\":1000,\"transactionRef\":\"\"},{\"vendorID\":106904,\"chessScore\":0,\"transactionRef\":\"306042\"}]";
            List<object> data = JsonConvert.DeserializeObject<List<object>>(x);
            //foreach(var d in data)
            //{
            //    Console.WriteLine(d.vendorID);
            //    Console.WriteLine(d.chessScore);
            //    Console.WriteLine(d.transactionRef);
            //}
            Console.WriteLine(data);
        }
    }
}
