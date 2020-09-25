using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("ToDos")]
    public class ToDo : EntityBase<Guid>
    {

        [ForeignKey("UserId")]
        [Required]
        [Column("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [Column("Start")]
        public DateTime Start { get; set; }

        [Required]
        [Column("Task")]
        public string Task { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Forecast")]
        public DateTime? Forecast { get; set; }

        [Column("End")]
        public DateTime? End { get; set; }
    }
}
