using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using Humanizer;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Configuration;
using MongoDB.Bson.Serialization;

namespace Kangaroo.Infrastructure
{
    // http://blog.janjonas.net/2012-04-03/asp_net-mvc_3-mongodb-crud-operation-nosql-database-fluentmongo
    // http://blogs.endjin.com/2010/12/a-step-by-step-guide-to-mongodb-for-net-developers/
    public class Session : ISession
    {
        //public MongoDatabase MyProperty { get; set; }
        MongoDatabase database;
        MongoClient client;
        readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        readonly string databaseName = ConfigurationManager.AppSettings["DatabaseName"];


        public Session()
        {
            client = new MongoClient(connectionString);

            database = client.GetServer().GetDatabase(databaseName);
        }

        public void Add<T>(T item)
        {
            CurrentCollection<T>().Save(item);
        }

        public void AddBatch<T>(IEnumerable<T> items)
        {
            CurrentCollection<T>().InsertBatch(items);
        }

        public void UpdateBatch<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                CurrentCollection<T>().Save(item);
            }
        }

        private string GetCollectionName<T>()
        {
            return typeof(T).Name.Pluralize();
        }

        public TOutput Aggregate<T, TOutput>(IEnumerable<BsonDocument> pipeline)
        {
            var pipe = new AggregateArgs();
            
            pipe.Pipeline = pipeline;

            var result = CurrentCollection<T>().Aggregate(pipe);

            return BsonSerializer.Deserialize<TOutput>(result.ToJson());

        }

        public IEnumerable<BsonDocument> MapReduce<T>(string map, string reduce)
        {
            var arg = new MapReduceArgs();

            arg.MapFunction = map;
            arg.ReduceFunction = reduce;
            //var result = CurrentCollection<T>().MapReduce(map, reduce).GetResults();

            return CurrentCollection<T>().MapReduce(arg).GetResults();//.GetResultsAs<TResult>().ToList();
        }

        public IQueryable<T> GetQueryable<T>()
        {
            return CurrentCollection<T>().AsQueryable<T>();
        }
        private MongoCollection CurrentCollection<T>()
        {
            return database.GetCollection<T>(GetCollectionName<T>());
        }

        public void RemoveBatch<T>(IEnumerable<string> ids)
        {
            var query = Query.In("_id", new BsonArray(ids));
            CurrentCollection<T>().Remove(query);
        }

    }
}