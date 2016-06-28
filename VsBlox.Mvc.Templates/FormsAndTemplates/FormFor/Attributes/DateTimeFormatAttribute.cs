using System;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Attributes
{
  /// <summary>
  /// Format DataAnnotation
  /// Specify DateTime format for model binding 
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class DateTimeFormatAttribute : Attribute
  {
    public string Format { get; set; }

    public DateTimeFormatAttribute(string format)
    {
      Format = string.IsNullOrEmpty(format) ? "iso" : format;
    }
  }
}
