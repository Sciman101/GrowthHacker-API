using System.Reflection;
using BepInEx;
using UnityEngine;
using GUAPI;

namespace BringHomeTheBaconTable
{
    [BepInPlugin("info.sciman.beinghomethebacontable", "Bring Home the Bacon (Table)", "0.0.0")]
    [BepInDependency("info.sciman.guapi")]
    public class BaconTableSkillPlugin : BaseUnityPlugin
    {

        void Start()
        {
            // Create our custom skill

            // First, we load the resources we want from testbundle
            // this includes a model of suzanne from blender, and omegaswomp

            GameObject skillModel = null;
            Sprite skillIcon = null;

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BringHomeTheBaconTable.bacontable"))
            {

                var assetBundle = AssetBundle.LoadFromStream(stream);
                if (assetBundle == null)
                {
                    Debug.LogError("Failed to load AssetBundle!");
                }
                else
                {
                    Debug.Log("Loaded assetbundle!");
                    skillModel = assetBundle.LoadAsset<GameObject>("assets/BaconTable.prefab");
                    skillIcon = assetBundle.LoadAsset<Sprite>("assets/BaconTableIcon.png");
                }
            }

            GameObject skill = SkillAPI.createSkill<BaconTableSkill>(
                "Bring Home the Bacon (Table)",
                "A creation of pure hubris.\nChance for tables to drop cash when you destroy them.",
                skillIcon,
                skillModel,
                Rarity.Junk);

            Transform model = skill.transform.Find("Visuals").GetChild(0);
            model.localScale *= 0.5f;
            model.localPosition += Vector3.up;

        }

    }
}
