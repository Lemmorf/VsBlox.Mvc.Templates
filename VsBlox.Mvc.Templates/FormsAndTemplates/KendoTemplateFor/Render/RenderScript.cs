using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Buttons.Button;
using VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Helpers;
using VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Script;
using VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Script.Events;
using VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Template;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Render
{
  public class RenderScript<T>
  {
    private readonly TagBuilder _script = new TagBuilder("script");

    private readonly StringBuilder _innerHtml = new StringBuilder();

    public RenderScript(HtmlHelper<T> html, Template<T> template, Script<T> script)
    {
      _script.MergeAttribute("type", "text/javascript");

      var hideNullPropertiesSnippet = CreateHideNullPropertiesSnippet(template.Form.HideNullProperties);
      var bindOnSnippet = CreateBindOnSnippet(script.Events.BindTo, template.Form.Name);
      var bindOffSnippet = CreateBindOffSnippet(script.Events.BindTo, template.Form.Name);
      var dateTimeSnippet = CreateDateTimeSnippet(template.Form.Name);
      var setValuesSnippet = CreateSetValuesSnippet(template.Form.Name);
      var disabledSnippet = CreateDisabledSnippet(template.Form.Name, template.Form.Name);
      var readOnlySnippet = CreateReadOnlySnippet(template.Form.Name);
      var onCancelCompletionSnippet = CreateOnCompletionSnippet(script.Events, false);
      var onSubmitCompletionSnippet = CreateOnCompletionSnippet(script.Events, true);
      var onInitializationSnippet = CreateOnInitializationSnippet(script.Events);
      var onSubmitSnippet = CreateOnSubmitSnippet(script.Events, onSubmitCompletionSnippet, bindOffSnippet);
      var onCancelSnippet = CreateOnCancelSnippet(script.Events);
      var idLookupTable = CreateIdLookupTableSnippet(template.Form.Name);

      var buttonsSnippet = string.Empty;
      
      var btnSubmit = template.Form.Buttons.ButtonList.FirstOrDefault(b => b.Type == Button.ButtonType.Submit);
      if (btnSubmit != null)
      {
        var subMitHandler = script.Events.BindTo.FirstOrDefault(e => e.Type == Events.BindToInfo.BindingType.IsId && e.Name == btnSubmit.Name);
        if (subMitHandler == null)
        {
          if (buttonsSnippet.Length > 0) buttonsSnippet += Environment.NewLine;
          buttonsSnippet += $@"$('#{btnSubmit.Name}').click(function() {{ 
                                  var formSelector = $('#{template.Form.Name}');
                                  model = AjaxKendoForms.serializeToObjectRemovePrefix(formSelector, '{template.Form.Name}_');
                                  model['KendoCrudMode'] = mode;
                                  {onSubmitSnippet}
                               }});";
        }
      }

      var btnCancel = template.Form.Buttons.ButtonList.FirstOrDefault(b => b.Type == Button.ButtonType.Cancel);
      if (btnCancel != null)
      {
        var cancelHandler = script.Events.BindTo.FirstOrDefault(e => e.Type == Events.BindToInfo.BindingType.IsId && e.Name == btnCancel.Name);
        if (cancelHandler == null)
        {
          if (buttonsSnippet.Length > 0) buttonsSnippet += Environment.NewLine;
          buttonsSnippet += $@"$('#{btnCancel.Name}').click(function() {{
                                  {onCancelSnippet}
                                  wnd.close();
                                  {onCancelCompletionSnippet}
                                  {bindOffSnippet}
                                }});";
        }
      }

      _innerHtml.Append($@"var {script.Namespace} = (function (){{

          /* ============================================================================
           * Requires AjaxKendoForms (ajax-kendoforms.js) and AjaxForms (ajax-forms.js).
           * ============================================================================
           */

           {idLookupTable}

          function dialog(model, mode) {{

            var formSelector = $('#{template.Form.Name}');
            var templateSelector = $('#{template.Name}');
            var windowSelector = $('#{template.Name}Window');

            // initialize Kendo window
            if (windowSelector.data('kendoWindow') === undefined)
            {{
              windowSelector.kendoWindow({{
                modal: true,
                width: 500,
                actions: [],
                visible: false
              }});
            }}

            var wnd = windowSelector.data('kendoWindow');

            if (mode === 0) {{ // add (create)
              wnd.title('{string.Format(template.Title, "Add")}');
            }}
            else if (mode === 1) {{ // edit (update)
              wnd.title('{string.Format(template.Title, "Update")}');
            }}
            else if (mode === 2) {{ // delete (destroy)
              wnd.title('{string.Format(template.Title, "Delete")}');
            }}
            else {{ // view 
              wnd.title('{string.Format(template.Title, "View")}');
            }}

            var template = kendo.template(templateSelector.html());
            wnd.content(template(model));

            {hideNullPropertiesSnippet}
            {bindOnSnippet}
            {setValuesSnippet}
            {dateTimeSnippet}
            {onInitializationSnippet}

            if (mode !== 0 && mode !== 1) {{
              {disabledSnippet}
            }}
            {readOnlySnippet}

            {buttonsSnippet}

            wnd.center().open();
          }}

          function toLocalDateParse(date, format) {{
            return toLocalDate(Date.parse(date), format);
          }}

          function toLocalDate(date, format) {{
            var givenDate = new Date(date);
            var offset = -(givenDate.getTimezoneOffset() / 60);
            var hours = givenDate.getHours();
            hours += offset;
            givenDate.setHours(hours);
            return $.format.date(givenDate, format);
          }}

          function selectorFor(propertyName) {{
            return $('#' + idLookupTable[propertyName]);
          }}

          function closeWindow() {{
            var windowSelector = $('#{template.Name}Window');
            var wnd = windowSelector.data('kendoWindow');
            wnd.close();            
          }}

          function noClick(e) {{
            e.preventDefault();
          }}

          /* ============================================================================
           * Public API.
           * ============================================================================
           */
          return {{
            {script.FunctionName ?? "dialog" }: dialog,
            selectorFor: selectorFor,
            closeWindow: closeWindow
          }};

        }})();");
    }

    /// <summary>
    /// Creates the hide null properties snippet.
    /// </summary>
    /// <param name="hideNullProperties">if set to <c>true</c> [hide null properties].</param>
    /// <returns></returns>
    private string CreateHideNullPropertiesSnippet(bool hideNullProperties)
    {
      var sb = new StringBuilder();

      if (!hideNullProperties) return sb.ToString();

      foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead))
      {
        if (propertyInfo.IgnoreInForm()) continue;

        sb.AppendLine($@"if (model.{propertyInfo.Name} === undefined || model.{propertyInfo.Name} === null) {{  
                            $(\'#{{propertyInfo.InputTagId(prefix)}}\').hide();
                         }}");
      }

      return sb.ToString();
    }

    /// <summary>
    /// Creates the identifier lookup table snippet.
    /// </summary>
    /// <returns></returns>
    private string CreateIdLookupTableSnippet(string prefix)
    {
      var sb = new StringBuilder();

      sb.AppendLine("var idLookupTable = {");

      foreach (var propertyInfo in typeof (T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead))
      {
        if (propertyInfo.IsHidden() || propertyInfo.IgnoreInForm()) continue;

        sb.AppendLine($"  '{propertyInfo.Name}': '{propertyInfo.InputTagId(prefix)}',");
      }

      sb.AppendLine("}");

      return sb.ToString();
    }
    /// <summary>
    /// Creates the date time snippet.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <returns></returns>
    private string CreateDateTimeSnippet(string prefix)
    {
      var sb = new StringBuilder();

      foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead))
      {
        if (propertyInfo.IsHidden() || propertyInfo.IgnoreInForm()) continue;
        if (!propertyInfo.IsDate() && !propertyInfo.IsTime() && !propertyInfo.IsDateTime()) continue;

        var javaScript = new StringBuilder();

        var dataType = propertyInfo.GetDataType();

        if (dataType == DataType.Time)
        {
          var format = propertyInfo.DataFormatString() ?? "HH:mm:ss";
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').kendoTimePicker({{");
          javaScript.AppendLine($"format: '{format}',");
          javaScript.AppendLine("});");
        }
        else if (dataType == DataType.DateTime)
        {
          var format = propertyInfo.DataFormatString() ?? "dd-MM-yyyy HH:mm:ss";
          var startDepth = format.Contains("d") ? "" : "start: \"year\",\r\ndepth: \"year\",\r\n";
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').kendoDateTimePicker({{");
          javaScript.AppendLine(startDepth);
          javaScript.AppendLine($"format: '{format}',");
          javaScript.AppendLine("});");
        }
        else
        {
          var format = propertyInfo.DataFormatString() ?? "dd-MM-yyyy";
          var startDepth = format.Contains("d") ? "" : "start: \"year\",\r\ndepth: \"year\",\r\n";
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').kendoDatePicker({{");
          javaScript.AppendLine(startDepth);
          javaScript.AppendLine($"format: '{format}',");
          javaScript.AppendLine("});");
        }

        if (!string.IsNullOrEmpty(javaScript.ToString()))
        {
          sb.AppendLine($@"if (model.{propertyInfo.Name} !== undefined && model.{propertyInfo.Name} !== null) {{  
                              {javaScript}
                         }}");
        }
      }

      return sb.ToString();
    }

    /// <summary>
    /// Creates the set values snippet.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <returns></returns>
    private string CreateSetValuesSnippet(string prefix)
    {
      var sb = new StringBuilder();

      foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead))
      {
        if (propertyInfo.IgnoreInForm()) continue;

        var javaScript = new StringBuilder();

        var propertyInfoType = propertyInfo.GetPropertyType();

        if (propertyInfo.IsDate())
        {
          javaScript.AppendLine($"if (typeof model.{propertyInfo.Name} === 'string') {{");
          javaScript.AppendLine($"  $('#{propertyInfo.InputTagId(prefix)}').val(model.{propertyInfo.Name});");
          javaScript.AppendLine("} else {");
          javaScript.AppendLine($"  $('#{propertyInfo.InputTagId(prefix)}').val(model.{propertyInfo.Name}.toISOString());");
          javaScript.AppendLine("}");
        }
        else if (propertyInfo.IsTime())
        {
          javaScript.AppendLine($"var date{propertyInfo.Name};");
          javaScript.AppendLine($"if (typeof model.{propertyInfo.Name} === 'string') {{");
          javaScript.AppendLine(  $"date{propertyInfo.Name} = new Date(Date.parse(model.{propertyInfo.Name}));");
          javaScript.AppendLine("}");
          javaScript.AppendLine($"var dateFormat{propertyInfo.Name} = toLocalDate(date{propertyInfo.Name}, '{propertyInfo.DataFormatString() ?? "HH:mm:ss"}');");
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').val(dateFormat{propertyInfo.Name});");
        }
        else if (propertyInfoType == typeof (bool) && !propertyInfo.IsHidden())
        { 
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').prop('checked', model.{propertyInfo.Name});");
        }
        else
        {
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').val(model.{propertyInfo.Name});");
        }

        if (!string.IsNullOrEmpty(javaScript.ToString()))
        {
          sb.AppendLine($@"if (model.{propertyInfo.Name} !== undefined && model.{propertyInfo.Name} !== null) {{  
                            {javaScript}
                         }}");
        }
      }

      return sb.ToString();
    }

    private string CreateDisabledSnippet(string formId, string prefix)
    {
      var sb = new StringBuilder();

      sb.AppendLine($"$('#{formId} input').prop('readonly', true);");
      sb.AppendLine($"$('#{formId} select').prop('disabled', true);");
      sb.AppendLine($"$('#{formId} .required-indicator').hide();");
      sb.AppendLine($"$('#{formId} label').on('click', noClick);");

      foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead))
      {
        if (propertyInfo.IgnoreInForm()) continue;

        var javaScript = new StringBuilder();

        if (propertyInfo.IsDate())
        {
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').data('kendoDatePicker').enable(false);");
        }
        else if (propertyInfo.IsDateTime())
        {
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').data('kendoDateTimePicker').enable(false);");
        }
        else if (propertyInfo.IsTime())
        {
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').data('kendoTimePicker').enable(false);");
        }

        if (!string.IsNullOrEmpty(javaScript.ToString()))
        {
          sb.AppendLine($@"if (model.{propertyInfo.Name} !== undefined && model.{propertyInfo.Name} !== null) {{  
                            {javaScript}
                         }}");
        }
      }

      return sb.ToString();
    }

    private string CreateReadOnlySnippet(string prefix)
    {
      var sb = new StringBuilder();

      foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead))
      {
        if (propertyInfo.IgnoreInForm()) continue;
        if (!propertyInfo.IsReadOnly()) continue;

        var javaScript = new StringBuilder();

        if (propertyInfo.IsDate())
        {
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').data('kendoDatePicker').enable(false);");
        }
        else if (propertyInfo.IsDateTime())
        {
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').data('kendoDateTimePicker').enable(false);");
        }
        else if (propertyInfo.IsTime())
        {
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').data('kendoTimePicker').enable(false);");
        }
        else
        {
          javaScript.AppendLine($"$('#{propertyInfo.InputTagId(prefix)}').prop('readonly', true);");
        }

        if (!string.IsNullOrEmpty(javaScript.ToString()))
        {
          sb.AppendLine($@"if (model.{propertyInfo.Name} !== undefined && model.{propertyInfo.Name} !== null) {{  
                            {javaScript}
                         }}");
        }
      }

      return sb.ToString();
    }

    /// <summary>
    /// Creates the on submit snippet.
    /// </summary>
    /// <param name="events">The events.</param>
    /// <param name="onSubmitCompletionSnippet">The on submit completion snippet.</param>
    /// <param name="bindOffSnippet">The bind off snippet.</param>
    /// <returns></returns>
    private string CreateOnSubmitSnippet(Events events, string onSubmitCompletionSnippet, string bindOffSnippet)
    {
      if (string.IsNullOrEmpty(events.OnSubmit))
      {
        return $"AjaxForms.submitModel(formSelector, model, function(data){{wnd.close(); {onSubmitCompletionSnippet} {bindOffSnippet} }});";
      }

      return $"if ({events.OnSubmit}(formSelector, mode, model) === true) {{wnd.close(); var data = {{}}; {onSubmitCompletionSnippet} {bindOffSnippet} }}";
    }

    private string CreateOnInitializationSnippet(Events events)
    {
      return string.IsNullOrEmpty(events.OnInitialization) ? string.Empty : $"{events.OnInitialization}(formSelector, mode, model);";
    }

    /// <summary>
    /// Creates the on cancel snippet.
    /// </summary>
    /// <param name="events">The events.</param>
    /// <returns></returns>
    private string CreateOnCancelSnippet(Events events)
    {
      return string.IsNullOrEmpty(events.OnCancel) ? "" : $"{events.OnCancel}(formSelector, mode);";
    }

    /// <summary>
    /// Creates the on completion snippet.
    /// </summary>
    /// <param name="events">The events.</param>
    /// <param name="submitOrCancel">if set to <c>true</c> [submit or cancel].</param>
    /// <returns></returns>
    private string CreateOnCompletionSnippet(Events events, bool submitOrCancel)
    {
      var flag = submitOrCancel ? "true" : "false";
      return string.IsNullOrEmpty(events.OnCompletion) ? "" : $" var data = {{}}; {events.OnCompletion}(formSelector, mode, data, {flag});";
    }

    private string CreateBindOnSnippet(List<Events.BindToInfo> bindToInfos, string prefix)
    {
      var sb = new StringBuilder();

      if (bindToInfos == null) return sb.ToString();

      foreach (var bindToInfo in bindToInfos)
      {
        if (!bindToInfo.IsValid) continue;

        var javaScript = new StringBuilder();

        var propertyInfo = typeof(T).GetProperty(bindToInfo.Name);

        var selector = string.Empty;
        switch (bindToInfo.Type)
        {
          case Events.BindToInfo.BindingType.IsProperty:
            selector = $"$('#{propertyInfo.InputTagId(prefix)}')";
            break;
          case Events.BindToInfo.BindingType.IsId:
            selector = $"$('#{bindToInfo.Name}')";
            break;
        }
        if (string.IsNullOrEmpty(selector)) continue;

        foreach (var handler in bindToInfo.Handlers)
        {
          javaScript.AppendLine($"{selector}.on('{handler.Key}', {handler.Value});");
        }

        if (string.IsNullOrEmpty(javaScript.ToString())) continue;

        if (propertyInfo != null)
        {
          sb.AppendLine($@"if (model.{propertyInfo.Name} !== undefined && model.{propertyInfo.Name} !== null) {{  
                            {javaScript}
                         }}");
        }
        else
        {
          sb.AppendLine(javaScript.ToString());
        }
      }
      
      return sb.ToString();
    }

    private string CreateBindOffSnippet(List<Events.BindToInfo> bindToInfos, string prefix)
    {
      var sb = new StringBuilder();

      if (bindToInfos == null) return sb.ToString();

      foreach (var bindToInfo in Enumerable.Reverse(bindToInfos))
      {
        if (!bindToInfo.IsValid) continue;

        var javaScript = new StringBuilder();

        var propertyInfo = typeof(T).GetProperty(bindToInfo.Name);

        var selector = string.Empty;
        switch (bindToInfo.Type)
        {
          case Events.BindToInfo.BindingType.IsProperty:
            selector = $"$('#{propertyInfo.InputTagId(prefix)}')";
            break;
          case Events.BindToInfo.BindingType.IsId:
            selector = $"$('#{bindToInfo.Name}')";
            break;
        }
        if (string.IsNullOrEmpty(selector)) continue;

        foreach (var handler in bindToInfo.Handlers)
        {
          javaScript.AppendLine($"{selector}.off('{handler.Key}', {handler.Value});");
        }

        if (string.IsNullOrEmpty(javaScript.ToString())) continue;

        if (propertyInfo != null)
        {
          sb.AppendLine($@"if (model.{propertyInfo.Name} !== undefined && model.{propertyInfo.Name} !== null) {{  
                            {javaScript}
                         }}");
        }
        else
        {
          sb.AppendLine(javaScript.ToString());
        }
      }

      return sb.ToString();
    }

    public override string ToString()
    {
      _script.InnerHtml = _innerHtml.ToString();

      return _script.ToString();
    }
  }
}
