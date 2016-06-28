using System;

namespace VsBlox.Mvc.Templates.FormsAndTemplates.Helpers
{
  public static class TypeHelper
  {
    public static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
    {
      return potentialDescendant.IsSubclassOf(potentialBase) || potentialDescendant == potentialBase;
    }
  }
}
