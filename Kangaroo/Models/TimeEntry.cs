using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kangaroo.Models
{
    public class TimeEntry
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public decimal? Quantity { get; set; }

        private DateTime date;

        public string UserName { get ; set; }
        public DateTime Date
        {
            get { return date.ToLocalTime(); }
            set { date = value.ToUniversalTime(); }
        }

        public string Project { get; set; }

        

        public TimeEntry()
        {
            //Id = Guid.NewGuid();
        }
    }
}
