using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render
{
  public class RenderValidationSummary
  {
    private readonly TagBuilder _div = new TagBuilder("div");

    private readonly StringBuilder _sb = new StringBuilder();

    public RenderValidationSummary(HtmlHelper html, ItemTemplates itemTemplates)
    {
      if (itemTemplates.IsKendoTemplate)
      {
        _div.MergeAttribute("class", "validation-summary-valid");
        _div.MergeAttribute("data-valmsg-summary", "true");

        var ul = new TagBuilder("ul");
        var li = new TagBuilder("li");
        li.MergeAttribute("style", "display:none");
        ul.InnerHtml = li.ToString();
        _div.InnerHtml = ul.ToString();

        _sb.Append(_div);
      }
      else
      {
        _sb.Append(html.ValidationSummary(false));
      }
    }

    public override string ToString()
    {
      return _sb.ToString();
    }
  }
}
