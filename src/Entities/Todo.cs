using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Todo")]
    public class Todo : EntityBase<int>
    {
        [Required]
        [Column("Task")]
        public string Task { get; set; }
    }
}
