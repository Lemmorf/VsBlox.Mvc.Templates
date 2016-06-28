namespace VsBlox.Mvc.Templates.FormsAndTemplates.Config
{
  /// <summary>
  /// Singleton class which contains the global templates.
  /// </summary>
  public sealed class TemplateConfig
  {
    private static volatile TemplateConfig _instance;
    private static readonly object SyncRoot = new object();

    /// <summary>
    /// Returns the one and only instance of this class.
    /// </summary>
    /// <value>
    /// The instance.
    /// </value>
    public static TemplateConfig Instance
    {
      get
      {
        if (_instance != null) return _instance;
        lock (SyncRoot) { if (_instance == null) _instance = new TemplateConfig(); }
        return _instance;
      }
    }

    private TemplateConfig() { }

    /// <summary>
    /// The place holder token
    /// </summary>
    public const string PlaceHolderToken = "#= placeholder #";

    /// <summary>
    /// Gets or sets the date time template.
    /// </summary>
    /// <value>
    /// The date time template.
    /// </value>
    public string DateTimeTemplate { get; set; } = $@"<fieldset class='form-group margin-bottom-10'>{PlaceHolderToken}</fieldset>";

    /// <summary>
    /// Gets or sets the general template.
    /// </summary>
    /// <value>
    /// The general template.
    /// </value>
    public string GeneralTemplate { get; set; } = $@"<fieldset class='form-group'>{PlaceHolderToken}</fieldset>";

    /// <summary>
    /// Gets or sets the CheckBox template.
    /// </summary>
    /// <value>
    /// The CheckBox template.
    /// </value>
    public string CheckBoxTemplate { get; set; } = $@"<fieldset class='form-group'><label class='checkbox'>{PlaceHolderToken}</label></fieldset>";

    /// <summary>
    /// Gets or sets the RadioButton template.
    /// </summary>
    /// <value>
    /// The RadioButton template.
    /// </value>
    public string RadioButtonTemplate { get; set; } = $@"<fieldset class='form-group'><label class='radiobutton'>{PlaceHolderToken}</label></fieldset>";

    /// <summary>
    /// Gets or sets the default HTML attributes.
    /// </summary>
    /// <value>
    /// The default HTML attributes.
    /// </value>
    public string DefaultHtmlAttributes { get; set; } = "form-control";

    /// <summary>
    /// Gets or sets the form button group.
    /// {0} placeholder for buttons
    /// </summary>
    /// <value>
    /// The form button group.
    /// </value>
    public string FormButtonGroup { get; set; } = @"<div class='dialog_buttons pull-right'>
                                                      {0}
                                                    </div>";

    /// <summary>
    /// Gets or sets the form button.
    /// {0} placeholder for button id
    /// {1} placeholder for button text
    /// </summary>
    /// <value>
    /// The form button.
    /// </value>
    public string FormButton { get; set; } = @"<input id='{0}' class='btn btn-default' type='button' value='{1}' />";

    /// <summary>
    /// Gets or sets the mandatory indicator.
    /// </summary>
    /// <value>
    /// The mandatory indicator.
    /// </value>
    public string MandatoryIndicator { get; set; } = @"<small class='required-indicator'><i class='icon-asterisk' style='color: red;'></i></small>";

    /// <summary>
    /// Gets or sets the mandatory text.
    /// </summary>
    /// <value>
    /// The mandatory text.
    /// </value>
    public string MandatoryText { get; set; } = @"<p class='required-indicator'><small><i class='icon-asterisk' style='color: red;'></i> Mandatory field!</small></p>";

    /// <summary>
    /// Gets or sets the kendo popup template.
    /// {0} placeholder for template id
    /// {1} placeholder for form
    /// {2} placeholder for buttons
    /// </summary>
    /// <value>
    /// The kendo popup template.
    /// </value>
    public string KendoPopupTemplate { get; set; } = @"<script id='{0}' type='text/x-kendo-template'>
                                                          <div class='popupMessage'>
                                                            <div class='container-fluid'>
                                                              <div class='row'>
                                                                <div class='col-md-12'>
                                                                  {1}
                                                                </div>
                                                              </div>
                                                            </div>
                                                          </div>
                                                          <hr />
                                                          <div class='dialog_buttons pull-right'>
                                                            {2}
                                                          </div>
                                                       </script>";

    /// <summary>
    /// Gets or sets the kendo popup button.
    /// {0} placeholder for button id
    /// {1} placeholder for button text
    /// </summary>
    /// <value>
    /// The kendo popup button.
    /// </value>
    public string KendoPopupButton { get; set; } = @"<input id='{0}' class='k-button' type='button' value='{1}' style='width: 70px;' />";
  }
}
