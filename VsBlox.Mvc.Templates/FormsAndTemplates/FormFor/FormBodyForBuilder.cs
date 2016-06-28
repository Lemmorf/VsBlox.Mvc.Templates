using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Render;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor
{
  public class FormBodyForBuilder<T> : IHideObjectMembers, IHtmlString where T : class
  {
    private readonly HtmlHelper _html;
    private Body<T> _body;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormBodyForBuilder{T}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    public FormBodyForBuilder(HtmlHelper html)
    {
      _html = html;
    }
    public FormBodyForBuilder<T> Body(Action<BodyBuilder<T>> configurator)
    {
      _body = new Body<T>();
      configurator(new BodyBuilder<T>(_body));
      return this;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    private bool IsValid => _body != null && _body.IsValid;

    string IHtmlString.ToHtmlString()
    {
      Debug.Assert(IsValid);
      return RenderContext<T>.Render(_html, _body.Model.Data, helper => new RenderBody<T>(helper, _body).ToString());
    }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
