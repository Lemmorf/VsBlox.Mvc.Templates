using System.Text;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.Config;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class RenderForm<T>
  {
    private readonly TagBuilder _formTag = new TagBuilder("form");

    private readonly StringBuilder _innerHtml = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderForm{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="form">The form.</param>
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

      var htmlAttributes = typeof (T).HtmlAttributes();
      if (htmlAttributes != null)
      {
        foreach (var htmlAttribute in htmlAttributes)
        {
          if (htmlAttribute.Key.ToLowerInvariant() == "class") _formTag.AddCssClass(htmlAttribute.Value.ToString());
          else _formTag.MergeAttribute(htmlAttribute.Key.ToLowerInvariant(), htmlAttribute.Value.ToString(), false);
        }
      }

      _innerHtml.Append(new RenderBody<T>(html, form.Body));

      var buttons = new StringBuilder();
      foreach (var button in form.Buttons.ButtonList)
      {
        if (buttons.Length > 0) buttons.Append("&nbsp;");

        //var btnTag = new TagBuilder("input");
        //btnTag.MergeAttribute("type", "button");
        //btnTag.MergeAttribute("id", button.Name);
        //btnTag.MergeAttribute("value", button.Text);
        //if (button.HtmlAttributes != null)
        //{
        //  var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(button.HtmlAttributes);
        //  btnTag.MergeAttributes(attributes, false);
        //}
        //todo: parse html fragment met html agility pack en merge de html attributes
        buttons.AppendFormat(TemplateConfig.Instance.FormButton, button.Name, button.Text);
      }

      _innerHtml.AppendFormat(TemplateConfig.Instance.FormButtonGroup, buttons);
    }

    public override string ToString()
    {
      _formTag.InnerHtml = _innerHtml.ToString();

      return _formTag.ToString();
    }
  }
}
