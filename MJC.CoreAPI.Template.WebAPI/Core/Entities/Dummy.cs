using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MJC.CoreAPI.Template.WebAPI.Core.Entities
{
    public class Dummy
    {
        public long Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Timestamp UTC date time column, which is written when adding a new record.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp UTC date time column, which is refreshed when updating a record.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
