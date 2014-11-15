using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Models;
using Kangaroo.Utils;
using MongoDB.Bson;

namespace Kangaroo.Infrastructure.Queries.Reports
{
    public class ProjectsHoursReport : IQuery<DateTime, DateTime, List<ProjectHoursModel>>
    {
        ISession session;
        public ProjectsHoursReport(ISession session)
        {
            this.session = session;
        }

        public List<ProjectHoursModel> Run(DateTime startDate, DateTime endDate)
        {
            var pipeline = new List<BsonDocument>();
            var match = new BsonDocument 
                { 
                    { 
                        "$match", 
                        new BsonDocument 
                            { 
                                 {"Date", new BsonDocument 
                                                   { 
                                                       { 
                                                           MongoOperators.GreaterThanOrEqual, startDate
                                                       } ,
                                                       { 
                                                           MongoOperators.LowerThanOrEqual, endDate
                                                       } 
                                                   }} 
                            } 
                    } 
                };

            var group = new BsonDocument 
                { 
                    { 
                        "$group", 
                        new BsonDocument 
                            { 
                                {
                                    "_id", "$Project" 
                                },
                                { 
                                    "Hours", new BsonDocument 
                                                 { 
                                                     { 
                                                         "$sum", "$Quantity"
                                                     } 
                                                 } 
                                } 
                            } 
                  } 
                };

            pipeline.Add(match);
            pipeline.Add(group);

            var result = session.Aggregate<TimeEntry, List<ProjectHoursModel>>(pipeline);


            return result.OrderByDescending(r => r.Hours).ToList();
        }
    }
}