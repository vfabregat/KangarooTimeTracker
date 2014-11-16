using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kangaroo.Infrastructure;
using Kangaroo.Models;
using MongoDB.Bson;

namespace Kangaroo.Tests.Infrastructure
{
    public class MockSession : ISession
    {
        public ArrayList collection = new ArrayList();

        public void Add<T>(T item)
        {
            collection.Add(item);
        }

        public void AddBatch<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public void UpdateBatch<T>(IEnumerable<T> items)
        {

            foreach (var item in items)
            {
                collection.Remove(item);
            }

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public IQueryable<T> GetQueryable<T>()
        {
            return collection.Cast<T>().AsQueryable<T>();
        }


        public void RemoveBatch<T>(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<BsonDocument> MapReduce<T>(string map, string reduce)
        {
            throw new NotImplementedException();
        }


        public TOutput Aggregate<T, TOutput>(IEnumerable<BsonDocument> pipeline)
        {
            throw new NotImplementedException();
        }
    }
}
