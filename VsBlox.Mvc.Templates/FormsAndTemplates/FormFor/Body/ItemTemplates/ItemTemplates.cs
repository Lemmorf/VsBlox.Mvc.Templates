using System;
using System.Collections.Generic;
using System.ComponentModel;
using VsBlox.Mvc.Templates.FormsAndTemplates.Config;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates
{
  /// <summary>
  /// Container for link info.
  /// </summary>
  public class ItemTemplates : IHideObjectMembers
  {
    /// <summary>
    /// 
    /// </summary>
    public enum Control
    {
      /// <summary>
      /// The general
      /// </summary>
      General,
      /// <summary>
      /// The CheckBox
      /// </summary>
      CheckBox,
      /// <summary>
      /// The RadioButton
      /// </summary>
      RadioButton
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemTemplates"/> class.
    /// </summary>
    public ItemTemplates()
    {
      TypeTemplates.Add(typeof(DateTime), TemplateConfig.Instance.DateTimeTemplate);

      ControlTemplates.Add(Control.General, TemplateConfig.Instance.GeneralTemplate);
      ControlTemplates.Add(Control.CheckBox, TemplateConfig.Instance.CheckBoxTemplate);
      ControlTemplates.Add(Control.RadioButton, TemplateConfig.Instance.RadioButtonTemplate);

      DefaultHtmlAttributes = new { @class = TemplateConfig.Instance.DefaultHtmlAttributes };
    }

    /// <summary>
    /// The place holder token
    /// </summary>
    public const string PlaceHolderToken = TemplateConfig.PlaceHolderToken;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is kendo template.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is kendo template; otherwise, <c>false</c>.
    /// </value>
    public bool IsKendoTemplate { get; set; }

    /// <summary>
    /// Gets or sets the prefix.
    /// </summary>
    /// <value>
    /// The prefix.
    /// </value>
    public string Prefix { get; set; }

    /// <summary>
    /// Gets or sets the property templates.
    /// </summary>
    /// <value>
    /// The property templates.
    /// </value>
    public Dictionary<string, string> PropertyTemplates { get; set; } = new Dictionary<string, string>();
    /// <summary>
    /// Gets or sets the control templates.
    /// </summary>
    /// <value>
    /// The control templates.
    /// </value>
    public Dictionary<Control, string> ControlTemplates { get; set; } = new Dictionary<Control, string>();
    /// <summary>
    /// Gets or sets the type templates.
    /// </summary>
    /// <value>
    /// The type templates.
    /// </value>
    public Dictionary<Type, string> TypeTemplates { get; set; } = new Dictionary<Type, string>();
    /// <summary>
    /// Gets or sets the default HTML attributes.
    /// </summary>
    /// <value>
    /// The default HTML attributes.
    /// </value>
    public object DefaultHtmlAttributes { get; set; }

    /// <summary>
    /// Templates for.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public string TemplateFor(string propertyName, Type type)
    {
      return TemplateForHelper(propertyName, type, Control.General);
    }

    /// <summary>
    /// Templates for type.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public string TemplateForType(string propertyName, Type type)
    {
      return TemplateForHelper(propertyName, type);
    }

    /// <summary>
    /// CheckBoxes the template for.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public string CheckBoxTemplateFor(string propertyName, Type type)
    {
      return TemplateForHelper(propertyName, type, Control.CheckBox);
    }

    /// <summary>
    /// RadioButtons the template for.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public string RadioButtonTemplateFor(string propertyName, Type type)
    {
      return TemplateForHelper(propertyName, type, Control.RadioButton);
    }

    /// <summary>
    /// Replaces the place holder.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public string ReplacePlaceHolder(string template, string value)
    {
      return template.Replace(PlaceHolderToken, value);
    }

    private string TemplateForHelper(string propertyName, Type type, Control? control = null)
    {
      var template = PropertyTemplateFor(propertyName);
      if (!string.IsNullOrEmpty(template)) return template;

      if (control.HasValue)
      {
        template = ControlTemplateFor(control.GetValueOrDefault(Control.General));
        if (!string.IsNullOrEmpty(template)) return template;
      }

      template = TypeTemplateFor(type);
      return !string.IsNullOrEmpty(template) ? template : ControlTemplateFor(Control.General);
    }

    private string PropertyTemplateFor(string propertyName)
    {
      if (string.IsNullOrEmpty(propertyName)) return string.Empty;
      return PropertyTemplates.ContainsKey(propertyName) ? PropertyTemplates[propertyName] : string.Empty;
    }

    private string TypeTemplateFor(Type type)
    {
      return TypeTemplates.ContainsKey(type) ? TypeTemplates[type] : string.Empty;
    }

    private string ControlTemplateFor(Control control)
    {
      return ControlTemplates.ContainsKey(control) ? ControlTemplates[control] : string.Empty;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is valid.
    /// </summary>
    /// <value>
    /// {D255958A-8513-4226-94B9-080D98F904A1}  <c>true</c> if this instance is valid; otherwise, <c>false</c>.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsValid => true;

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
