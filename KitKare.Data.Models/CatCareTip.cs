namespace KitKare.Data.Models
{
    using KitKare.Data.Models.Common;

    public class CatCareTip : AuditInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
