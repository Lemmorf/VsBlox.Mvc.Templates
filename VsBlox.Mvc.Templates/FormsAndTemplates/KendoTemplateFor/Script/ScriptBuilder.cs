using System;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Script.Events;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Script
{
  /// <summary>
  /// Builder for form info container
  /// </summary>
  public class ScriptBuilder<T> : IHideObjectMembers
  {
    /// <summary>
    /// Gets or sets the script.
    /// </summary>
    /// <value>
    /// The script.
    /// </value>
    protected Script<T> Script { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptBuilder{T}"/> class.
    /// </summary>
    /// <param name="script">The script.</param>
    public ScriptBuilder(Script<T> script) { Script = script; }

    /// <summary>
    /// Namespaces the specified namespace.
    /// </summary>
    /// <param name="namespace">The namespace.</param>
    /// <returns></returns>
    public ScriptBuilder<T> Namespace(string @namespace) { Script.Namespace = @namespace; return this; }
    /// <summary>
    /// Functions the name.
    /// </summary>
    /// <param name="functionName">Name of the function.</param>
    /// <returns></returns>
    public ScriptBuilder<T> FunctionName(string functionName) { Script.FunctionName = functionName; return this; }

    /// <summary>
    /// Eventses the specified configurator.
    /// </summary>
    /// <param name="configurator">The configurator.</param>
    /// <returns></returns>
    public ScriptBuilder<T> Events(Action<EventsBuilder<T>> configurator)
    {
      Script.Events = new Events.Events();
      configurator(new EventsBuilder<T>(Script.Events));
      return this;
    }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
