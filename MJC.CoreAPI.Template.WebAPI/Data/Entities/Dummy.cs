using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MJC.CoreAPI.Template.WebAPI.Data.Entities
{
    public class Dummy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(75)]
        public string Name { get; set; }

        /// <summary>
        /// Timestamp UTC date time column, which is written when adding a new record.
        /// </summary>
        [Column("CreatedAt", Order = 6, TypeName = "datetime2(7)")]
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp UTC date time column, which is refreshed when updating a record.
        /// </summary>
        [Column("UpdatedAt", Order = 7, TypeName = "datetime2(7)")]
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
