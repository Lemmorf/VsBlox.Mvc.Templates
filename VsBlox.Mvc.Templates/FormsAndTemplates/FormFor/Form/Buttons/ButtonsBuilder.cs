using System;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Buttons.Button;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Buttons
{
  public class ButtonsBuilder : IHideObjectMembers
  {
    protected Buttons Buttons { get; set; }

    public ButtonsBuilder(Buttons buttons) { Buttons = buttons; }

    public ButtonsBuilder Button(Action<ButtonBuilder> configurator)
    {
      var button = new Button.Button();
      configurator(new ButtonBuilder(button));
      Buttons.ButtonList.Add(button);
      return this;
    }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
