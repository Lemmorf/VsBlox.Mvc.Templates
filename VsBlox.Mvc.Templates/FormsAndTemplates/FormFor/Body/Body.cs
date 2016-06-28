using System;
using System.Collections.Generic;
using System.ComponentModel;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Model;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body
{
  /// <summary>
  /// Container for link info.
  /// </summary>
  public class Body<T> : IHideObjectMembers
  {
    public Body()
    {
      ItemTemplates = new ItemTemplates.ItemTemplates
      {
        IsKendoTemplate = false,
        Prefix = Prefix
      };
    }

    private string _prefix = "form-" + Guid.NewGuid();
    public string Prefix
    {
      get { return _prefix; }

      set
      {
        _prefix = value;
        ItemTemplates.Prefix = _prefix;
      }
    }
    
    public bool AntiForgeryToken { get; set; }

    public bool ValidationSummary { get; set; }

    public ItemTemplates.ItemTemplates ItemTemplates { get; set; }

    public List<PropertyInfo.PropertyInfo> PropertyInfos { get; set; } = new List<PropertyInfo.PropertyInfo>();

    public Model<T> Model { get; set; }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsValid => 
      ItemTemplates != null && ItemTemplates.IsValid &&
      (Model == null || Model.IsValid) &&
      PropertyInfos != null && PropertyInfos.TrueForAll(p => p.IsValid);

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
