using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class RenderDropDownGroupedList<T>
  {
    private readonly StringBuilder _sb = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderDropDownGroupedList{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="itemTemplates">The item templates.</param>
    /// <param name="groupSelectList">The group select list.</param>
    /// <param name="propertyInfo">The property information.</param>
    public RenderDropDownGroupedList(HtmlHelper<T> html, ItemTemplates itemTemplates, IEnumerable<GroupedSelectListItem> groupSelectList, PropertyInfo propertyInfo)
    {
      if (itemTemplates == null) return;

      var propertyType = propertyInfo.GetPropertyType();
      if (!propertyInfo.CanWrite || !propertyInfo.CanRead) return;

      var template = itemTemplates.TemplateFor(propertyInfo.Name, propertyType);
      if (string.IsNullOrEmpty(template)) return;


      var labelFor = LambdaHtmlHelper.LambdaHtmlHelper.LabelRequiredFor(html, propertyInfo);

      var htmlAttributes = propertyInfo.HtmlAttributes(itemTemplates.DefaultHtmlAttributes, new
      {
        id = propertyInfo.InputTagId(itemTemplates.Prefix),
        placeholder = $"{LambdaHtmlHelper.LambdaHtmlHelper.DisplayName(propertyInfo)}"
      });
      if (propertyInfo.IsDisabled()) htmlAttributes.Add("disabled", "disabled");
      htmlAttributes.AddCssClass(propertyInfo.IsRequired() ? "required-field" : "non-required-field");

      var dropDownGroupListFor = LambdaHtmlHelper.LambdaHtmlHelper.DropDownGroupListFor(html, propertyInfo, groupSelectList, "- Select an item -", htmlAttributes);

      _sb.Append(itemTemplates.ReplacePlaceHolder(template, $"{labelFor}{Environment.NewLine}{dropDownGroupListFor}"));
    }

    public override string ToString()
    {
      return _sb.ToString();
    }
  }
}
