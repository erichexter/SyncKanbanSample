using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication22
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<List> Lists { get; set; } 
    }
}