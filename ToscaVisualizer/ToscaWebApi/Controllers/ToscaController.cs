using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ToscaVisualizer;

namespace ToscaWebApi.Controllers
{
    public class ToscaController : ApiController
    {
        // GET: api/Tosca
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public HttpResponseMessage LoadToscaData()
        {
            var path = @"C:\Users\oren.c\Desktop\ToscaProj\Examples\tosca.zip";

            var toscaJson = Builder.GetToscaZipAsJson(path);

            return new HttpResponseMessage()
            {
                Content = new StringContent(toscaJson, System.Text.Encoding.UTF8, "application/json")
            };
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Upload()
//        public void Upload()
        {


//            if (HttpContext.Current.Request.Files.AllKeys.Any())
//            {
//                // Get the uploaded image from the Files collection
//                var httpPostedFile = HttpContext.Current.Request;
//
//            }

            if (!Request.Content.IsMimeMultipartContent())
            {
                //webErrorLogger.LogError("Unsupported media type.");
                return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "Unsupported media type.");
            }

            // Read the file and form data.
            var provider = new MultipartFormDataMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            // Check if files are on the request.
            if (!provider.FileStreams.Any())
            {
                //webErrorLogger.LogError("No shell in request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No shell in request.");
            }

            if (provider.FileStreams.Count > 1)
            {
                //webErrorLogger.LogError("Single shell publishing allowed.");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Single shell publishing allowed.");
            }

            var fileStreamKeyValue = provider.FileStreams.Single();

           // try
            {
                var toscaJson = Builder.GetToscaStreamAsJson(fileStreamKeyValue.Value);

                return new HttpResponseMessage()
                {
                    Content = new StringContent(toscaJson, System.Text.Encoding.UTF8, "application/json")
                };
            }
            //catch (ToscaBaseException toscaBaseException)
            //{
            //    webErrorLogger.LogError(toscaBaseException.Message);
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, toscaBaseException.Message);
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, "Successfully published: " + fileStreamKeyValue.Key);
        }

        // POST: api/Tosca
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Tosca/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tosca/5
        public void Delete(int id)
        {
        }
    }
}
