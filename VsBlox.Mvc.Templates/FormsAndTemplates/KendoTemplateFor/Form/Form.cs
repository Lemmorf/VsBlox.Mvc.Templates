using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.PropertyInfo;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Buttons;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Form
{
  /// <summary>
  /// Container for link info.
  /// </summary>
  public class Form<T> : IHideObjectMembers
  {
    public Form()
    {
      ItemTemplates = new ItemTemplates
      {
        IsKendoTemplate = true,
        Prefix = Name
      };
    }

    public string Url { get; set; }

    private string _name = "form-" + Guid.NewGuid();
    public string Name
    {
      get { return _name; }

      set
      {
        _name = value;
        ItemTemplates.Prefix = Name;
      }
    }

    public object HtmlAttributes { get; set; }

    public bool AntiForgeryToken { get; set; }

    public bool ValidationSummary { get; set; }

    public bool HideNullProperties { get; set; }

    public Buttons Buttons { get; set; } = new Buttons();

    public FormMethod Method { get; set; } = FormMethod.Post;

    public ItemTemplates ItemTemplates { get; set; }

    private FormFor.Body.Body<T> _body;
    public FormFor.Body.Body<T> Body
    {
      get
      {
        if (_body == null) _body = new FormFor.Body.Body<T>();

        _body.AntiForgeryToken = AntiForgeryToken;
        _body.ValidationSummary = ValidationSummary;
        _body.ItemTemplates = ItemTemplates;
        _body.PropertyInfos = PropertyInfos;
        _body.Prefix = Name;

        return _body;
      }
    }

    public List<PropertyInfo> PropertyInfos { get; set; } = new List<PropertyInfo>();

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsValid => 
      !string.IsNullOrEmpty(Name) &&
      ItemTemplates != null && ItemTemplates.IsValid &&
      PropertyInfos != null && PropertyInfos.TrueForAll(p => p.IsValid) &&
      (Buttons == null || Buttons.IsValid) &&
      Body.IsValid;

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
