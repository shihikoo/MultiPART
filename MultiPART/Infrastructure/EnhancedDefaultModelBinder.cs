using System;
using System.Web.Mvc;

namespace MultiPART.Infrastructure
{
    public class EnhancedDefaultModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            if (bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName + ".ModelType"))
            {
                //get the model type
                var typeName = (string)bindingContext.ValueProvider
                    .GetValue(bindingContext.ModelName + ".ModelType").ConvertTo(typeof(string));
                var modelType = Type.GetType(typeName);
                //tell the binder to use it
                bindingContext.ModelMetadata = ModelMetadataProviders.Current
                    .GetMetadataForType(null, modelType);
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}