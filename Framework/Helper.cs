using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq;
using System.Collections.Generic;
namespace System.Web.Mvc.Html
{
        public static class EnumDropDownList
        {
            private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };
            public static HtmlString EnumTypeDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, TEnum>> modelExpression)
            {
                ModelMetadata metadata = ModelMetadata.FromLambdaExpression(modelExpression, htmlHelper.ViewData);

                var typeOfProperty = modelExpression.ReturnType;
                if (!typeOfProperty.IsEnum)
                    throw new ArgumentException(string.Format("Type {0} is not an enum", typeOfProperty));

                var values = Enum.GetValues(typeof(TEnum)).Cast<object>();

                IEnumerable<SelectListItem> items = values.Select(value => new SelectListItem
                {
                    Text = value.ToString().Replace('_', ' '),
                    Value =  ((int)value).ToString(),
                    Selected = value.Equals(metadata.Model)
                });

                if (metadata.IsNullableValueType)
                {
                    items = SingleEmptyItem.Concat(items);
                }

                return htmlHelper.DropDownListFor(modelExpression, items);
            }
        }
}