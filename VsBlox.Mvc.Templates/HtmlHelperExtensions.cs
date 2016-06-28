using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor;
using VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor;
using VsBlox.Mvc.Templates.JsObjectFor;

namespace VsBlox.Mvc.Templates
{
  public static class HtmlHelperExtensions
  {
    public static KendoTemplateForBuilder<T> KendoTemplateFor<T>(this HtmlHelper helper) where T : class
    {
      return  new KendoTemplateForBuilder<T>(helper);
    }

    public static FormForBuilder<T> FormFor<T>(this HtmlHelper helper) where T : class
    {
      return new FormForBuilder<T>(helper);
    }

    public static FormBodyForBuilder<T> FormBodyFor<T>(this HtmlHelper helper) where T : class
    {
      return new FormBodyForBuilder<T>(helper);
    } 

    public static Builder1<T> JsObjectFor<T>(this HtmlHelper helper, bool asObject, bool lowercaseNames = false, string namePrefix = "") => new Builder1<T>(helper, asObject, lowercaseNames, namePrefix);
    public static Builder2<T1, T2> JsObjectFor<T1, T2>(this HtmlHelper helper, bool asObject, bool lowercaseNames = false, string namePrefix = "") => new Builder2<T1, T2>(helper, asObject, lowercaseNames, namePrefix);
    public static Builder3<T1, T2, T3> JsObjectFor<T1, T2, T3>(this HtmlHelper helper, bool asObject, bool lowercaseNames = false, string namePrefix = "") => new Builder3<T1, T2, T3>(helper, asObject, lowercaseNames, namePrefix);
    public static Builder4<T1, T2, T3, T4> JsObjectFor<T1, T2, T3, T4>(this HtmlHelper helper, bool asObject, bool lowercaseNames = false, string namePrefix = "") => new Builder4<T1, T2, T3, T4>(helper, asObject, lowercaseNames, namePrefix);
    public static Builder5<T1, T2, T3, T4, T5> JsObjectFor<T1, T2, T3, T4, T5>(this HtmlHelper helper, bool asObject, bool lowercaseNames = false, string namePrefix = "") => new Builder5<T1, T2, T3, T4, T5>(helper, asObject, lowercaseNames, namePrefix);
    public static Builder6<T1, T2, T3, T4, T5, T6> JsObjectFor<T1, T2, T3, T4, T5, T6>(this HtmlHelper helper, bool asObject, bool lowercaseNames = false, string namePrefix = "") => new Builder6<T1, T2, T3, T4, T5, T6>(helper, asObject, lowercaseNames, namePrefix);
  }
}
