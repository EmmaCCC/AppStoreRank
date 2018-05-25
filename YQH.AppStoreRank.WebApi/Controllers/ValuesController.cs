using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace YQH.AppStoreRank.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage GetFile()
        {
            try
            {
                var filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/jquery.jpg");
                if (filePath != null)
                {
                    var stream = new FileStream(filePath, FileMode.Open);
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StreamContent(stream);
                    response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpg");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "jquery.jpg"
                    };
                    return response;
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
