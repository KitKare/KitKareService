﻿namespace KitKare.Server.Controllers
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
    using Newtonsoft.Json;    // [Authorize]
    [RoutePrefix("api/Device")]
    public class DeviceController : ApiController
    {
        private const int ServoPin = 9;
        private const string TeleduinoKey = "2BAAB610021E3E7DFAC34C532F12A540";

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
                    UserId = this.userId
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
            var lightsAreOn = this.users
                .All()
                .Where(x => x.Id == this.userId)
                .Select(x => new { LightsAreOn = x.LightsAreOn })
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
                    "https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=setDigitalOutput&pin={1}&output=1&expire_time=0&save=1", TeleduinoKey, 7));

                webClient.DownloadString(string.Format(
                    "https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=setDigitalOutput&pin={1}&output=1&expire_time=0&save=1", TeleduinoKey, 6));

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
                    "https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=setDigitalOutput&pin={1}&output=0&expire_time=0&save=1", TeleduinoKey, 7));

                webClient.DownloadString(string.Format(
                    "https://us01.proxy.teleduino.org/api/1.0/328.php?k={0}&r=setDigitalOutput&pin={1}&output=0&expire_time=0&save=1", TeleduinoKey, 6));

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        
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
                var streamContent = new PushStreamContent((Action<Stream, HttpContent, System.Net.TransportContext>)videoStream.WriteToStream, header);
                response.Content = streamContent;

                return response;
            }
            catch (Exception e)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, e.Message);
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