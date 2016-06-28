using System;
using System.ComponentModel;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Template
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class Template<T> : IHideObjectMembers
  {
    private string _prefix;
    /// <summary>
    /// Gets or sets the prefix.
    /// </summary>
    /// <value>
    /// The prefix.
    /// </value>
    public string Prefix
    {
      get { return string.IsNullOrEmpty(_prefix) ? $"{Name}" : _prefix; }
      set { _prefix = value; }
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; } = "template-" + Guid.NewGuid();

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    public string Title { get; set; } = "{0}";

    /// <summary>
    /// Gets or sets the form.
    /// </summary>
    /// <value>
    /// The form.
    /// </value>
    public Form.Form<T> Form { get; set; }

    /// <summary>
    /// Gets or sets the header template.
    /// </summary>
    /// <value>
    /// The header template.
    /// </value>
    public string HeaderTemplate { get; set; }

    /// <summary>
    /// Gets or sets the footer template.
    /// </summary>
    /// <value>
    /// The footer template.
    /// </value>
    public string FooterTemplate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is valid.
    /// </summary>
    /// <value>
    /// {D255958A-8513-4226-94B9-080D98F904A1}  <c>true</c> if this instance is valid; otherwise, <c>false</c>.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsValid =>
          !string.IsNullOrEmpty(Name) &&
          !string.IsNullOrEmpty(Prefix) &&
          Form != null && Form.IsValid;

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
