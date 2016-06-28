using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Body.PropertyInfo
{
  /// <summary>
  /// Builder for form info container
  /// </summary>
  public class PropertyInfoBuilder<T> : IHideObjectMembers
  {
    protected PropertyInfo PropertyInfo { get; set; }

    public PropertyInfoBuilder(PropertyInfo propertyInfo) { PropertyInfo = propertyInfo; }

    public PropertyInfoBuilder<T> SelectListFor<TP>(Expression<Func<T, TP>> expression, IEnumerable<SelectListItem> selectList)
    {
      PropertyInfo.PropertyName = LambdaHtmlHelper.LambdaHtmlHelper.PropertyNameFromLambdaExpression(expression);
      PropertyInfo.SelectList = selectList;
      return this;
    }

    public PropertyInfoBuilder<T> GroupSelectListItem<TP>(Expression<Func<T, TP>> expression, IEnumerable<GroupedSelectListItem> groupSelectList)
    {
      PropertyInfo.PropertyName = LambdaHtmlHelper.LambdaHtmlHelper.PropertyNameFromLambdaExpression(expression);
      PropertyInfo.GroupSelectList = groupSelectList;
      return this;
    }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
