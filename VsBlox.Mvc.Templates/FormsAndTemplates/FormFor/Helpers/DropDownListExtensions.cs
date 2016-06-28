using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers
{
  public class GroupedSelectListItem : SelectListItem
  {
    public string GroupKey { get; set; }
    public string GroupName { get; set; }
  }

  public static class DropDownListExtensions
  {
    public static MvcHtmlString DropDownGroupListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
    {
      if (expression == null) throw new ArgumentNullException(nameof(expression));
      return DropDownListHelper(htmlHelper, ExpressionHelper.GetExpressionText(expression), selectList, optionLabel, htmlAttributes);
    }

    private static MvcHtmlString DropDownListHelper(HtmlHelper htmlHelper, string expression, IEnumerable<GroupedSelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
    {
      return SelectInternal(htmlHelper, optionLabel, expression, selectList, false /* allowMultiple */, htmlAttributes);
    }

    // Helper methods

    private static IEnumerable<GroupedSelectListItem> GetSelectData(this HtmlHelper htmlHelper, string name)
    {
      object o = null;
      if (htmlHelper.ViewData != null) { o = htmlHelper.ViewData.Eval(name); }
      if (o == null) { throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Missing Select Data")); }
      var selectList = o as IEnumerable<GroupedSelectListItem>;
      if (selectList == null) { throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Wrong Select DataType")); }
      return selectList;
    }

    private static string ListItemToOption(GroupedSelectListItem item)
    {
      if (item == null) throw new ArgumentNullException(nameof(item));

      var builder = new TagBuilder("option") { InnerHtml = HttpUtility.HtmlEncode(item.Text) };
      if (item.Value != null) { builder.Attributes["value"] = item.Value; }
      if (item.Selected) { builder.Attributes["selected"] = "selected"; }
      return builder.ToString(TagRenderMode.Normal);
    }

    private static MvcHtmlString SelectInternal(this HtmlHelper htmlHelper, string optionLabel, string name, IEnumerable<GroupedSelectListItem> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes)
    {
      name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
      if (string.IsNullOrEmpty(name)) { throw new ArgumentException("Null Or Empty", nameof(name)); }

      var usedViewData = false;
      // If we got a null selectList, try to use ViewData to get the list of items.
      if (selectList == null)
      {
        selectList = htmlHelper.GetSelectData(name);
        usedViewData = true;
      }

      var defaultValue = (allowMultiple) ? htmlHelper.GetModelStateValue(name, typeof(string[])) : htmlHelper.GetModelStateValue(name, typeof(string));

      // If we haven't already used ViewData to get the entire list of items then we need to
      // use the ViewData-supplied value before using the parameter-supplied value.
      if (!usedViewData)
      {
        if (defaultValue == null)
        {
          defaultValue = htmlHelper.ViewData.Eval(name);
        }
      }

      if (defaultValue != null)
      {
        var defaultValues = allowMultiple ? defaultValue as IEnumerable : new[] { defaultValue };
        var values = from object value in defaultValues select Convert.ToString(value, CultureInfo.CurrentCulture);
        var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
        var newSelectList = new List<GroupedSelectListItem>();

        foreach (var item in selectList)
        {
          item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
          newSelectList.Add(item);
        }
        selectList = newSelectList;
      }

      // Convert each ListItem to an <option> tag
      var listItemBuilder = new StringBuilder();

      // Make optionLabel the first item that gets rendered.
      if (optionLabel != null)
      {
        listItemBuilder.AppendLine(ListItemToOption(new GroupedSelectListItem { Text = optionLabel, Value = string.Empty, Selected = false }));
      }

      var groupSelectListItems = selectList as GroupedSelectListItem[] ?? selectList.ToArray();
      foreach (var group in groupSelectListItems.GroupBy(i => i.GroupKey))
      {
        var groupName = groupSelectListItems.Where(i => i.GroupKey == group.Key).Select(it => it.GroupName).FirstOrDefault();
        listItemBuilder.AppendLine($"<optgroup label=\"{groupName}\" value=\"{@group.Key}\">");
        foreach (var item in group)
        {
          listItemBuilder.AppendLine(ListItemToOption(item));
        }
        listItemBuilder.AppendLine("</optgroup>");
      }

      var tagBuilder = new TagBuilder("select")
      {
        InnerHtml = listItemBuilder.ToString()
      };
      tagBuilder.MergeAttributes(htmlAttributes);
      tagBuilder.MergeAttribute("name", name, true /* replaceExisting */);
      tagBuilder.GenerateId(name);
      if (allowMultiple)
      {
        tagBuilder.MergeAttribute("multiple", "multiple");
      }

      // If there are any errors for a named field, we add the css attribute.
      ModelState modelState;
      if (!htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
        return MvcHtmlString.Create(tagBuilder.ToString());
      if (modelState.Errors.Count > 0)
      {
        tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
      }

      return MvcHtmlString.Create(tagBuilder.ToString());
    }

    private static object GetModelStateValue(this HtmlHelper helper, string key, Type destinationType)
    {
      ModelState modelState;
      return !helper.ViewData.ModelState.TryGetValue(key, out modelState) ? null : modelState.Value?.ConvertTo(destinationType, null /* culture */);
    }
  }
}