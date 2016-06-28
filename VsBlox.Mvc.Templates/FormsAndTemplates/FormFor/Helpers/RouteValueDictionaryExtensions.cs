using System.Web.Routing;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers
{
  public static class RouteValueDictionaryExtensions
  {
    public static void AddCssClass(this RouteValueDictionary htmlAttributes, string cssClass)
    {
      if (htmlAttributes == null || string.IsNullOrEmpty(cssClass)) return;
      
      if (htmlAttributes.ContainsKey("class")) cssClass += " " + htmlAttributes["class"] ;
      htmlAttributes["class"] = cssClass;
    }
  }
}
