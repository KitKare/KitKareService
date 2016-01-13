namespace KitKare.Server.Controllers
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;

    using KitKare.Server.Common.Streaming;
    using Newtonsoft.Json;
    using System.Text;
    using System.Web;
    using System.IO;
    using System;

    [RoutePrefix("api/Profile")]
    public class ProfileController : ApiController
    {
        [Route("GetVideo")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var mock = new byte[2];

            var video = new VideoStream(mock);

            var response = Request.CreateResponse();
            var header = new MediaTypeHeaderValue("video/avi");
            var streamContent = new PushStreamContent((Action<Stream, HttpContent, System.Net.TransportContext>)video.WriteToStream, header);
            response.Content = streamContent;            

            return response;
        }
    }
}