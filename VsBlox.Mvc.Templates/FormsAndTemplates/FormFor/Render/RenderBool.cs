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
  public class RenderBool<T>
  {
    private readonly StringBuilder _sb = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderBool{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="itemTemplates">The item templates.</param>
    /// <param name="propertyInfo">The property information.</param>
    public RenderBool(HtmlHelper<T> html, ItemTemplates itemTemplates, PropertyInfo propertyInfo)
    {
      if (itemTemplates == null) return;

      var propertyType = propertyInfo.GetPropertyType();
      if (!propertyInfo.CanWrite || !propertyInfo.CanRead) return;
      if (!TypeHelper.IsSameOrSubclass(typeof(bool), propertyType)) return;
      
      var template = itemTemplates.CheckBoxTemplateFor(propertyInfo.Name, propertyType);
      if (string.IsNullOrEmpty(template)) return;

      var htmlAttributes = propertyInfo.HtmlAttributes(new
      {
        id = propertyInfo.InputTagId(itemTemplates.Prefix)
      });

      var customCssClass = string.Empty;
      if (htmlAttributes.ContainsKey("class"))
      {
        customCssClass = (string) htmlAttributes["class"];
      }

      htmlAttributes.AddCssClass("checkbox");
     
      if (propertyInfo.IsDisabled()) htmlAttributes.Add("disabled", "disabled");

      var divTag = new TagBuilder("div");
      divTag.AddCssClass(customCssClass);

      var checkboxFor = LambdaHtmlHelper.LambdaHtmlHelper.CheckBoxFor(html, propertyInfo, htmlAttributes);
      var displayNameFor = LambdaHtmlHelper.LambdaHtmlHelper.DisplayNameFor(html, propertyInfo);

      divTag.InnerHtml = itemTemplates.ReplacePlaceHolder(template, $"{checkboxFor}{Environment.NewLine}{displayNameFor}");
      _sb.Append(divTag);
    }

    public override string ToString()
    {
      return _sb.ToString();
    }
  }
}
