using UnityEngine;
using BepInEx;
using System.Collections.Generic;

namespace GrowthHacker
{
    public delegate void OnFindScrob(ScriptableObject obj);

    [BepInPlugin("info.sciman.growthhacker","Growth Hacker API","0.2.0")]
    public class GrowthHackerAPI : BaseUnityPlugin
    {
        public static Transform prefabHelper;
        public static event OnFindScrob onScrobFound;

        // Entrypoint
        void Start()
        {
            Logger.LogMessage("Initializing Growth Hacker API - v" + Info.Metadata.Version);

            // Do all the patches
            Patcher.DoPatching();

            // Setup the prefab helper
            prefabHelper = new GameObject("Prefab Helper").transform;
            prefabHelper.gameObject.SetActive(false);
            DontDestroyOnLoad(prefabHelper);

            // Set up template skill
            SkillAPI.Initialize();

            // Find all Scrobs
            FindScrobs();
        }

        // Add a new costume to the game
        public static void AddCostume(ModdedCostume costume, string id, List<string> names)
        {
            Costume[] costumes = new Costume[GlobalSettings.defaults.jackieCostumes.Length + 1];
            for (int i = 0; i < costumes.Length - 1; i++)
            {
                costumes[i] = GlobalSettings.defaults.jackieCostumes[i];
            }
            costumes[costumes.Length - 1] = costume;
            GlobalSettings.defaults.jackieCostumes = costumes;

            // Add localization options
            LocalizedText.mainTable.Add(id, names);
        }

        // Create a fake prefab
        public static GameObject BindFakePrefab(GameObject go)
        {
            go.transform.SetParent(prefabHelper.transform);
            return go;
        }
        public static GameObject BindFakePrefab()
        {
            return BindFakePrefab(new GameObject());
        }

        // Used to find any Scrobs that we might need. This isn't the best approach but
        // IDK how else we'd do it
        // Broadcast as an event so other APIs can tap into it
        private static void FindScrobs()
        {
            UnityEngine.Object[] allScrobs = Resources.FindObjectsOfTypeAll<ScriptableObject>();
            foreach (ScriptableObject item in allScrobs)
            {
                if (onScrobFound != null)
                {
                    onScrobFound(item);
                }
                else
                {
                    break;
                }
            }
        }

    }
}
