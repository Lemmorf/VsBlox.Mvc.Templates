using System;
using System.Collections.Generic;
using System.Linq;
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
  public class RenderProperty<T>
  {
    private readonly StringBuilder _innerHtml = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderProperty{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="modelPropertyInfos">The model property infos.</param>
    /// <param name="itemTemplates">The item templates.</param>
    /// <param name="propertyInfo">The property information.</param>
    public RenderProperty(HtmlHelper<T> html, List<Body.PropertyInfo.PropertyInfo> modelPropertyInfos, ItemTemplates itemTemplates, PropertyInfo propertyInfo)
    {
      var propertyType = propertyInfo.GetPropertyType();
      if (!propertyInfo.CanWrite || !propertyInfo.CanRead) return;

      Body.PropertyInfo.PropertyInfo modelPropertyInfo = null;
      if (modelPropertyInfos != null) modelPropertyInfo = modelPropertyInfos.FirstOrDefault(p => p.PropertyName == propertyInfo.Name);

      if (propertyInfo.HasUiHint())
      {
        _innerHtml.AppendLine(new RenderUiHint<T>(html, itemTemplates, propertyInfo).ToString());
      }
      else if (modelPropertyInfo != null && modelPropertyInfo.HasSelectList)
      {
        _innerHtml.AppendLine(new RenderDropDownList<T>(html, itemTemplates, modelPropertyInfo.SelectList, propertyInfo).ToString());
      }
      else if (modelPropertyInfo != null && modelPropertyInfo.HasGroupSelectList)
      {
        _innerHtml.AppendLine(new RenderDropDownGroupedList<T>(html, itemTemplates, modelPropertyInfo.GroupSelectList, propertyInfo).ToString());
      }
      else if (TypeHelper.IsSameOrSubclass(typeof(string), propertyType))
      {
        _innerHtml.AppendLine(new RenderString<T>(html, itemTemplates, propertyInfo).ToString());
      }
      else if (TypeHelper.IsSameOrSubclass(typeof(int), propertyType))
      {
        _innerHtml.AppendLine(new RenderInt<T>(html, itemTemplates, propertyInfo).ToString());
      }
      else if (TypeHelper.IsSameOrSubclass(typeof(Enum), propertyType))
      {
        _innerHtml.AppendLine(new RenderEnum<T>(html, itemTemplates, propertyInfo).ToString());
      }
      else if (TypeHelper.IsSameOrSubclass(typeof(bool), propertyType))
      {
        _innerHtml.AppendLine(new RenderBool<T>(html, itemTemplates, propertyInfo).ToString());
      }
      else if (TypeHelper.IsSameOrSubclass(typeof(DateTime), propertyType))
      {
        _innerHtml.AppendLine(new RenderDateTime<T>(html, itemTemplates, propertyInfo).ToString());
      }
    }

    public override string ToString()
    {
      return _innerHtml.ToString();
    }
  }
}
