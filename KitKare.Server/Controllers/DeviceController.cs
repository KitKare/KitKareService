using KitKare.Data.Models;
using KitKare.Data.Repositories;
using KitKare.Server.Common.Streaming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace KitKare.Server.Controllers
{
    [RoutePrefix("api/Device")]
    public class DeviceController : ApiController
    {
        private const string TeleduinoKey = "FFCC09A911CC4D6B8AF8E8A0941E1F87";

        private IRepository<Video> videos;

        public DeviceController(IRepository<Video> videos)
        {
            this.videos = videos;
        }
        
        [Route("GiveFood")]
        [HttpGet]
        public IHttpActionResult GiveFood()
        {
            return this.Ok();
        }

        [Route("GiveWater")]
        [HttpGet]
        public IHttpActionResult GiveWater()
        {
            return this.Ok();
        }

        [Route("TurnLightsOn")]
        [HttpGet]
        public IHttpActionResult TurnLightsOn()
        {
            return this.Ok();
        }

        [Route("TurnLightsOff")]
        [HttpGet]
        public IHttpActionResult TurnLightsOff()
        {
            return this.Ok();
        }
        
        [Route("TurnCameraOn")]
        [HttpGet]
        public HttpResponseMessage TurnCameraOn()
        {
            var video = this.videos.All().FirstOrDefault();

            var videoStream = new VideoStream(video.Data);

            var response = Request.CreateResponse();
            var header = new MediaTypeHeaderValue("video/avi");
            var streamContent = new PushStreamContent((Action<Stream, HttpContent, System.Net.TransportContext>)videoStream.WriteToStream, header);
            response.Content = streamContent;

            return response;
        }

        [Route("TurnCameraOff")]
        [HttpGet]
        public IHttpActionResult TurnCameraOff()
        {

            return this.Ok();
        }
    }
}