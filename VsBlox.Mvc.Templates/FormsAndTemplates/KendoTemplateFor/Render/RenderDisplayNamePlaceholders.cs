using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Render
{
  public class RenderDisplayNamePlaceholders<T>
  {
    private readonly string _template;

    private readonly Regex _regex = new Regex(@"(?<displayNamePlaceholder>#\+ .+? #)", RegexOptions.None);

    public RenderDisplayNamePlaceholders(HtmlHelper<T> html, string template)
    {
      _template = template;

      if (string.IsNullOrEmpty(template) || !template.Contains("#+")) return;

      foreach (Match match in _regex.Matches(template))
      {
        if (!match.Success) continue;

        var displayNamePlaceholder = match.Groups["displayNamePlaceholder"].Value;
        if (string.IsNullOrEmpty(displayNamePlaceholder)) continue;

        var propertyName = displayNamePlaceholder.Trim('#').TrimStart('+').Trim();
        if (string.IsNullOrEmpty(propertyName)) continue;

        var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (propertyInfo == null || !propertyInfo.CanRead || !propertyInfo.CanWrite) continue;

        _template = _template.Replace(displayNamePlaceholder, LambdaHtmlHelper.LambdaHtmlHelper.DisplayName(propertyInfo));
      }
    }

    public override string ToString()
    {
      return _template;
    }
  }
}
