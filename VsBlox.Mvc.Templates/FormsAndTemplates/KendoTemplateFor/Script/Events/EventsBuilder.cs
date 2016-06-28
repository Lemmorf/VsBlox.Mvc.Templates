using System;
using System.Linq.Expressions;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.KendoTemplateFor.Script.Events
{
  public class EventsBuilder<T> : IHideObjectMembers
  {
    protected Events Events { get; set; }

    public EventsBuilder(Events events) { Events = events; }

    public EventsBuilder<T> OnInitialization(string onInitializationFunction) { Events.OnInitialization = onInitializationFunction; return this; }

    public EventsBuilder<T> OnCompletion(string onCompletionFunction) { Events.OnCompletion = onCompletionFunction; return this; }

    public EventsBuilder<T> OnSubmit(string onSubmitFunction) { Events.OnSubmit = onSubmitFunction; return this; }

    public EventsBuilder<T> OnCancel(string onCancelFunction) { Events.OnCancel = onCancelFunction; return this; }

    public EventsBuilder<T> BindTo<TP>(Expression<Func<T, TP>> expression, object handlers)
    {
      Events.BindTo.Add(new Events.BindToInfo(LambdaHtmlHelper.LambdaHtmlHelper.PropertyNameFromLambdaExpression(expression), handlers, Events.BindToInfo.BindingType.IsProperty)); return this;
    }

    public EventsBuilder<T> BindTo(string id, object handlers)
    {
      Events.BindTo.Add(new Events.BindToInfo(id, handlers, Events.BindToInfo.BindingType.IsId)); return this;
    }

    Type IHideObjectMembers.GetType()
    {
      return GetType();
    }
  }
}
