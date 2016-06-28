using System;
using System.Web;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.JsObjectFor
{
  /// <summary>
  /// Creates the fluent API the modal widget 
  /// </summary>
  public class Builder2<T1, T2> : IHideObjectMembers, IHtmlString
  {
    private readonly HtmlHelper _html;
    private readonly JsObjectBuilder _objectBuilder = new JsObjectBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="Builder2{T1, T2}"/> class.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <param name="asObject">if set to <c>true</c> [as object].</param>
    /// <param name="lowercaseNames">if set to <c>true</c> [lowercase names].</param>
    /// <param name="namePrefix">The name prefix.</param>
    public Builder2(HtmlHelper html, bool asObject, bool lowercaseNames = false, string namePrefix = "")
    {
      _html = html;
      _objectBuilder.AsObject = asObject;
      _objectBuilder.NamePrefix = namePrefix;
      _objectBuilder.LowercaseNames = lowercaseNames;
    }

    /// <summary>
    /// Renders the modal widget (both link and dialog)
    /// </summary>
    /// <returns>
    /// An HTML-encoded string.
    /// </returns>
    string IHtmlString.ToHtmlString()
    {
      _objectBuilder.AddProperties(typeof(T1));
      _objectBuilder.AddProperties(typeof(T2));
      return _objectBuilder.ToString();
    }

    Type IHideObjectMembers.GetType()
    {
      return GetType();
    }
  }
}
