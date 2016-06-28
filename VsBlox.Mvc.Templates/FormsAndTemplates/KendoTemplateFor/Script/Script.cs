using System;
using System.ComponentModel;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Script
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class Script<T> : IHideObjectMembers
  {
    /// <summary>
    /// Gets or sets the namespace.
    /// </summary>
    /// <value>
    /// The namespace.
    /// </value>
    public string Namespace { get; set; } = $"{typeof(T).Name}Window";
    /// <summary>
    /// Gets or sets the name of the function.
    /// </summary>
    /// <value>
    /// The name of the function.
    /// </value>
    public string FunctionName { get; set; }

    /// <summary>
    /// Gets or sets the events.
    /// </summary>
    /// <value>
    /// The events.
    /// </value>
    public Events.Events Events { get; set; } = new Events.Events();

    /// <summary>
    /// Gets or sets a value indicating whether this instance is valid.
    /// </summary>
    /// <value>
    /// {D255958A-8513-4226-94B9-080D98F904A1}  <c>true</c> if this instance is valid; otherwise, <c>false</c>.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsValid => !string.IsNullOrEmpty(Namespace) && !string.IsNullOrEmpty(FunctionName) && Events.IsValid;

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
