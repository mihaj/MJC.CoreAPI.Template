using System;

namespace MJC.CoreAPI.Template.WebAPI.Core.Entities
{
    public class Dummy
    {
        public long Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Timestamp UTC date time column, when adding a new record.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp UTC date time column, when updating a record.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
