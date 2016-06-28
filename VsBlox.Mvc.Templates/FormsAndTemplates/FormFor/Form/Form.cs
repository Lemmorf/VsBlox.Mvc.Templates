using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.PropertyInfo;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Model;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form
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
        IsKendoTemplate = false,
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

    /// <summary>
    /// Gets or sets the HTML attributes.
    /// </summary>
    /// <value>
    /// The HTML attributes.
    /// </value>
    public object HtmlAttributes { get; set; }

    public bool AntiForgeryToken { get; set; }

    public bool ValidationSummary { get; set; }

    public FormMethod Method { get; set; } = FormMethod.Post;

    public ItemTemplates ItemTemplates { get; set; } 

    public Buttons.Buttons Buttons { get; set; } = new Buttons.Buttons();

    public List<PropertyInfo> PropertyInfos { get; set; } = new List<PropertyInfo>();

    public Model<T> Model { get; set; }

    private Body.Body<T> _body;
    public Body.Body<T> Body
    {
      get
      {
        if (_body == null) _body = new Body.Body<T>();

        _body.AntiForgeryToken = AntiForgeryToken;
        _body.ValidationSummary = ValidationSummary;
        _body.ItemTemplates = ItemTemplates;
        _body.PropertyInfos = PropertyInfos;
        _body.Model = Model;
        _body.Prefix = Name;

        return _body;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsValid => 
      !string.IsNullOrEmpty(Name) &&
      ItemTemplates != null && ItemTemplates.IsValid &&
      PropertyInfos != null && PropertyInfos.TrueForAll(p => p.IsValid) &&
      (Model == null || Model.IsValid) &&
      (Buttons == null || Buttons.IsValid) &&
      Body.IsValid;

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
