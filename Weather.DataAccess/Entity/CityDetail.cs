using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.DataAccess.Entity
{
    public class CityDetail
    {
        [Key, ForeignKey("City")]
        public int Id { get; set; }

        public int ViewCount { get; set; }

        public DateTime? LastVisitedDate { get; set; }

        public virtual City City { get; set; }
    }
}