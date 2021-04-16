using UnityEngine;
using BepInEx;

namespace GrowthHacker
{
    [BepInPlugin("info.sciman.growthhacker","Growth Hacker API","0.1.1")]
    public class GrowthHackerAPI : BaseUnityPlugin
    {
        public static GameObject prefabHelper;

        // Entrypoint
        void Start()
        {
            Logger.LogMessage("Initializing Growth Hacker API v" + Info.Metadata.Version);

            // Do all the patches
            Patcher.DoPatching();

            // Setup the prefab helper
            prefabHelper = new GameObject("Prefab Helper");
            prefabHelper.SetActive(false);
            DontDestroyOnLoad(prefabHelper);

            // Set up template skill
            SkillAPI.setupTemplateSkill();
        }

        // Add a new costume to the game
        public static void AddCostume(ModdedCostume costume)
        {
            Costume[] costumes = new Costume[GlobalSettings.defaults.jackieCostumes.Length + 1];
            for (int i = 0; i < costumes.Length - 1; i++)
            {
                costumes[i] = GlobalSettings.defaults.jackieCostumes[i];
            }
            costumes[costumes.Length - 1] = costume;
            GlobalSettings.defaults.jackieCostumes = costumes;
        }

    }
}
