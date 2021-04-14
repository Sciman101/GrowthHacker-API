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

    }
}
