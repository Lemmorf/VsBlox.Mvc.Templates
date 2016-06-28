using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class RenderEnum<T>
  {
    private readonly StringBuilder _sb = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderEnum{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="itemTemplates">The item templates.</param>
    /// <param name="propertyInfo">The property information.</param>
    public RenderEnum(HtmlHelper<T> html, ItemTemplates itemTemplates, PropertyInfo propertyInfo)
    {
      if (itemTemplates == null) return;

      var propertyType = propertyInfo.GetPropertyType();
      if (!propertyInfo.CanWrite || !propertyInfo.CanRead) return;
      if (!TypeHelper.IsSameOrSubclass(typeof(Enum), propertyType)) return;

      var template = itemTemplates.TemplateFor(propertyInfo.Name, propertyType);
      if (string.IsNullOrEmpty(template)) return;

      var enumDescriptions = new Dictionary<int, string>();
      foreach (var item in Enum.GetValues(propertyType))
      {
        var fieldInfo = item.GetType().GetField(item.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        enumDescriptions.Add((int)item, attributes.Length > 0 ? attributes[0].Description : item.ToString());
      }

      var selectList = enumDescriptions.Any() ? new SelectList(enumDescriptions, "Key", "Value").ToList() : EnumHelper.GetSelectList(propertyType).ToList();

      var labelFor = LambdaHtmlHelper.LambdaHtmlHelper.LabelRequiredFor(html, propertyInfo);

      var htmlAttributes = propertyInfo.HtmlAttributes(itemTemplates.DefaultHtmlAttributes, new
      {
        id = propertyInfo.InputTagId(itemTemplates.Prefix),
        placeholder = $"{LambdaHtmlHelper.LambdaHtmlHelper.DisplayName(propertyInfo)}"
      });
      if (propertyInfo.IsDisabled()) htmlAttributes.Add("disabled", "disabled");
      htmlAttributes.AddCssClass(propertyInfo.IsRequired() ? "required-field" : "non-required-field");

      var dropDownListFor = LambdaHtmlHelper.LambdaHtmlHelper.DropDownListFor(html, propertyInfo, selectList, "- Select an item -", htmlAttributes);

      _sb.Append(itemTemplates.ReplacePlaceHolder(template, $"{labelFor}{Environment.NewLine}{dropDownListFor}"));
    }

    public override string ToString()
    {
      return _sb.ToString();
    }
  }
}
