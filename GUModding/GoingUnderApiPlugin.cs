using UnityEngine;
using BepInEx;

namespace GUAPI
{
    [BepInPlugin("info.sciman.guapi","Going Under Modding API","0.0.0")]
    public class GoingUnderApiPlugin : BaseUnityPlugin
    {

        public static GameObject prefabHelper;

        // API TODO
        /*
         * -Block achivements
         * -Some way to toggle/enable debug menu
         * -Modified version number
         * 
         * -'Prefab' helper
         * -Skill creation tools
         * -Resource loading tools
         */

        // Entrypoint
        void Start()
        {
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
