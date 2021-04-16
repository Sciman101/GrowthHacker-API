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
            // this includes a model of suzanne from blender, and omegaswomp

            GameObject skillModel = null;
            Sprite skillIcon = null;

            // Costume meshes
            Mesh jellyMesh = null;
            Mesh marvMesh = null;
            Mesh joblinMesh = null;

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

                    jellyMesh = assetBundle.LoadAsset<Mesh>("assets/JellyModel.asset");
                    marvMesh = assetBundle.LoadAsset<Mesh>("assets/MarvModel.asset");
                    joblinMesh = assetBundle.LoadAsset<Mesh>("assets/JoblinModel.asset");
                }
            }


            ModdedCostume jellyCostume = new ModdedCostume
            {
                nameIndex = "JELLY_COSTUME",
                mesh = jellyMesh,
                unlockRequirement = Costume.UnlockRequirement.None,
                material = GlobalSettings.defaults.jackieCostumes[0].material,
                checkUnlockedFunction = () =>
                {
                    // Set a custom function to determine if the costume is unlocked or not
                    return false;
                }
            };
            GrowthHackerAPI.AddCostume(jellyCostume);
            LocalizedText.mainTable.Add("JELLY_COSTUME",new List<string> { "The Barista" });

            ModdedCostume marvCostume = new ModdedCostume
            {
                nameIndex = "MARV_COSTUME",
                mesh = marvMesh,
                unlockRequirement = Costume.UnlockRequirement.None,
                material = GlobalSettings.defaults.jackieCostumes[0].material
            };
            GrowthHackerAPI.AddCostume(marvCostume);
            LocalizedText.mainTable.Add("MARV_COSTUME", new List<string> { "The Project Manager" });

            ModdedCostume joblinCostume = new ModdedCostume
            {
                nameIndex = "JOBLIN_COSTUME",
                mesh = joblinMesh,
                unlockRequirement = Costume.UnlockRequirement.None,
                material = GlobalSettings.defaults.jackieCostumes[0].material
            };
            GrowthHackerAPI.AddCostume(joblinCostume);
            LocalizedText.mainTable.Add("JOBLIN_COSTUME", new List<string> { "Joblin Time :)" });


            // Once the resources are loaded, we can call a method to add the skill entity
            // And that's it! The bulk of the skill's actual behaviour is handled in 'ExampleSkill.cs'
            // Of course this doesn't account for unlocks or progression yet, but it's a decent start
            GameObject skill = SkillAPI.createSkill<ExampleSkill>(
                "Modding Example Skill",
                "Example Skill Description\n+1<sprite=10>",
                skillIcon,
                skillModel,
                Rarity.Junk);

        }

    }
}
