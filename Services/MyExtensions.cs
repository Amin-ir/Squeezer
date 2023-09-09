using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Squeezer.Services
{
    public static class MyExtensions
    {
        public static List<string> GetModelErrorTexts(this ModelStateDictionary modelState) 
        {
            var invalidModelStateValues = modelState.Values
                .Where(value => value.ValidationState == ModelValidationState.Invalid).ToList();

            List<string> errorTexts = new List<string>();
            invalidModelStateValues.ForEach(value => errorTexts.Add(value.Errors.First().ErrorMessage));

            return errorTexts;
        }
    }
}
