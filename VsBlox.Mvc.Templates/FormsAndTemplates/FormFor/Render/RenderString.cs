using System;
using System.ComponentModel.DataAnnotations;
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
  public class RenderString<T>
  {
    private readonly StringBuilder _sb = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderString{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="itemTemplates">The item templates.</param>
    /// <param name="propertyInfo">The property information.</param>
    public RenderString(HtmlHelper<T> html, ItemTemplates itemTemplates, PropertyInfo propertyInfo)
    {
      if (itemTemplates == null) return;

      var propertyType = propertyInfo.GetPropertyType();
      if (!propertyInfo.CanWrite || !propertyInfo.CanRead) return;
      if (!TypeHelper.IsSameOrSubclass(typeof(string), propertyType)) return;

      var template = itemTemplates.TemplateFor(propertyInfo.Name, propertyType);
      if (string.IsNullOrEmpty(template)) return;

      //_sb.Append(LambdaHtmlHelper.LambdaHtmlHelper.LabelFor(html, propertyInfo));
      var labelFor = LambdaHtmlHelper.LambdaHtmlHelper.LabelRequiredFor(html, propertyInfo);
      string textBoxFor;

      var dataType = propertyInfo.GetDataType();
      if (dataType != null)
      {
        var dataTypeType =
          dataType == DataType.PhoneNumber ? "phonenumber" :
          dataType == DataType.EmailAddress ? "email" :
          dataType == DataType.ImageUrl ? "imageurl" :
          dataType == DataType.CreditCard ? "creditcard" :
          dataType == DataType.PostalCode ? "postalcode" :
          dataType == DataType.Text ? "text" :
          dataType == DataType.Currency ? "currency" :
          dataType == DataType.Url ? "url" :
          "text";

        var htmlAttributes = propertyInfo.HtmlAttributes(itemTemplates.DefaultHtmlAttributes, new
        {
          id = propertyInfo.InputTagId(itemTemplates.Prefix),
          @type = dataTypeType,
          placeholder = $"{LambdaHtmlHelper.LambdaHtmlHelper.DisplayName(propertyInfo)}"
        });
        if (propertyInfo.IsDisabled()) htmlAttributes.Add("disabled", "disabled");
        htmlAttributes.AddCssClass(propertyInfo.IsRequired() ? "required-field" : "non-required-field");

        textBoxFor = LambdaHtmlHelper.LambdaHtmlHelper.TextBoxFor(html, propertyInfo, htmlAttributes).ToString();
      }
      else
      {
        var htmlAttributes = propertyInfo.HtmlAttributes(itemTemplates.DefaultHtmlAttributes, new
        {
          id = propertyInfo.InputTagId(itemTemplates.Prefix),
          placeholder = $"{LambdaHtmlHelper.LambdaHtmlHelper.DisplayName(propertyInfo)}"
        });
        if (propertyInfo.IsDisabled()) htmlAttributes.Add("disabled", "disabled");
        htmlAttributes.AddCssClass(propertyInfo.IsRequired() ? "required-field" : "non-required-field");

        textBoxFor = LambdaHtmlHelper.LambdaHtmlHelper.TextBoxFor(html, propertyInfo, htmlAttributes).ToString();
      }

      _sb.Append(itemTemplates.ReplacePlaceHolder(template, $"{labelFor}{Environment.NewLine}{textBoxFor}"));
    }

    public override string ToString()
    {
      return _sb.ToString();
    }
  }
}
