using System;
using System.IO;
using System.Web.Mvc;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.Helpers
{
  public static class RenderContext<T> where T : class
  {
    public static string Render(HtmlHelper html, T model, Func<HtmlHelper<T>, string> render)
    {
      if (html == null || render == null) return string.Empty;

      using (var sw = new StringWriter())
      {
        var controllerContext = html.ViewContext.Controller.ControllerContext;
        //todo: hier nog wat op vinden!!
        var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, "_Placeholder");

        var orgModel = html.ViewData.Model;
        try
        {
          html.ViewData.Model = model;

          var viewContext = new ViewContext(html.ViewContext.Controller.ControllerContext, viewResult.View, html.ViewData, html.ViewContext.TempData, sw);
          var htmlHelper = new HtmlHelper<T>(viewContext, html.ViewDataContainer);
          
          sw.Write(render(htmlHelper));
        }
        finally
        {
          html.ViewData.Model = orgModel;
        }

        return sw.ToString();
      }
    }
  }
}
