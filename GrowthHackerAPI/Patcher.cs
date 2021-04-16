using System;
using UnityEngine;
using TMPro;
using HarmonyLib;

namespace GrowthHacker
{
    public class Patcher
    {
        public static void DoPatching()
        {
            var harmony = new Harmony("info.sciman.patch");
            harmony.PatchAll();
        }

    }

    // Allows the debug menu to exist
    [HarmonyPatch(typeof(DestroyIfNotDebug), "OnEnable")]
    class AllowDebugPatch
    {
        static bool Prefix(DestroyIfNotDebug __instance)
        {
            UnityEngine.Object.Destroy(__instance);
            return false;
        }
    }

    // this prevents the steam manager from subscribing to achievement events
    // this way, people can't cheese achievements with mods
    [HarmonyPatch(typeof(SteamManager), "Start")]
    class SteamInteractionBlocker
    {
        static bool Prefix()
        {
            Debug.Log("Steam disabled");
            return false;
        }
    }

    // Adds mod label to version text
    [HarmonyPatch(typeof(VersionText), "UpdateText")]
    class VersionPatch
    {

        static AccessTools.FieldRef<VersionText, TextMeshProUGUI> versionText =
        AccessTools.FieldRefAccess<VersionText, TextMeshProUGUI>("text");

        [HarmonyPostfix]
        static void Postfix(VersionText __instance)
        {
            versionText(__instance).text += " (MOD)";
        }
    }

    // Allows modded costumes to have custom unlock parameters
    [HarmonyPatch(typeof(Costume), "isUnlocked", MethodType.Getter)]
    class ModdedCostumePatch
    {
        [HarmonyPostfix]
        static void PostFix(Costume __instance, ref bool __result)
        {
            if (__instance is ModdedCostume)
            {
                if ((__instance as ModdedCostume).checkUnlockedFunction != null)
                {
                    __result = (__instance as ModdedCostume).checkUnlockedFunction();
                }
            }
        }
    }

}
