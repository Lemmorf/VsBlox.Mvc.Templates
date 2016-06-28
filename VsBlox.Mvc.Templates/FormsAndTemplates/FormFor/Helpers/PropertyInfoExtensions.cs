using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper.Configuration;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Attributes;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers
{
  public static class PropertyInfoExtensions
  {
    public static bool HasUiHint(this PropertyInfo propertyInfo)
    {
      return propertyInfo.GetCustomAttributes(typeof(UIHintAttribute), true).FirstOrDefault() != null;
    }

    public static DataType? GetDataType(this PropertyInfo propertyInfo)
    {
      var dataType = propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), true).Cast<DataTypeAttribute>().FirstOrDefault();
      return dataType?.DataType;
    }

    public static bool IsReadOnly(this PropertyInfo propertyInfo)
    {
      var readOnly = propertyInfo.GetCustomAttributes(typeof(ReadOnlyAttribute), true).Cast<ReadOnlyAttribute>().FirstOrDefault();
      return readOnly?.IsReadOnly ?? false;
    }

    public static bool IsDate(this PropertyInfo propertyInfo)
    {
      if (propertyInfo.GetPropertyType() != typeof(DateTime)) return false;

      var dataType = propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), true).Cast<DataTypeAttribute>().FirstOrDefault();
      if (dataType == null) return true;

      return dataType.DataType == DataType.Date;
    }

    public static bool IsDateTime(this PropertyInfo propertyInfo)
    {
      if (propertyInfo.GetPropertyType() != typeof(DateTime)) return false;

      var dataType = propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), true).Cast<DataTypeAttribute>().FirstOrDefault();
      if (dataType == null) return true;

      return dataType.DataType == DataType.DateTime;
    }

    public static string DataFormatString(this PropertyInfo propertyInfo)
    {
      if (propertyInfo.GetPropertyType() != typeof(DateTime)) return null;

      var displayFormat = propertyInfo.GetCustomAttributes(typeof(DisplayFormatAttribute), true).Cast<DisplayFormatAttribute>().FirstOrDefault();
      return displayFormat?.DataFormatString;
    }

    public static string DateTimeFormat(this PropertyInfo propertyInfo)
    {
      if (propertyInfo.GetPropertyType() != typeof(DateTime)) return null;
      var dateTimeformat = propertyInfo.GetCustomAttributes(typeof(DateTimeFormatAttribute), true).Cast<DateTimeFormatAttribute>().FirstOrDefault();
      return dateTimeformat?.Format;
    }

    public static bool IsTime(this PropertyInfo propertyInfo)
    {
      if (propertyInfo.GetPropertyType() != typeof(DateTime)) return false;

      var dataType = propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), true).Cast<DataTypeAttribute>().FirstOrDefault();

      return dataType?.DataType == DataType.Time;
    }

    public static bool IsHidden(this PropertyInfo propertyInfo)
    {
      return propertyInfo.GetCustomAttributes(typeof(HiddenInputAttribute), true).FirstOrDefault() != null;
    }

    public static bool IgnoreInForm(this PropertyInfo propertyInfo)
    {
      return propertyInfo.GetCustomAttributes(typeof(IgnoreInFormAttribute), true).FirstOrDefault() != null;
    }

    public static bool IsDisabled(this PropertyInfo propertyInfo)
    {
      return propertyInfo.GetCustomAttributes(typeof(DisabledAttribute), true).FirstOrDefault() != null;
    }

    public static bool IsRequired(this PropertyInfo propertyInfo)
    {
      return propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true).FirstOrDefault() != null;
    }

    public static bool AsRadiobuttons(this PropertyInfo propertyInfo)
    {
      return propertyInfo.GetCustomAttributes(typeof(RadiobuttonsAttribute), true).FirstOrDefault() != null;
    }

    public static string InputTagId(this PropertyInfo propertyInfo, string prefix)
    {
      return string.IsNullOrEmpty(prefix) ? propertyInfo.Name : $"{prefix}_{propertyInfo.Name}";
    }

    public static RouteValueDictionary HtmlAttributes(this PropertyInfo propertyInfo, object defaultHtmlAttributes, object htmlAttributesToMerge1 = null, object htmlAttributesToMerge2 = null)
    {
      var routeValues = propertyInfo.HtmlAttributes();

      if (defaultHtmlAttributes != null) routeValues = MergeHtmlAttributes(routeValues, defaultHtmlAttributes);
      if (htmlAttributesToMerge1 != null) routeValues = MergeHtmlAttributes(routeValues, htmlAttributesToMerge1);
      if (htmlAttributesToMerge2 != null) routeValues = MergeHtmlAttributes(routeValues, htmlAttributesToMerge2);

      return routeValues;
    }

    public static RouteValueDictionary HtmlAttributes(this PropertyInfo propertyInfo)
    {
      var htmlAttributes = propertyInfo.GetCustomAttributes(typeof(HtmlAttributesAttribute), true).Cast<HtmlAttributesAttribute>().FirstOrDefault();
      if (htmlAttributes == null) return null;

      var dict = new RouteValueDictionary();
      foreach (var attribute in htmlAttributes.HtmlAttributes.Split(','))
      {
        var kv = attribute.Split('=', ':');
        if (kv.Length != 2) throw new InvalidOperationException();
        dict.Add(kv[0].Trim().Trim('\'').Trim('\"'), kv[1].Trim().Trim('\'').Trim('\"').Trim(';'));
      }

      return dict;
    }

    public static RouteValueDictionary HtmlAttributes(this Type type)
    {
      var htmlAttributes = type.GetCustomAttributes(typeof(HtmlAttributesAttribute), true).Cast<HtmlAttributesAttribute>().FirstOrDefault();
      if (htmlAttributes == null) return null;

      var dict = new RouteValueDictionary();
      foreach (var attribute in htmlAttributes.HtmlAttributes.Split(','))
      {
        var kv = attribute.Split('=');
        if (kv.Length != 2) throw new InvalidOperationException();
        dict.Add(kv[0].Trim().Trim('\'').Trim('\"'), kv[1].Trim().Trim('\'').Trim('\"'));
      }

      return dict;
    }

    public static RouteValueDictionary MergeHtmlAttributes(params object[] routeValuesObjects)
    {
      if (routeValuesObjects == null) return null;
      if (routeValuesObjects.Length <= 1) return new RouteValueDictionary(routeValuesObjects[0]);

      var result = new RouteValueDictionary();

      foreach (var routeValues in routeValuesObjects)
      {
        var values = routeValues is RouteValueDictionary ? routeValues as RouteValueDictionary : new RouteValueDictionary(routeValues);
        foreach (var item in values)
        {
          result[item.Key] = item.Value;
        }
      }

      return result;
    }

    public static Type GetPropertyType(this PropertyInfo propertyInfo)
    {
      return propertyInfo.IsNullablePropertyType() ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;
    }

    public static bool IsNullablePropertyType(this PropertyInfo propertyInfo)
    {
      return propertyInfo.PropertyType.IsNullableType();
    }
  }
}
