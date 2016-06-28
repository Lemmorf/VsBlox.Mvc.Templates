using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using VsBlox.Mvc.Templates.FormsAndTemplates.Config;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;
using DynamicExpression = VsBlox.Mvc.Templates.FormsAndTemplates.Helpers.DynamicExpression;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.LambdaHtmlHelper
{
  /// <summary>
  /// Lambda helper extensions
  /// </summary>
  public static class LambdaHtmlHelper
  {
    /// <summary>
    /// Drops down list for.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="html">The HTML.</param>
    /// <param name="propertyInfo">The property information.</param>
    /// <param name="selectList">The select list.</param>
    /// <param name="optionLabel">The option label.</param>
    /// <param name="htmlAttributes">The HTML attributes.</param>
    /// <returns></returns>
    public static HtmlString DropDownListFor<TModel>(HtmlHelper<TModel> html, PropertyInfo propertyInfo, IEnumerable<SelectListItem> selectList, string optionLabel, RouteValueDictionary htmlAttributes)
    {
      var lambda = DynamicExpression.ParseLambda(typeof(TModel), propertyInfo.PropertyType, "m => m." + propertyInfo.Name, null);

      var methods = GetMethods(typeof(SelectExtensions), "DropDownListFor", "TProperty");
      var method = methods.Where(x =>
      {
        var parms = x.GetParameters();
        return parms.Length == 5 && parms[2].ParameterType.Name == "IEnumerable`1" && parms[3].ParameterType.Name == "String" && parms[4].ParameterType.Name == "IDictionary`2";
      }).First();

      if (method == null) return new HtmlString(string.Empty);

      method = method.MakeGenericMethod(typeof(TModel), lambda.Body.Type);
      var htmlString = method.Invoke(null, new [] { html, lambda, selectList, optionLabel, (object)htmlAttributes }).ToString();
      return new HtmlString(HttpUtility.HtmlDecode(htmlString));
    }

    /// <summary>
    /// Drops down group list for.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="html">The HTML.</param>
    /// <param name="propertyInfo">The property information.</param>
    /// <param name="selectList">The select list.</param>
    /// <param name="optionLabel">The option label.</param>
    /// <param name="htmlAttributes">The HTML attributes.</param>
    /// <returns></returns>
    public static HtmlString DropDownGroupListFor<TModel>(HtmlHelper<TModel> html, PropertyInfo propertyInfo, IEnumerable<GroupedSelectListItem> selectList, string optionLabel, RouteValueDictionary htmlAttributes)
    {
      var lambda = DynamicExpression.ParseLambda(typeof(TModel), propertyInfo.PropertyType, "m => m." + propertyInfo.Name, null);

      var methods = GetMethods(typeof(DropDownListExtensions), "DropDownGroupListFor", "TProperty");
      var method = methods.Where(x =>
      {
        var parms = x.GetParameters();
        return parms.Length == 5 && parms[2].ParameterType.Name == "IEnumerable`1" && parms[3].ParameterType.Name == "String" && parms[4].ParameterType.Name == "IDictionary`2";
      }).First();
      
      if (method == null) return new HtmlString(string.Empty);

      method = method.MakeGenericMethod(typeof(TModel), lambda.Body.Type);
      var htmlString = method.Invoke(null, new[] { html, lambda, selectList, optionLabel, (object)htmlAttributes }).ToString();
      return new HtmlString(HttpUtility.HtmlDecode(htmlString));
    }

    /// <summary>
    /// Texts the box for.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="html">The HTML.</param>
    /// <param name="propertyInfo">The property information.</param>
    /// <param name="htmlAttributes">The HTML attributes.</param>
    /// <returns></returns>
    public static HtmlString TextBoxFor<TModel>(HtmlHelper<TModel> html, PropertyInfo propertyInfo, RouteValueDictionary htmlAttributes)
    {
      var lambda = DynamicExpression.ParseLambda(typeof(TModel), propertyInfo.PropertyType, "m => m." + propertyInfo.Name, null);

      var methods = GetMethods(typeof(InputExtensions), "TextBoxFor", "TProperty");
      var method = methods.Where(x =>
      {
        var parms = x.GetParameters();
        //return parms.Length == 3 && parms[2].ParameterType.Name == "Object";
        return parms.Length == 3 && parms[2].ParameterType.Name == "IDictionary`2";
      }).First();

      if (method == null) return new HtmlString(string.Empty);

      method = method.MakeGenericMethod(typeof(TModel), lambda.Body.Type);
      var htmlString = method.Invoke(null, new[] { html, lambda, (object)htmlAttributes }).ToString();
      return new HtmlString(HttpUtility.HtmlDecode(htmlString));
    }

    /// <summary>
    /// Hiddens for.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="html">The HTML.</param>
    /// <param name="propertyInfo">The property information.</param>
    /// <param name="htmlAttributes">The HTML attributes.</param>
    /// <returns></returns>
    public static HtmlString HiddenFor<TModel>(HtmlHelper<TModel> html, PropertyInfo propertyInfo, RouteValueDictionary htmlAttributes)
    {
      var lambda = DynamicExpression.ParseLambda(typeof(TModel), propertyInfo.PropertyType, "m => m." + propertyInfo.Name, null);

      var methods = GetMethods(typeof(InputExtensions), "HiddenFor", "TProperty");
      var method = methods.Where(x =>
      {
        var parms = x.GetParameters();
        return parms.Length == 3 && parms[2].ParameterType.Name == "IDictionary`2";
      }).First();

      if (method == null) return new HtmlString(string.Empty);

      method = method.MakeGenericMethod(typeof(TModel), lambda.Body.Type);
      var htmlString = method.Invoke(null, new [] { html, lambda, (object)htmlAttributes }).ToString();
      return new HtmlString(HttpUtility.HtmlDecode(htmlString));
    }

    /// <summary>
    /// CheckBoxes for.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="html">The HTML.</param>
    /// <param name="propertyInfo">The property information.</param>
    /// <param name="htmlAttributes">The HTML attributes.</param>
    /// <returns></returns>
    public static HtmlString CheckBoxFor<TModel>(HtmlHelper<TModel> html, PropertyInfo propertyInfo, RouteValueDictionary htmlAttributes)
    {
      var lambda = DynamicExpression.ParseLambda(typeof(TModel), propertyInfo.PropertyType, "m => m." + propertyInfo.Name, null);

      var methods = GetMethods(typeof(InputExtensions), "CheckBoxFor");
      var method = methods.Where(x =>
      {
        var parms = x.GetParameters();
        return parms.Length == 3 && parms[2].ParameterType.Name == "IDictionary`2";
      }).FirstOrDefault();

      if (method == null) return new HtmlString(string.Empty);

      method = method.MakeGenericMethod(typeof(TModel));
      var htmlString = method.Invoke(null, new[] { html, lambda, (object)htmlAttributes }).ToString();
      return new HtmlString(HttpUtility.HtmlDecode(htmlString));
    }

    /// <summary>
    /// Labels for.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="html">The HTML.</param>
    /// <param name="propertyInfo">The property information.</param>
    /// <returns></returns>
    public static HtmlString LabelFor<TModel>(HtmlHelper<TModel> html, PropertyInfo propertyInfo)
    {
      var lambda = DynamicExpression.ParseLambda(typeof(TModel), propertyInfo.PropertyType, "m => m." + propertyInfo.Name, null);

      var methods = GetMethods(typeof(LabelExtensions), "LabelFor", "TValue");
      var method = methods.Where(x =>
      {
        var parms = x.GetParameters();
        return parms.Length == 2;
      }).First();

      if (method == null) return new HtmlString(string.Empty);

      method = method.MakeGenericMethod(typeof(TModel), lambda.Body.Type);
      var htmlString = method.Invoke(null, new object[] { html, lambda }).ToString();
      return new HtmlString(HttpUtility.HtmlDecode(htmlString));
    }

    /// <summary>
    /// Labels the required for.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="html">The HTML.</param>
    /// <param name="propertyInfo">The property information.</param>
    /// <returns></returns>
    public static HtmlString LabelRequiredFor<TModel>(HtmlHelper<TModel> html, PropertyInfo propertyInfo)
    {
      var displayName = DisplayNameFor(html, propertyInfo);

      var labelTag = new TagBuilder("label");
      labelTag.MergeAttribute("for", propertyInfo.Name);
      labelTag.InnerHtml = displayName.ToString();
      if (!propertyInfo.IsRequired()) return new HtmlString(labelTag.ToString());

      labelTag.InnerHtml += " " + TemplateConfig.Instance.MandatoryIndicator;

      return new HtmlString(labelTag.ToString());
    }

    /// <summary>
    /// Displays the name for.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="html">The HTML.</param>
    /// <param name="propertyInfo">The property information.</param>
    /// <returns></returns>
    public static HtmlString DisplayNameFor<TModel>(HtmlHelper<TModel> html, PropertyInfo propertyInfo)
    {
      var lambda = DynamicExpression.ParseLambda(typeof(TModel), propertyInfo.PropertyType, "m => m." + propertyInfo.Name, null);

      var methods = GetMethods(typeof(DisplayNameExtensions), "DisplayNameFor", "TValue");
      var method = methods.Where(x =>
      {
        var parms = x.GetParameters();
        return parms.Length == 2;
      }).First();

      if (method == null) return new HtmlString(string.Empty);

      method = method.MakeGenericMethod(typeof(TModel), lambda.Body.Type);
      var htmlString = method.Invoke(null, new object[] { html, lambda }).ToString();
      return new HtmlString(HttpUtility.HtmlDecode(htmlString));
    }

    /// <summary>
    /// Displays the name.
    /// </summary>
    /// <param name="propertyInfo">The property information.</param>
    /// <returns></returns>
    public static string DisplayName(PropertyInfo propertyInfo)
    {
      var displayName = string.Empty;

      var displayNameAttribute = propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
      if (displayNameAttribute != null) displayName = ((DisplayNameAttribute) displayNameAttribute).DisplayName;
      if (string.IsNullOrEmpty(displayName))
      {
        var displayAttribute = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
        if (displayAttribute != null) displayName = ((DisplayAttribute)displayAttribute).Name;
      }

      return string.IsNullOrEmpty(displayName) ? propertyInfo.Name : displayName;
    }

    /// <summary>
    /// Properties the name from lambda expression.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static string PropertyNameFromLambdaExpression<TParameter, TValue>(Expression<Func<TParameter, TValue>> expression)
    {
      if (expression == null) throw new ArgumentNullException(nameof(expression));
      if (expression.Body.NodeType != ExpressionType.MemberAccess) throw new InvalidOperationException(nameof(expression));

      var memberExpression = (MemberExpression)expression.Body;
      var propertyName = memberExpression.Member is PropertyInfo ? memberExpression.Member.Name : string.Empty;
      //var containerType = memberExpression.Expression.Type;

      return propertyName;
    }

    /// <summary>
    /// Gets the methods.
    /// </summary>
    /// <param name="extensionType">Type of the extension.</param>
    /// <param name="functionName">Name of the function.</param>
    /// <param name="genericPropertyName">Name of the generic property.</param>
    /// <returns></returns>
    private static MethodInfo[] GetMethods(Type extensionType, string functionName, string genericPropertyName = null)
    {
      var methods = extensionType.GetMethods();
      var result = methods.Where(m =>
      {

        var parms = m.GetParameters();
        if (parms.Length < 2) return false;

        // No support for htmlHelper<IEnumerable<TModel>>
        if (parms[0].ParameterType.GetGenericArguments().Any(a => a.Name == "IEnumerable`1")) return false;

        var args = m.GetGenericArguments();

        if (string.IsNullOrEmpty(genericPropertyName))
        {
          return m.IsGenericMethod &&
                 args.Length == 1 && args[0].Name == "TModel" && 
                 parms[0].ParameterType.Name == "HtmlHelper`1" && parms[1].ParameterType.Name == "Expression`1";
        }
        return m.IsGenericMethod &&
               args.Length == 2 && args[0].Name == "TModel" && args[1].Name == genericPropertyName && m.Name == functionName &&
               parms[0].ParameterType.Name == "HtmlHelper`1" && parms[1].ParameterType.Name == "Expression`1";
      });

      return result.ToArray();
    }

    //private static void InspectMethods(Type extensionType, string functionName)
    //{
    //  var methods = extensionType.GetMethods();

    //  foreach (var x in methods)
    //  {
    //    if (x.Name != functionName) continue;

    //    var args = x.GetGenericArguments();
    //    var parms = x.GetParameters();

    //    Debug.WriteLine($"Generic arguments for {functionName} in {extensionType.Name}:");
    //    foreach (var arg in args)
    //    {
    //      Debug.WriteLine(arg.Name);
    //    }

    //    Debug.WriteLine($"Parameters for {functionName} in {extensionType.Name}:");
    //    foreach (var parm in parms)
    //    {
    //      Debug.WriteLine(parm.ParameterType.Name);
    //    }
    //  }
    //}
  }
}
