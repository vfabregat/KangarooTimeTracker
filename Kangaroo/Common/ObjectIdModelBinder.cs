using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;

namespace Kangaroo.Common
{
    // http://stackoverflow.com/a/3252783/1322417
    public class ObjectIdModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(ObjectId))
            {
                var key = bindingContext.ModelName;
                var valueProviderResult = bindingContext.ValueProvider.GetValue(key);

                if (valueProviderResult == null ||
                string.IsNullOrEmpty(valueProviderResult.AttemptedValue))
                {
                    return ObjectId.Empty;
                }
                return new ObjectId(valueProviderResult.AttemptedValue);
            }
            else
                return base.BindModel(controllerContext, bindingContext);
        }
    }

}