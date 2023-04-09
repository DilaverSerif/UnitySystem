using System;
using System.Linq;

namespace _SYSTEMS_._Upgrade_System_.Class
{
    [Serializable]
    public class RequirementLevelArray
    {
        public RequirementLevel[] requirementsForUpgrade;

        public RequirementLevel[] GetRequirements()
        {
            var requirementLevels = requirementsForUpgrade.Where(x => x.reqStat == RequirementStat.NotFinish);
            return requirementLevels.ToArray();
        }
    }
}