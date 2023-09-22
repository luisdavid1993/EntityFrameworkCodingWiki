using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    [Table("SubCategories")]
    public class SubCategory
    {
        [Key]
        public int SubCategory_Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
