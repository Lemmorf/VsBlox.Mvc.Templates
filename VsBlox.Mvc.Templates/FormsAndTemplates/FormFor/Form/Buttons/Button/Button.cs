using System.ComponentModel;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Buttons.Button
{
  public class Button : IHideObjectMembers
  {
    public enum ButtonType { Normal, Submit, Cancel }

    public ButtonType Type { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public object HtmlAttributes { get; set; }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Text);
  }
}


