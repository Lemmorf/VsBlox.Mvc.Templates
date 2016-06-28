using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class RenderDateTime<T>
  {
    private readonly StringBuilder _sb = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderDateTime{T}" /> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="itemTemplates">The item templates.</param>
    /// <param name="propertyInfo">The property information.</param>
    public RenderDateTime(HtmlHelper<T> html, ItemTemplates itemTemplates, PropertyInfo propertyInfo)
    {
      if (itemTemplates == null) return;

      var propertyType = propertyInfo.GetPropertyType();
      if (!propertyInfo.CanWrite || !propertyInfo.CanRead) return;
      if (!TypeHelper.IsSameOrSubclass(typeof(DateTime), propertyType)) return;

      var template = itemTemplates.TemplateForType(propertyInfo.Name, propertyType);
      if (string.IsNullOrEmpty(template)) return;

      var labelFor = LambdaHtmlHelper.LambdaHtmlHelper.LabelRequiredFor(html, propertyInfo);
      string textBoxFor;

      if (itemTemplates.IsKendoTemplate)
      {
        var htmlAttributes = propertyInfo.HtmlAttributes() ?? new RouteValueDictionary();
        var cssClass = htmlAttributes.ContainsKey("class") ? (string)htmlAttributes["class"] : string.Empty;

        var dateTime = new TagBuilder("input");
        dateTime.AddCssClass(cssClass);
        dateTime.MergeAttribute("id", propertyInfo.InputTagId(itemTemplates.Prefix));
        dateTime.MergeAttribute("name", propertyInfo.Name);
        dateTime.MergeAttribute("type", "text");
        dateTime.MergeAttribute("data-datetimeformat", propertyInfo.DateTimeFormat() ?? "iso");

        textBoxFor = dateTime.ToString(TagRenderMode.SelfClosing);
      }
      else
      {
        var dataType = propertyInfo.GetDataType();
        if (dataType != null && (dataType == DataType.DateTime || dataType == DataType.Date || dataType == DataType.Time))
        {
          var dataTypeType =
            dataType == DataType.Date ? "date" :
            dataType == DataType.Time ? "time" :
            "datetime";

          var value = html.ViewData.Model != null ? (DateTime?)propertyInfo.GetValue(html.ViewData.Model, null) : null;

          string convertedValue;

          switch (dataType)
          {
            case DataType.Date:
              convertedValue = value?.ToString("yyyy-MM-dd") ?? string.Empty;
              break;
            case DataType.Time:
              convertedValue = value?.ToString("HH:mm:ss") ?? string.Empty;
              break;
            default:
              convertedValue = value?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty;
              break;
          }

          var htmlAttributes = propertyInfo.HtmlAttributes(itemTemplates.DefaultHtmlAttributes, new
          {
            id = propertyInfo.InputTagId(itemTemplates.Prefix),
            @type = dataTypeType,
            Value = convertedValue,
            placeholder = $"{LambdaHtmlHelper.LambdaHtmlHelper.DisplayName(propertyInfo)}"
          });
          if (propertyInfo.IsDisabled()) htmlAttributes.Add("disabled", "disabled");
          htmlAttributes.AddCssClass(propertyInfo.IsRequired() ? "required-field" : "non-required-field");

          textBoxFor = LambdaHtmlHelper.LambdaHtmlHelper.TextBoxFor(html, propertyInfo, htmlAttributes).ToString();
        }
        else
        {
          var value = html.ViewData.Model != null ? (DateTime?)propertyInfo.GetValue(html.ViewData.Model, null) : null;

          var htmlAttributes = propertyInfo.HtmlAttributes(itemTemplates.DefaultHtmlAttributes, new
          {
            id = propertyInfo.InputTagId(itemTemplates.Prefix),
            @type = "date",
            Value = value?.ToString(@"yyyy-MM-dd") ?? string.Empty,
            placeholder = $"{LambdaHtmlHelper.LambdaHtmlHelper.DisplayName(propertyInfo)}"
          });
          if (propertyInfo.IsDisabled()) htmlAttributes.Add("disabled", "disabled");
          htmlAttributes.AddCssClass(propertyInfo.IsRequired() ? "required-field" : "non-required-field");

          textBoxFor = LambdaHtmlHelper.LambdaHtmlHelper.TextBoxFor(html, propertyInfo, htmlAttributes).ToString();
        }
      }

      _sb.Append(itemTemplates.ReplacePlaceHolder(template, $"{labelFor}{Environment.NewLine}{textBoxFor}"));
    }

    public override string ToString()
    {
      return _sb.ToString();
    }
  }
}
