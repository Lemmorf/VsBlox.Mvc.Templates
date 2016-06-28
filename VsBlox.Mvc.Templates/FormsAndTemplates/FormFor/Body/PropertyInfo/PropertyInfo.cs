using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.PropertyInfo
{
  /// <summary>
  /// Container for link info.
  /// </summary>
  public class PropertyInfo : IHideObjectMembers
  {
    public string PropertyName { get; set; }

    public IEnumerable<SelectListItem> SelectList { get; set; }

    public IEnumerable<GroupedSelectListItem> GroupSelectList { get; set; }

    public bool HasSelectList => SelectList != null && SelectList.Any();

    public bool HasGroupSelectList => GroupSelectList != null && GroupSelectList.Any();

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsValid => !string.IsNullOrEmpty(PropertyName);

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
