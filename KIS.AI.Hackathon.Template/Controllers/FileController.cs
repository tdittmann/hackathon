using KIS.AI.Hackathon.Template.Models;
using KIS.AI.Hackathon.Template.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace KIS.AI.Hackathon.Template.Controllers
{
    [RoutePrefix("api/File")]
    public class FileController : ApiController
    {
        [Route(""), HttpGet]
        public HttpResponseMessage Get()
        {
    
            return null;
        }


        

    }
}
