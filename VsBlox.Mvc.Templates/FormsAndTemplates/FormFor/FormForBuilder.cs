using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor
{
  public class FormForBuilder<T> : IHideObjectMembers, IHtmlString where T : class
  {
    private readonly HtmlHelper _html;
    private Form<T> _form;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormForBuilder{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    public FormForBuilder(HtmlHelper html)
    {
      _html = html;
    }
    public FormForBuilder<T> Form(Action<FormBuilder<T>> configurator)
    {
      _form = new Form<T>();
      configurator(new FormBuilder<T>(_form));
      return this;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    private bool IsValid => _form != null && _form.IsValid;

    string IHtmlString.ToHtmlString()
    {
      Debug.Assert(IsValid);
      return RenderContext<T>.Render(_html, _form.Model.Data, helper => new RenderForm<T>(helper, _form).ToString());
    }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
