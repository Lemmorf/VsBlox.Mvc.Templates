using System;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class RenderInt<T>
  {
    private readonly StringBuilder _sb = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderInt{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="itemTemplates">The item templates.</param>
    /// <param name="propertyInfo">The property information.</param>
    public RenderInt(HtmlHelper<T> html, ItemTemplates itemTemplates, PropertyInfo propertyInfo)
    {
      if (itemTemplates == null) return;

      var propertyType = propertyInfo.GetPropertyType();
      if (!propertyInfo.CanWrite || !propertyInfo.CanRead) return;
      if (!TypeHelper.IsSameOrSubclass(typeof(int), propertyType)) return;
      if (propertyInfo.IsHidden() || propertyInfo.IgnoreInForm()) return;

      var template = itemTemplates.TemplateFor(propertyInfo.Name, propertyType);
      if (string.IsNullOrEmpty(template)) return;

      var labelFor = LambdaHtmlHelper.LambdaHtmlHelper.LabelRequiredFor(html, propertyInfo);

      var htmlAttributes = propertyInfo.HtmlAttributes(itemTemplates.DefaultHtmlAttributes, new
      {
        id = propertyInfo.InputTagId(itemTemplates.Prefix),
        @type = "number",
        placeholder = $"{LambdaHtmlHelper.LambdaHtmlHelper.DisplayName(propertyInfo)}"
      });
      if (propertyInfo.IsDisabled()) htmlAttributes.Add("disabled", "disabled");
      htmlAttributes.AddCssClass(propertyInfo.IsRequired() ? "required-field" : "non-required-field");

      var textBoxfor = LambdaHtmlHelper.LambdaHtmlHelper.TextBoxFor(html, propertyInfo, htmlAttributes);

      _sb.Append(itemTemplates.ReplacePlaceHolder(template, $"{labelFor}{Environment.NewLine}{textBoxfor}"));
    }

    public override string ToString()
    {
      return _sb.ToString();
    }
  }
}
