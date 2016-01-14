namespace KitKare.Server.ViewModels
{
    using System;

    using KitKare.Data.Models;
    using KitKare.Server.Common.Mapping;

    public class FeedingViewModel : IMapFrom<Feeding>
    {
        public int Quantity { get; set; }

        public DateTime Time { get; set; }

        public string UserId { get; set; }
    }
}