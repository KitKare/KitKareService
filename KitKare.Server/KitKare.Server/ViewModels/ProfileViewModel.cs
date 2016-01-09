namespace KitKare.Server.ViewModels
{
    using KitKare.Data.Models;
    using KitKare.Server.Common.Mapping;

    public class ProfileViewModel : IMapFrom<User>
    {
        public string Email { get; set; }

        public string Username { get; set; }
    }
}