using System.ComponentModel.DataAnnotations.Schema;

namespace APIMCC.Models
{
    [Table("tb_m_rooms")]
    public class Room : BaseProp
    {
        [Column("name",TypeName ="nvarchar(100)")]
        public string Name { get; set; }

        [Column("floor")]
        public int Floor { get; set; }

        [Column("capacity")]
        public int Capacity { get; set; }

        //Cardinalities

        public ICollection<Booking>? Bookings { get; set; }

    }
}
