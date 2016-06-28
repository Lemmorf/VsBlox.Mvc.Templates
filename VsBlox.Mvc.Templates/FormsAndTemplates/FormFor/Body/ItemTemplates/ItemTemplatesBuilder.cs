using System;
using System.Linq.Expressions;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.ItemTemplates
{
  /// <summary>
  /// Builder for form info container
  /// </summary>
  public class ItemTemplatesBuilder<T> : IHideObjectMembers
  {
    protected ItemTemplates ItemTemplates { get; set; }

    public ItemTemplatesBuilder(ItemTemplates itemTemplates) { ItemTemplates = itemTemplates; }

    public ItemTemplatesBuilder<T> TemplateFor<TP>(Expression<Func<T, TP>> expression, string template = null)
    {
      if (string.IsNullOrEmpty(template)) template = ItemTemplates.PlaceHolderToken;
      var propertyName = LambdaHtmlHelper.LambdaHtmlHelper.PropertyNameFromLambdaExpression(expression);

      if (ItemTemplates.PropertyTemplates.ContainsKey(propertyName))
      {
        ItemTemplates.PropertyTemplates[propertyName] = template;
      }
      else
      {
        ItemTemplates.PropertyTemplates.Add(propertyName, template);
      }

      return this;
    }

    public ItemTemplatesBuilder<T> TemplateFor(Type type, string template = null)
    {
      if (string.IsNullOrEmpty(template)) template = ItemTemplates.PlaceHolderToken;

      if (ItemTemplates.TypeTemplates.ContainsKey(type))
      {
        ItemTemplates.TypeTemplates[type] = template;
      }
      else
      {
        ItemTemplates.TypeTemplates.Add(type, template);
      }

      return this;
    }

    public ItemTemplatesBuilder<T> TemplateFor(ItemTemplates.Control control, string template = null)
    {
      if (string.IsNullOrEmpty(template)) template = ItemTemplates.PlaceHolderToken;

      if (ItemTemplates.ControlTemplates.ContainsKey(control))
      {
        ItemTemplates.ControlTemplates[control] = template;
      }
      else
      {
        ItemTemplates.ControlTemplates.Add(control, template);
      }

      return this;
    }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
