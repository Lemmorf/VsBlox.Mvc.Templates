using System;
using System.Collections.Generic;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Buttons
{
  public class Buttons : IHideObjectMembers
  {
    public List<Button.Button> ButtonList { get; set; } = new List<Button.Button>();

    public bool IsValid => ButtonList.Count == 0 || ButtonList.TrueForAll(b => b.IsValid);
    
    Type IHideObjectMembers.GetType()
    {
      return GetType();
    }
  }
}
