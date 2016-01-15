namespace KitKare.Server.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;

    using KitKare.Data.Models;
    using KitKare.Data.Repositories;
    using KitKare.Server.Common.Streaming;

   // [Authorize]
    [RoutePrefix("api/Device")]
    public class DeviceController : ApiController
    {
        private const string TeleduinoKey = "FFCC09A911CC4D6B8AF8E8A0941E1F87";

        private IRepository<Video> videos;
        private IRepository<User> users;
        private IRepository<Feeding> feedings;

        private string userId;

        public DeviceController(IRepository<Video> videos, IRepository<User> users, IRepository<Feeding> feedings)
        {
            this.users = users;
            this.videos = videos;
            this.feedings = feedings;

            this.userId = this.users
                .All()
                .Where(x => x.UserName == this.User.Identity.Name)
                .FirstOrDefault()
                ?.Id;
        }
        
        [Route("GiveFood")]
        [HttpGet]
        public IHttpActionResult GiveFood()
        {
            // TODO: make api call to teleduino service
            var feeding = new Feeding
            {
                Quantity = 200,
                Time = DateTime.Now,
                UserId = this.userId
            };

            this.feedings.Add(feeding);
            this.feedings.SaveChanges();

            return this.Ok();
        }

        [Route("GiveWater")]
        [HttpGet]
        public IHttpActionResult GiveWater()
        {
            // TODO: make api call to teleduino service

            return this.Ok();
        }

        [Route("TurnLightsOn")]
        [HttpGet]
        public IHttpActionResult TurnLightsOn()
        {
            // TODO: make api call to teleduino service

            return this.Ok();
        }

        [Route("TurnLightsOff")]
        [HttpGet]
        public IHttpActionResult TurnLightsOff()
        {
            // TODO: make api call to teleduino service

            return this.Ok();
        }
        
        [Route("TurnCameraOn")]
        [HttpGet]
        public HttpResponseMessage TurnCameraOn()
        {
            try
            {
                var video = this.videos.All().FirstOrDefault();

                var videoStream = new VideoStream(video.Data);

                var response = Request.CreateResponse();
                var header = new MediaTypeHeaderValue("video/avi");
                var streamContent = new PushStreamContent((Action<Stream, HttpContent, System.Net.TransportContext>)videoStream.WriteToStream, header);
                response.Content = streamContent;

                return response;
            }
            catch (Exception e)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, e.Message);

            }

        }

        [Route("TurnCameraOff")]
        [HttpGet]
        public IHttpActionResult TurnCameraOff()
        {

            return this.Ok();
        }
    }
}