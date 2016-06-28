using System;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Buttons.Button
{
  public class ButtonBuilder : IHideObjectMembers
  {
    protected Button Button { get; set; }

    public ButtonBuilder(Button button) { Button = button; }

    public ButtonBuilder Type(Button.ButtonType type) { Button.Type = type; return this; }

    public ButtonBuilder Name(string name) { Button.Name = name; return this; }

    public ButtonBuilder Text(string text) { Button.Text = text; return this; }

    public ButtonBuilder HtmlAttributes(object htmlAttributes) { Button.HtmlAttributes = htmlAttributes; return this; }
    
    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
