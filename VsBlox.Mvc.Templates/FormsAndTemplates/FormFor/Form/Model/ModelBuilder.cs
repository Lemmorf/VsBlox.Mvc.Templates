using System;
using System.Collections;
using VsBlox.Mvc.Templates.FormsAndTemplates.Helpers;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.FormFor.Form.Model
{
  /// <summary>
  /// Builder for model info container
  /// </summary>
  public class ModelBuilder<T> : IHideObjectMembers
  {
    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <value>
    /// The model.
    /// </value>
    protected Model<T> Model { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelBuilder{T}"/> class.
    /// </summary>
    /// <param name="model">The model.</param>
    public ModelBuilder(Model<T> model) { Model = model; }

    /// <summary>
    /// Datas the specified data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    /// <exception cref="TypeAccessException">
    /// $Model type {typeof(T).Name} is an array. Arrays are not supported!
    /// or
    /// $Model type {typeof(T).Name} is an enumerable. Enumerables are not supported!
    /// </exception>
    public ModelBuilder<T> Data(T data)
    {
      if (typeof(T).IsArray) throw new TypeAccessException($"Model type {typeof(T).Name} is an array. Arrays are not supported!");
      if (typeof(IEnumerable).IsAssignableFrom(typeof(T))) throw new TypeAccessException($"Model type {typeof(T).Name} is an enumerable. Enumerables are not supported!");

      Model.Data = data;
      return this;
    }

    /// <summary>
    /// Hides the null properties.
    /// </summary>
    /// <param name="hideNullProperties">if set to <c>true</c> [hide null properties].</param>
    /// <returns></returns>
    public ModelBuilder<T> HideNullProperties(bool hideNullProperties) { Model.HideNullProperties = hideNullProperties; return this; }

    Type IHideObjectMembers.GetType() { return GetType(); }
  }
}
