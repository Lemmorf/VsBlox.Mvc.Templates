using System;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Attributes
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
  public class HtmlAttributesAttribute : Attribute
  {
    public string HtmlAttributes { get; set; }

    public HtmlAttributesAttribute(string htmlAttributes)
    {
      HtmlAttributes = htmlAttributes;
    }
  }
}