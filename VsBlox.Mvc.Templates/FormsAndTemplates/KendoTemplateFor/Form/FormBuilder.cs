using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.PropertyInfo;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Buttons;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Form
{
  /// <summary>
  /// Builder for form info container
  /// </summary>
  public class FormBuilder<T> : IHideObjectMembers
  {
    protected Form<T> Form { get; set; }

    public FormBuilder(Form<T> form) { Form = form; }

    public FormBuilder<T> Url(string url) { Form.Url = url; return this; }

    public FormBuilder<T> Name(string name) { Form.Name = name; return this; }

    public FormBuilder<T> AntiForgerytoken(bool antiForgeryToken) { Form.AntiForgeryToken = antiForgeryToken; return this; }

    public FormBuilder<T> ValidationSummary(bool validationSummary) { Form.ValidationSummary = validationSummary; return this; }

    public FormBuilder<T> Method(FormMethod method) { Form.Method = method; return this; }

    public FormBuilder<T> HtmlAttributes(object htmlAttributes) { Form.HtmlAttributes = htmlAttributes; return this; }

    public FormBuilder<T> ItemTemplates(Action<ItemTemplatesBuilder<T>> configurator)
    {
      Form.ItemTemplates = new ItemTemplates
      {
        IsKendoTemplate = true,
        Prefix = Form.Name
      };
      configurator(new ItemTemplatesBuilder<T>(Form.ItemTemplates));
      return this;
    }

    public FormBuilder<T> Buttons(Action<ButtonsBuilder> configurator)
    {
      configurator(new ButtonsBuilder(Form.Buttons));
      return this;
    }

    public FormBuilder<T> PropertyInfo(Action<PropertyInfoBuilder<T>> configurator)
    {
      if (Form.PropertyInfos == null) Form.PropertyInfos = new List<PropertyInfo>();
      var propertyInfo = new PropertyInfo();
      configurator(new PropertyInfoBuilder<T>(propertyInfo));
      Form.PropertyInfos.Add(propertyInfo);
      return this;
    }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
