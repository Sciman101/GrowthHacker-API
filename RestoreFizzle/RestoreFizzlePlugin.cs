using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace RestoreFizzle
{
    [BepInPlugin("info.sciman.restorefizzle", "Restore Fizzle Post-Game", "0.0.0")]
    [BepInDependency("info.sciman.guapi")]
    public class RestoreFizzlePlugin : BaseUnityPlugin
    {
        void Start()
        {
            var harmony = new Harmony("info.sciman.restorefizzlepatch");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(GameManager), "FIZZLE_SCENE", MethodType.Getter)]
        class RestoreFizzlePostgamePatch
        {
            [HarmonyPostfix]
            static void PostFix(ref string __result)
            {
                if (SaveData.instance.GetBool("beatFinalBoss"))
                {
                    Debug.LogError("Fixing fizzle...");
                    __result = "FizzleFlagshipFinal";
                }
            }
        }

    }
}
