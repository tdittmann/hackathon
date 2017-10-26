namespace KIS.AI.Hackathon.Template.Controllers
{
    using KIS.AI.Hackathon.Template.Models;

    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Linq;
    using System.Configuration;
    using KIS.AI.Hackathon.Template.Services;
    using System.Collections.Generic;


    [RoutePrefix("api/Search")]
    public class SearchController : ApiController
    {
        [Route(""), HttpPost]
        public async Task<IHttpActionResult> SearchAsync(SearchQueryModel searchQuery)
        {
            RecognitionService rS = new RecognitionService();

            string requestAddress = searchQuery.Input.ToString();
            
            try
            {
                var result = await rS.GetRecipeName(requestAddress);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }




    }
}