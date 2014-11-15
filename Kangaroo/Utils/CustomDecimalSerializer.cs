using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Kangaroo.Utils
{
    public class CustomDecimalSerializer : DecimalSerializer, IBsonSerializationProvider
    {
        private IBsonSerializationOptions _defaultSerializationOptions = new RepresentationSerializationOptions(BsonType.Double);

        public override void Serialize(
            BsonWriter bsonWriter,
            Type nominalType,
            object value,
            IBsonSerializationOptions options)
        {
            if (options == null) options = _defaultSerializationOptions;
            base.Serialize(bsonWriter, nominalType, value, options);
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            return type == typeof(Decimal) ? this : null;
        }
    }
}