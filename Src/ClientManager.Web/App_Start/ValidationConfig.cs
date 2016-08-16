using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Web.Mvc;
using Resources;

namespace ClientManager.Web
{
	public class ValidationConfig
	{
		public static void Config()
		{
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "ClientManagerGlobalResources";
            DefaultModelBinder.ResourceClassKey = "ClientManagerGlobalResources";
            
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(RequiredAttribute),
                typeof(ResourseBaseRequiredAttributeAdapter));

            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(RangeAttribute),
                typeof(ResourseBaseRangeAttributeAdapter));

            ModelMetadataProviders.Current = new LocalizedDataAnnotationsModelMetadataProvider();
		}
	}

    public class ResourseBaseRequiredAttributeAdapter : RequiredAttributeAdapter
    {
        public ResourseBaseRequiredAttributeAdapter(ModelMetadata metadata,
                                          ControllerContext context,
                                          RequiredAttribute attribute)
            : base(metadata, context, attribute)
        {
            attribute.ErrorMessageResourceType = typeof(ClientManagerGlobalResources);
            attribute.ErrorMessageResourceName = "Required";
        }
    }

    public class ResourseBaseRangeAttributeAdapter : RangeAttributeAdapter
    {
        public ResourseBaseRangeAttributeAdapter(ModelMetadata metadata,
                                          ControllerContext context,
                                          RangeAttribute attribute)
            : base(metadata, context, attribute)
        {
            attribute.ErrorMessageResourceType = typeof(ClientManagerGlobalResources);
            attribute.ErrorMessageResourceName = "Range";
        }
    }
    public class LocalizedDataAnnotationsModelMetadataProvider :  DataAnnotationsModelMetadataProvider
     {
         protected override ModelMetadata CreateMetadata(
             IEnumerable<Attribute> attributes, 
             Type containerType, 
             Func<object> modelAccessor, 
             Type modelType, 
             string propertyName)
         {
             var meta = base.CreateMetadata
                 (attributes, containerType, modelAccessor, modelType, propertyName);
  
             if (string.IsNullOrEmpty(propertyName)) 
                 return meta;
  
             if (meta.DisplayName == null)
                 GetLocalizedDisplayName(meta, propertyName);
  
             if (string.IsNullOrEmpty(meta.DisplayName))
                 //meta.DisplayName = string.Format("[[{0}]]", propertyName);
                 meta.DisplayName = propertyName;
  
             return meta;
         }
  
         private static void GetLocalizedDisplayName(ModelMetadata meta, string propertyName)
         {
             ResourceManager resourceManager = ClientManagerGlobalResources.ResourceManager;
             CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
             meta.DisplayName = resourceManager.GetString(propertyName, culture);
         }
     }

}