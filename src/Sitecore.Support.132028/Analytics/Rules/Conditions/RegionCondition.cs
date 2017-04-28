namespace Sitecore.Support.Analytics.Rules.Conditions
{
  using Sitecore.Rules;
  using Sitecore.Analytics.Tracking;
  using System.Net;
  using Sitecore.Analytics.Lookups;
  using System;
  using Diagnostics;
  public class RegionCondition<T> : Sitecore.Analytics.Rules.Conditions.RegionCondition<T> where T : RuleContext
    {
        protected override string GetVisitStringValue(CurrentInteraction visit)
        {
            if (!Sitecore.ExperienceExplorer.Business.Helpers.PageModeHelper.IsExperienceMode)
            {
                return visit.GeoData.Region ?? string.Empty;
            }
            else
            {
                if (Sitecore.Analytics.Tracker.Current != null && Sitecore.Analytics.Tracker.Current.Interaction != null)
                {
                    try
                    {
                        var stringIP = new IPAddress(Sitecore.Analytics.Tracker.Current.Interaction.Ip).ToString();
                        return LookupManager.GetInformationByIp(stringIP).Region ?? string.Empty;
                    }
                    catch (Exception e)
                    {
                        Log.Error("Sitecore.Support.132028: Failed to get the region for ip. Returning empty value instead. Exception: " + e.Message, this);
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
