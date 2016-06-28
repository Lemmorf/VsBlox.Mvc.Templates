using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Script.Events
{
  public class Events : IHideObjectMembers
  {
    public string OnInitialization { get; set; }
    public string OnCompletion { get; set; }
    public string OnCancel { get; set; }
    public string OnSubmit { get; set; }

    public class BindToInfo
    {
      public BindToInfo(string name, object handlers, BindingType type)
      {
        Name = name;
        Handlers = HtmlHelper.AnonymousObjectToHtmlAttributes(handlers);
        Type = type;
      }
      public enum BindingType { IsProperty, IsId }
      public BindingType Type { get; set; }
      public string Name { get; set; }
      public RouteValueDictionary Handlers { get; set; }

      public bool IsValid => !string.IsNullOrEmpty(Name) && Handlers != null;
    }

    public List<BindToInfo> BindTo { get; set; } = new List<BindToInfo>();

    public bool IsValid => true;
    
    Type IHideObjectMembers.GetType()
    {
      return GetType();
    }
  }
}
