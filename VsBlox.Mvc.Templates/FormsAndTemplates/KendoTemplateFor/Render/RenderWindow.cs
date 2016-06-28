using System.Web.Mvc;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Render
{
  public class RenderWindow<T>
  {
    private readonly TagBuilder _window = new TagBuilder("div");

    public RenderWindow(HtmlHelper<T> html, Template.Template<T> template)
    {
      _window.MergeAttribute("id", $"{template.Name}Window");
    }

    public override string ToString()
    {
      return _window.ToString();
    }
  }
}
