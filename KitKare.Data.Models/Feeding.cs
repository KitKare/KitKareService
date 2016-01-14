namespace KitKare.Data.Models
{
    using System;

    public class Feeding
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public DateTime Time { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
