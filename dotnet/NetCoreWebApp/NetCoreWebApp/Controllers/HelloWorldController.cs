using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreWebApp.Controllers
{
    public class HelloWorldController : Controller
    {
        public string Index()
        {
            return "This is default action in HelloWorldController";
        }

        public string Welcome(string name, int numTimes, int id)
        {
            return HtmlEncoder.Default.Encode($"This is welcome action by {name} for {numTimes} with {id}");
        }
    }
}
