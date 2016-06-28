using System;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Form;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Template
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class TemplateBuilder<T> : IHideObjectMembers
  {
    /// <summary>
    /// Gets or sets the template.
    /// </summary>
    /// <value>
    /// The template.
    /// </value>
    protected Template<T> Template { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateBuilder{T}"/> class.
    /// </summary>
    /// <param name="template">The template.</param>
    public TemplateBuilder(Template<T> template) { Template = template; }

    /// <summary>
    /// Prefixes the specified prefix.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <returns></returns>
    public TemplateBuilder<T> Prefix(string prefix) { Template.Prefix = prefix; return this; }

    /// <summary>
    /// Names the specified name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public TemplateBuilder<T> Name(string name) { Template.Name = name; return this; }

    /// <summary>
    /// Titles the specified title.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <returns></returns>
    public TemplateBuilder<T> Title(string title) { Template.Title = title; return this; }

    /// <summary>
    /// Headers the template.
    /// </summary>
    /// <param name="headerTemplate">The header template.</param>
    /// <returns></returns>
    public TemplateBuilder<T> HeaderTemplate(string headerTemplate) { Template.HeaderTemplate = headerTemplate; return this; }

    /// <summary>
    /// Footers the template.
    /// </summary>
    /// <param name="footerTemplate">The footer template.</param>
    /// <returns></returns>
    public TemplateBuilder<T> FooterTemplate(string footerTemplate) { Template.FooterTemplate = footerTemplate; return this; }

    /// <summary>
    /// Forms the specified configurator.
    /// </summary>
    /// <param name="configurator">The configurator.</param>
    /// <returns></returns>
    public TemplateBuilder<T> Form(Action<FormBuilder<T>> configurator)
    {
      Template.Form = new Form<T>();
      configurator(new FormBuilder<T>(Template.Form));
      return this;
    }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
