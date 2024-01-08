using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace WebMVC.Binders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != ValueProviderResult.None && !String.IsNullOrEmpty(valueProviderResult.FirstValue))
            {
                decimal result;
                // Use invariant culture to parse decimals with '.' 
                if (decimal.TryParse(valueProviderResult.FirstValue, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                    return Task.CompletedTask;
                }
            }

            return Task.CompletedTask;
        }
    }
}
