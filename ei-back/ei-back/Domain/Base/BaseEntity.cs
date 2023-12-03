using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ei_back.Domain.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime UpdatedAt { get; set; }

    }
}
