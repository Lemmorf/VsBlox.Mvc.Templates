using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.Config;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render
{
  public class RenderBody<T>
  {
    private readonly StringBuilder _innerHtml = new StringBuilder();

    public RenderBody(HtmlHelper<T> html, Body.Body<T> body)
    {
      if (body.ValidationSummary) _innerHtml.AppendLine(new RenderValidationSummary(html, body.ItemTemplates).ToString());
      if (body.AntiForgeryToken) _innerHtml.AppendLine(html.AntiForgeryToken().ToString());

      _innerHtml.Append(new RenderHiddenProperties<T>(html, body.ItemTemplates));

      var anyRequired = false;

      foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead))
      {
        if (propertyInfo.IsHidden() || propertyInfo.IgnoreInForm()) continue;
        // when HideNullProperties == true, check for null values...
        if (html.ViewData.Model != null && body.Model.HideNullProperties && propertyInfo.GetValue(html.ViewData.Model, null) == null) continue;

        if (propertyInfo.IsRequired()) anyRequired = true;

        _innerHtml.AppendLine(new RenderProperty<T>(html, body.PropertyInfos, body.ItemTemplates, propertyInfo).ToString());
      }

      if (!anyRequired) return;

      _innerHtml.AppendLine(TemplateConfig.Instance.MandatoryText);
    }

    public override string ToString()
    {
      return _innerHtml.ToString();
    }
  }
}
