using System.Linq;
using System.Web.Mvc;

namespace Hoangldp.Web.Attributes
{
    public class TrimModelBinder : DefaultModelBinder
    {
        protected override void SetProperty(ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            System.ComponentModel.PropertyDescriptor propertyDescriptor, object value)
        {
            NoTrimAttribute allowTrim = null;
            if (propertyDescriptor.Attributes.Count > 0)
            {
                allowTrim = propertyDescriptor.Attributes.OfType<NoTrimAttribute>().FirstOrDefault();
            }
            if (propertyDescriptor.PropertyType == typeof(string) && (allowTrim == null || allowTrim.IsTrim))
            {
                var stringValue = (string)value;
                if (!string.IsNullOrWhiteSpace(stringValue))
                {
                    value = stringValue.Trim();
                }
                else
                {
                    value = null;
                }
            }

            base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        }
    }
}