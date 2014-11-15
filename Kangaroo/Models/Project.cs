using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace Kangaroo.Models
{
    public class Project
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }

    public class ProjectAssignation
    {
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
    }
}