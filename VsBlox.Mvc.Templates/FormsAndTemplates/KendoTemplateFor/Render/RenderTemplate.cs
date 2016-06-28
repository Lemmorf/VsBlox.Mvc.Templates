using System.Text;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.Config;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Render
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class RenderTemplate<T>
  {
    private readonly StringBuilder _innerHtml = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderTemplate{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="template">The template.</param>
    public RenderTemplate(HtmlHelper<T> html, Template.Template<T> template)
    {
      var form = new StringBuilder();

      form.Append(new RenderDisplayNamePlaceholders<T>(html, template.HeaderTemplate));
      form.Append(new RenderForm<T>(html, template.Form));
      form.Append(new RenderDisplayNamePlaceholders<T>(html, template.FooterTemplate));

      var buttons = new StringBuilder();
      foreach (var button in template.Form.Buttons.ButtonList)
      {
        if (buttons.Length > 0) buttons.Append("&nbsp;");

        //var btnTag = new TagBuilder("input");
        //btnTag.AddCssClass("k-button");
        //btnTag.MergeAttribute("type", "button");
        //btnTag.MergeAttribute("id", button.Name);
        //btnTag.MergeAttribute("value", button.Text);
        //btnTag.MergeAttribute("style", "width: 70px;");
        //if (button.HtmlAttributes != null)
        //{
        //  var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(button.HtmlAttributes);
        //  btnTag.MergeAttributes(attributes, false);
        //}
        //buttons.Append(btnTag);
        //todo: parse html fragment met html agility pack en merge de html attributes
        buttons.AppendFormat(TemplateConfig.Instance.KendoPopupButton, button.Name, button.Text);
      }
      
      _innerHtml.AppendFormat(TemplateConfig.Instance.KendoPopupTemplate, template.Name, form, buttons);
    }

    public override string ToString()
    {
      return _innerHtml.ToString();
    }
  }
}
