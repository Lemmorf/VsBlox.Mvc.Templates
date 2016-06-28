using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.JsObjectFor
{
  /// <summary>
  /// Creates the fluent API the modal widget 
  /// </summary>
  public class JsObjectBuilder
  {
    /// <summary>
    /// Gets or sets a value indicating whether [as object].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [as object]; otherwise, <c>false</c>.
    /// </value>
    public bool AsObject { get; set; }

    /// <summary>
    /// Gets or sets the name prefix.
    /// </summary>
    /// <value>
    /// The name prefix.
    /// </value>
    public string NamePrefix { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [lowercase names].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [lowercase names]; otherwise, <c>false</c>.
    /// </value>
    public bool LowercaseNames { get; set; }

    /// <summary>
    /// The properties
    /// </summary>
    public Dictionary<string, Type> Properties = new Dictionary<string, Type>();

    /// <summary>
    /// Adds the properties.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <exception cref="System.InvalidOperationException">$Property {name} allready exists with a diferent type ({Properties[name]} => {propertyType})</exception>
    public void AddProperties(Type type)
    {
      foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead))
      {
        if (propertyInfo.IgnoreInForm()) continue;

        var name = LowercaseNames ? propertyInfo.Name.ToLowerInvariant() : propertyInfo.Name;
        var propertyType = propertyInfo.GetPropertyType();

        if (Properties.ContainsKey(name))
        {
          if (Properties[name] == propertyType) continue; // same (key, value)
          throw new InvalidOperationException($"Property {name} allready exists with a diferent type ({Properties[name]} => {propertyType})");
        }

        Properties.Add(name, propertyType);
      }
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      var sb = new StringBuilder();

      var delimiter = AsObject ? ":" : " =";
      var separator = AsObject ? "," : ";";

      sb.AppendLine("{");

      foreach (var property in Properties)
      {
        var name = property.Key;
        var propertyType = property.Value;

        if (TypeHelper.IsSameOrSubclass(typeof (string), propertyType))
        {
          sb.AppendLine($"\t\t\t{NamePrefix}{name}{delimiter} \"\"{separator}");
        }
        else if (TypeHelper.IsSameOrSubclass(typeof (int), propertyType))
        {
          sb.AppendLine($"\t\t\t{NamePrefix}{name}{delimiter} 0{separator}");
        }
        else if (TypeHelper.IsSameOrSubclass(typeof (DateTime), propertyType))
        {
          sb.AppendLine($"\t\t\t{NamePrefix}{name}{delimiter} new Date(){separator}");
        }
        else if (TypeHelper.IsSameOrSubclass(typeof (Enum), propertyType))
        {
          sb.AppendLine($"\t\t\t{NamePrefix}{name}{delimiter} 0{separator}");
        }
        else if (TypeHelper.IsSameOrSubclass(typeof (bool), propertyType))
        {
          sb.AppendLine($"\t\t\t{NamePrefix}{name}{delimiter} false{separator}");
        }
        else if (TypeHelper.IsSameOrSubclass(typeof (byte[]), propertyType))
        {
          sb.AppendLine($"\t\t\t{NamePrefix}{name}{delimiter} \"\"{separator}");
        }
        else
        {
          throw new InvalidOperationException($"{name} of type {propertyType} is unknown!");
        }
      }

      sb.Append("\t\t}");

      return sb.ToString();
    }
  }
}
