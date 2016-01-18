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
    using System.Net;
    
    
    [RoutePrefix("api/Device")]
    public class DeviceController : BaseAuthorizationController
    {
        private const int ServoPin = 5;
        private const int LightsPin = 7;
        private const string TeleduinoKey = "2BAAB610021E3E7DFAC34C532F12A540";

        private IRepository<Video> videos;
        private IRepository<Feeding> feedings;

        public DeviceController(IRepository<Video> videos, IRepository<User> users, IRepository<Feeding> feedings)
            : base(users)
        {
            this.videos = videos;
            this.feedings = feedings;
        }
        
        [HttpGet]
        [Route("GiveFood")]
        public IHttpActionResult GiveFood()
        {
            var webClient = new WebClient();

            try
            {
                // Define servo
                webClient.DownloadString(string.Format(
                    "https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=defineServo&servo=0&pin={1}", TeleduinoKey, ServoPin));

                // Set initial servo position
                webClient.DownloadString(
                    string.Format("https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=setServo&servo=0&position=25", TeleduinoKey));

                // Open door
                webClient.DownloadString(
                    string.Format("https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=setServo&servo=0&position=70", TeleduinoKey));

                // Close door
                webClient.DownloadString(
                    string.Format("https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=setServo&servo=0&position=25", TeleduinoKey));

                var feeding = new Feeding
                {
                    Quantity = 200,
                    Time = DateTime.Now,
                    UserId = this.CurrentUserId
                };

                this.feedings.Add(feeding);
                this.feedings.SaveChanges();

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        [Route("LightsAreOn")]
        public IHttpActionResult LightsAreOn()
        {
            var a = this.CurrentUserId;
            var lightsAreOn = this.users
                .All()
                .Where(x => x.Id == this.CurrentUserId)
                .Select(x => new { x.LightsAreOn })
                .FirstOrDefault();

            return this.Ok(lightsAreOn);
        }

        [HttpGet]
        [Route("GiveWater")]
        public IHttpActionResult GiveWater()
        {
            // TODO: make api call to teleduino service

            return this.Ok();
        }
        
        [HttpGet]
        [Route("TurnLightsOn")]
        public IHttpActionResult TurnLightsOn()
        {
            var webClient = new WebClient();

            try
            {
                // Turn lights on
                var responseLight1 = webClient.DownloadString(string.Format(
                    "https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=setDigitalOutput&pin={1}&output=1&expire_time=0&save=1", 
                    TeleduinoKey, 
                    LightsPin));

                var currentUser = this.users
                    .All()
                    .Where(x => x.Id == this.CurrentUserId)
                    .FirstOrDefault();

                currentUser.LightsAreOn = true;
                this.users.SaveChanges();

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        [Route("TurnLightsOff")]
        public IHttpActionResult TurnLightsOff()
        {
            var webClient = new WebClient();

            try
            {
                // Turn lights off
                webClient.DownloadString(string.Format(
                    "https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=setDigitalOutput&pin={1}&output=0&expire_time=0&save=1", 
                    TeleduinoKey, 
                    LightsPin));

                var currentUser = this.users
                    .All()
                    .Where(x => x.Id == this.CurrentUserId)
                    .FirstOrDefault();

                currentUser.LightsAreOn = false;
                this.users.SaveChanges();

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("TurnCameraOn")]
        public HttpResponseMessage TurnCameraOn()
        {
            try
            {
                var video = this.videos.All().FirstOrDefault();

                var videoStream = new VideoStream(video.Data);

                var response = Request.CreateResponse();
                var header = new MediaTypeHeaderValue("video/avi");
                var streamContent = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)videoStream.WriteToStream, header);
                response.Content = streamContent;

                return response;
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpGet]
        [Route("TurnCameraOff")]
        public IHttpActionResult TurnCameraOff()
        {

            return this.Ok();
        }
    }
}