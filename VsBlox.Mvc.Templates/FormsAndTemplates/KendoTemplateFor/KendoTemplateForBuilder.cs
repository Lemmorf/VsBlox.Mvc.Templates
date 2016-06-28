using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Buttons.Button;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Render;
using VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Script;
using VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Template;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor
{
  /// <summary>
  /// Creates the fluent API the modal widget 
  /// </summary>
  public class KendoTemplateForBuilder<T> : IHideObjectMembers, IHtmlString where T : class
  {
    private readonly HtmlHelper _html;

    private Template<T> _template;
    private Script<T> _script; 

    /// <summary>
    /// Initializes a new instance of the <see cref="KendoTemplateForBuilder{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    public KendoTemplateForBuilder(HtmlHelper html)
    {
      _html = html;
    }

    /// <summary>
    /// Templates the specified configurator.
    /// </summary>
    /// <param name="configurator">The configurator.</param>
    /// <returns></returns>
    public KendoTemplateForBuilder<T> Template(Action<TemplateBuilder<T>> configurator)
    {
      _template = new Template<T>();
      configurator(new TemplateBuilder<T>(_template));
      return this;
    }

    /// <summary>
    /// Scripts the specified configurator.
    /// </summary>
    /// <param name="configurator">The configurator.</param>
    /// <returns></returns>
    public KendoTemplateForBuilder<T> Script(Action<ScriptBuilder<T>> configurator)
    {
      _script = new Script<T>();
      configurator(new ScriptBuilder<T>(_script));
      return this;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    private bool IsValid => 
      _template != null && _template.IsValid &&
      _script != null && _script.IsValid;

    string IHtmlString.ToHtmlString()
    {
      Debug.Assert(IsValid);

      return RenderContext<T>.Render(_html, null, helper =>
      {
        var sb = new StringBuilder();

        sb.Append(new RenderWindow<T>(helper, _template).ToString());
        
        if (!_template.Form.Buttons.ButtonList.Any())
        {
          _template.Form.Buttons.ButtonList.Add(new Button { Name = _template.Prefix + "btnOk", Text = "Ok", Type = Button.ButtonType.Submit });
          _template.Form.Buttons.ButtonList.Add(new Button { Name = _template.Prefix + "btnCancel", Text = "Cancel", Type = Button.ButtonType.Cancel });
        }

        sb.Append(new RenderTemplate<T>(helper, _template).ToString());
        sb.Append(new RenderScript<T>(helper, _template, _script).ToString());

        return sb.ToString();
      });
    }

    Type IHideObjectMembers.GetType()
    {
      return GetType();
    }
  }
}
