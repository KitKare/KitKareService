namespace KitKare.Server.ViewModels
{
    using KitKare.Data.Models;
    using KitKare.Server.Common.Mapping;

    public class ProfileViewModel : IMapFrom<User>
    {
        public string CatName { get; set; }

        public string VetPhone { get; set; }
    }
}