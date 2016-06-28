using System;
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
  public class RenderUiHint<T>
  {
    private readonly StringBuilder _sb = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderUiHint{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="itemTemplates">The item templates.</param>
    /// <param name="propertyInfo">The property information.</param>
    /// <exception cref="System.NotSupportedException">UIHint is not supported at this moment</exception>
    public RenderUiHint(HtmlHelper<T> html, ItemTemplates itemTemplates, PropertyInfo propertyInfo)
    {
      var propertyType = propertyInfo.GetPropertyType();
      if (!propertyInfo.CanWrite || !propertyInfo.CanRead) return;

      // todo: uihint
      throw new NotSupportedException("UIHint is not supported at this moment");
    }

    public override string ToString()
    {
      return _sb.ToString();
    }
  }
}
