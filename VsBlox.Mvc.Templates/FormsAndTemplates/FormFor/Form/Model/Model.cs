using System;
using System.ComponentModel;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Model
{
  /// <summary>
  /// Container for link info.
  /// </summary>
  public class Model<T> : IHideObjectMembers
  {
    public T Data { get; set; }

    public bool HideNullProperties { get; set; }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsValid => Data != null;

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
