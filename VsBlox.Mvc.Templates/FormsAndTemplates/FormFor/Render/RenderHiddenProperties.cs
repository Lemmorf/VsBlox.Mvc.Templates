using System.Linq;
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
  public class RenderHiddenProperties<T>
  {
    private readonly StringBuilder _innerHtml = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderHiddenProperties{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="itemTemplates">The item templates.</param>
    public RenderHiddenProperties(HtmlHelper<T> html, ItemTemplates itemTemplates)
    {
      if (itemTemplates == null) return;

      foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead))
      {
        if (!propertyInfo.IsHidden() || propertyInfo.IgnoreInForm()) continue;

        var htmlAttributes = propertyInfo.HtmlAttributes(itemTemplates.DefaultHtmlAttributes, new { id = propertyInfo.InputTagId(itemTemplates.Prefix) });

        _innerHtml.Append(LambdaHtmlHelper.LambdaHtmlHelper.HiddenFor(html, propertyInfo, htmlAttributes));
      }
    }

    public override string ToString()
    {
      return _innerHtml.ToString();
    }
  }
}
  