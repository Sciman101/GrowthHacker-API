using System.Reflection;
using BepInEx;
using UnityEngine;
using GUAPI;

namespace ExampleSkill
{
    [BepInPlugin("info.sciman.exampleskill", "Skill API Example", "0.0.0")]
    [BepInDependency("info.sciman.guapi")]
    public class ExampleSkillPlugin : BaseUnityPlugin
    {
        void Start()
        {
            // Create our custom skill

            // First, we load the resources we want from testbundle
            // this includes a model of suzanne from blender, and omegaswomp

            GameObject skillModel = null;
            Sprite skillIcon = null;

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ExampleSkill.testbundle"))
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
            SkillAPI.createSkill<ExampleSkill>(
                "Modding Example Skill",
                "Example Skill Description\n+1<sprite=10>",
                skillIcon,
                skillModel,
                Rarity.Junk);

        }

    }
}
