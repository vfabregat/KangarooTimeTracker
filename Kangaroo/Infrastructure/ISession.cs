using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Kangaroo.Infrastructure
{
    public interface ISession
    {
        void Add<T>(T item);
        void AddBatch<T>(IEnumerable<T> items);
        void UpdateBatch<T>(IEnumerable<T> items);
        IQueryable<T> GetQueryable<T>();
        void RemoveBatch<T>(IEnumerable<string> ids);

        TOutput Aggregate<T, TOutput>(IEnumerable<BsonDocument> pipeline);
    }
}
