using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Models;
using Kangaroo.Utils;
using MongoDB.Bson;

namespace Kangaroo.Infrastructure.Queries.Reports
{

    public class ProjectsHoursByUserReport : IQuery<List<ProjectHoursByUserModel>>
    {
        ISession session;
        public ProjectsHoursByUserReport(ISession session)
        {
            this.session = session;
        }

        public List<ProjectHoursByUserModel> Run()
        {
            DateTime today = DateTime.Today;
            var daysInCurrentMonth = DateTime.DaysInMonth(today.Year, today.Month);
            DateTime endOfMonth = new DateTime(today.Year, today.Month, daysInCurrentMonth);

            var pipeline = new List<BsonDocument>();
            var match = new BsonDocument 
                { 
                    { 
                        MongoOperators.Where, 
                        new BsonDocument 
                            { 
                                 {"Date", new BsonDocument 
                                                   { 
                                                       { 
                                                           MongoOperators.GreaterThanOrEqual, endOfMonth.AddDays(-daysInCurrentMonth)
                                                       } ,
                                                       { 
                                                           MongoOperators.LowerThanOrEqual, endOfMonth
                                                       } 
                                                   }} 
                            } 
                    } 
                };

            var group = new BsonDocument 
                { 
                    { MongoOperators.Group, 
                        new BsonDocument 
                            { 
                                {"_id", new BsonDocument 
                                             { 
                                                 { "Project","$Project" }, 
                                                 { "UserName","$UserName" }, 
                                             }
                                },
                                { 
                                    "Hours", new BsonDocument 
                                                 { 
                                                     { 
                                                         MongoOperators.Sum, "$Quantity"
                                                     } 
                                                 } 
                                } 
                            } 
                  } 
                };
            var project = new BsonDocument 
                { 
                    { 
                        "$project", 
                        new BsonDocument 
                            { 
                                {"_id", 0}, 
                                {"UserName","$_id.UserName"}, 
                                {"Project", "$_id.Project"}, 
                                {"Hours", "$Hours"}, 
                            } 
                    } 
                };

            pipeline.Add(match);
            pipeline.Add(group);
            pipeline.Add(project);

            var result = session.Aggregate<TimeEntry, List<ProjectHoursByUserModel>>(pipeline);


            return result.OrderByDescending(r => r.Hours).ToList();
        }
    }
}