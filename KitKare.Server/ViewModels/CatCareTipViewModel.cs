namespace KitKare.Server.ViewModels
{
    using System;

    using KitKare.Data.Models;
    using KitKare.Server.Common.Mapping;

    public class CatCareTipViewModel : IMapFrom<CatCareTip>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}