namespace Sitecore.Support.Hooks
{
  using Sitecore.Configuration;
  using Sitecore.Diagnostics;
  using Sitecore.Events.Hooks;
  using Sitecore.SecurityModel;
  using System;
  public class UpdateRegionCondition :IHook
  {
    public void Initialize()
    {
      using (new SecurityDisabler())
      {
        var databaseName = "master";
        var itemPath = "/sitecore/system/Settings/Rules/Definitions/Elements/GeoIP/Region";
        var fieldName = "Type";

        // full name of the class is enough
        var fieldValue = "Sitecore.Support.Analytics.Rules.Conditions.RegionCondition,Sitecore.Support.132028";

        var database = Factory.GetDatabase(databaseName);
        var item = database.GetItem(itemPath);

        if (string.Equals(item[fieldName], fieldValue, StringComparison.Ordinal))
        {
          // already installed
          return;
        }

        Log.Info($"Installing {fieldValue}", this);
        item.Editing.BeginEdit();
        item[fieldName] = fieldValue;
        item.Editing.EndEdit();
      }
    }
  }
}