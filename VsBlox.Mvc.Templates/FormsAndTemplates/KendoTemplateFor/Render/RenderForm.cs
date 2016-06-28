using System.Text;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Render
{
  public class RenderForm<T>
  {
    private readonly TagBuilder _formTag = new TagBuilder("form");

    private readonly StringBuilder _innerHtml = new StringBuilder();

    public RenderForm(HtmlHelper<T> html, Form.Form<T> form)
    {
      if (!string.IsNullOrEmpty(form.Url)) _formTag.MergeAttribute("action", form.Url);
      _formTag.MergeAttribute("method", form.Method == FormMethod.Get ? "GET" : "POST");
      _formTag.MergeAttribute("id", form.Name);

      if (form.HtmlAttributes != null)
      {
        var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(form.HtmlAttributes);
        _formTag.MergeAttributes(attributes, false);
      }

      var htmlAttributes = typeof(T).HtmlAttributes();
      if (htmlAttributes != null)
      {
        foreach (var htmlAttribute in htmlAttributes)
        {
          if (htmlAttribute.Key.ToLowerInvariant() == "class") _formTag.AddCssClass(htmlAttribute.Value.ToString());
          else _formTag.MergeAttribute(htmlAttribute.Key.ToLowerInvariant(), htmlAttribute.Value.ToString(), false);
        }
      }

      _innerHtml.Append(new RenderBody<T>(html, form.Body));
    }

    public override string ToString()
    {
      _formTag.InnerHtml = _innerHtml.ToString();

      return _formTag.ToString();
    }
  }
}
