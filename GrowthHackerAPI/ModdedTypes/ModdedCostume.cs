using System;

namespace GrowthHacker
{
    public class ModdedCostume : Costume
    {
        // This function is evaluated in order to check if a modded costume qualifies as 'unlocked'. By default, it's null, meaning default unlock behaviour takes presedence
        public Func<bool> checkUnlockedFunction = null;
    }
}
