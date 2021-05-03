using System.Reflection;
using BepInEx;
using System.Collections.Generic;
using UnityEngine;
using GrowthHacker;

namespace ExampleSkill
{
    [BepInPlugin("info.sciman.exampleplugin", "Growth Hacker API Example", "0.1.1")]
    [BepInDependency("info.sciman.growthhacker")]
    public class ExamplePlugin : BaseUnityPlugin
    {

        void Start()
        {
            // Create our custom skill

            // First, we load the resources we want from testbundle
            GameObject skillModel = null;
            Sprite skillIcon = null;

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ExamplePlugin.testbundle"))
            {

                var assetBundle = AssetBundle.LoadFromStream(stream);
                if (assetBundle == null)
                {
                    Debug.LogError("Failed to load AssetBundle!");
                }
                else
                {
                    Debug.Log("Loaded assetbundle!");
                    skillModel = assetBundle.LoadAsset<GameObject>("assets/monkey.fbx");
                    skillIcon = assetBundle.LoadAsset<Sprite>("assets/omegaswomp.png");
                }
            }


            // Once the resources are loaded, we can call a method to add the skill entity
            // And that's it! The bulk of the skill's actual behaviour is handled in 'ExampleSkill.cs'
            // Of course this doesn't account for unlocks or progression yet, but it's a decent start
            GameObject skill = SkillAPI.CreateSkill<ExampleSkill>(
                "EXAMPLE_SKILL",
                new List<string> { "Modding Example Skill" },
                new List<string> { "Example Skill Description\n+1<sprite=10>" },
                skillIcon,
                skillModel,
                Rarity.Junk);

            GameObject curse = SkillAPI.CreateCurse<ExampleCurse>(
                "EXAMPLE_CURSE",
                new List<string> { "Modding Example Curse" },
                new List<string> { "Example Curse Description\n-1<sprite=10>" },
                skillIcon,
                skillModel,
                3,
                Rarity.Junk);

        }

    }
}
