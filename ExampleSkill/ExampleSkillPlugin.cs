﻿using System.Reflection;
using BepInEx;
using System.Collections.Generic;
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


            Costume jellyCostume = new Costume();
            jellyCostume.nameIndex = "JELLY_COSTUME";
            jellyCostume.mesh = jellyMesh;
            jellyCostume.unlockRequirement = Costume.UnlockRequirement.None;
            jellyCostume.material = GlobalSettings.defaults.jackieCostumes[0].material;
            GoingUnderApiPlugin.AddCostume(jellyCostume);
            LocalizedText.mainTable.Add("JELLY_COSTUME",new List<string> { "The Barista" });

            Costume marvCostume = new Costume();
            marvCostume.nameIndex = "MARV_COSTUME";
            marvCostume.mesh = marvMesh;
            marvCostume.unlockRequirement = Costume.UnlockRequirement.None;
            marvCostume.material = GlobalSettings.defaults.jackieCostumes[0].material;
            GoingUnderApiPlugin.AddCostume(marvCostume);
            LocalizedText.mainTable.Add("MARV_COSTUME", new List<string> { "The Project Manager" });

            Costume joblinCostume = new Costume();
            joblinCostume.nameIndex = "JOBLIN_COSTUME";
            joblinCostume.mesh = joblinMesh;
            joblinCostume.unlockRequirement = Costume.UnlockRequirement.None;
            joblinCostume.material = GlobalSettings.defaults.jackieCostumes[0].material;
            GoingUnderApiPlugin.AddCostume(joblinCostume);
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
