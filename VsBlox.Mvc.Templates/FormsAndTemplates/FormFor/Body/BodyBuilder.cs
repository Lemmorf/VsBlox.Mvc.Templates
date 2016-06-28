using System;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Model;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body
{
  /// <summary>
  /// Builder for form info container
  /// </summary>
  public class BodyBuilder<T> : IHideObjectMembers
  {
    protected Body<T> Body { get; set; }

    public BodyBuilder(Body<T> body) { Body = body; }
    public BodyBuilder<T> Prefix(string prefix) { Body.Prefix = prefix; return this; }
    public BodyBuilder<T> AntiForgerytoken(bool antiForgeryToken) { Body.AntiForgeryToken = antiForgeryToken; return this; }

    public BodyBuilder<T> ValidationSummary(bool validationSummary) { Body.ValidationSummary = validationSummary; return this; }

    public BodyBuilder<T> Model(Action<ModelBuilder<T>> configurator)
    {
      Body.Model = new Model<T>();
      configurator(new ModelBuilder<T>(Body.Model));
      return this;
    }

    public BodyBuilder<T> ItemTemplates(Action<ItemTemplatesBuilder<T>> configurator)
    {
      Body.ItemTemplates = new ItemTemplates.ItemTemplates
      {
        IsKendoTemplate = false,
        Prefix = Body.Prefix
      };
      configurator(new ItemTemplatesBuilder<T>(Body.ItemTemplates));
      return this;
    }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
