using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kangaroo.Utils
{
    /// <summary>
    /*    /// 
    Name	Description
    $gt	Matches values that are greater than the value specified in the query.
    $gte	Matches values that are greater than or equal to the value specified in the query.
    $in	Matches any of the values that exist in an array specified in the query.
    $lt	Matches values that are less than the value specified in the query.
    $lte	Matches values that are less than or equal to the value specified in the query.
    $ne	Matches all values that are not equal to the value specified in the query.
    $nin	Matches values that do not exist in an array specified to the query.
     * */
    /// </summary>
    public class MongoOperators
    {
        public readonly static string Equal = "$eq";
        public readonly static string GreaterThan = "$gt";
        public readonly static string GreaterThanOrEqual = "$gte";
        public readonly static string LowerThanOrEqual = "$lte";
        public readonly static string Where = "$match";
        public readonly static string Group = "$group";
        public readonly static string Sum = "$sum";
    }
}