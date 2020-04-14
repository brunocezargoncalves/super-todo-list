using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class EntityBase<T> where T : struct
    {
        [Key]
        [Column("Id")]
        public T Id { get; set; } 
    }
}