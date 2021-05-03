using UnityEngine;
using System.Collections.Generic;

namespace GrowthHacker
{
    public class SkillAPI
    {
        private static GameObject templateSkill;
        private static Material templateMaterial;
        private static EntitySpawnWeights hauntCurseTable;

        // This skill is used as something we can easily copy when creating our own skills
        public static void Initialize()
        {
            SetupTempalteSkill();
            // Setup event to find the haunt's scrob
            GrowthHackerAPI.onScrobFound += SearchHauntScrob;
        }


        /// <summary>
        /// Create a new skill and add it to the game's main pool
        /// </summary>
        /// <typeparam name="T">The skill class to use</typeparam>
        /// <param name="id">The ID to use in the localization table</param>
        /// <param name="names">Names of the skill</param>
        /// <param name="descriptions">Descriptions of the skill</param>
        /// <param name="icon">Icon displayed in the UI</param>
        /// <param name="model">The model that appears in the world</param>
        /// <param name="rarity">Rarity of the skill</param>
        /// <returns>The skill entity 'prefab'</returns>
        public static GameObject CreateSkill<T>(string id, List<string> names, List<string> descriptions, Sprite icon, GameObject model, Rarity rarity = Rarity.Fine) where T : ModdedSkill
        {
            GameObject skill = CreateModPickupBase(id, names, descriptions, model);

            // Attach the actual skill
            GameObject skillHolder = skill.transform.Find("Mod").gameObject;
            ModdedSkill skillInstance = skillHolder.AddComponent<T>();

            // Set skill properties
            skillInstance.DisplayNameIndex = id;
            skillInstance.rarity = rarity;
            skillInstance.SetIcon(icon);

            // Add the skill instance to the gamemanager's big list
            GameManager.instance.allSkills.Add(skill.GetComponent<ModPickup>());
            // Add the skill to the game's save data
            //SaveData.instance.UnlockSkill(names[0]);

            // Return the result
            return skill;
        }

        /// <summary>
        /// Create a new skill and add it to the game's main pool
        /// </summary>
        /// <typeparam name="T">The curse class to use</typeparam>
        /// <param name="id">The ID to use in the localization table</param>
        /// <param name="names">Names of the curse</param>
        /// <param name="descriptions">Descriptions of the curse</param>
        /// <param name="icon">Icon displayed in the UI</param>
        /// <param name="model">The model that appears in the world</param>
        /// <param name="duration">How many combats does this curse last for?</param>
        /// <returns>The curse entity 'prefab'</returns>
        public static GameObject CreateCurse<T>(string id, List<string> names, List<string> descriptions, Sprite icon, GameObject model, int duration, Rarity rarity = Rarity.Fine) where T : ModdedCurse
        {
            GameObject curse = CreateModPickupBase(id, names, descriptions, model);

            // Attach the actual skill
            GameObject curseHolder = curse.transform.Find("Mod").gameObject;
            ModdedCurse curseInstance = curseHolder.AddComponent<T>();

            // Set skill properties
            curseInstance.DisplayNameIndex = id;
            curseInstance.rarity = rarity;
            curseInstance.SetIcon(icon);
            curseInstance.time = duration;
            curseInstance.timeMode = EntityMod.TimeMode.Room;

            // Add the curse instance to the haunt's master skill list
            SpawnableEntityData data = new SpawnableEntityData();
            data.prefab = curse;
            // Set default curse parameters
            data.adjustPerFloorBy = 0;
            data.weight = 1;
            hauntCurseTable.possibleSpawnList.Add(data);

            // Return the result
            return curse;
        }


        // Create the base gameobject shared by skills and mods
        private static GameObject CreateModPickupBase(string id, List<string> names, List<string> descriptions, GameObject model)
        {
            GameObject skill = UnityEngine.Object.Instantiate(templateSkill, GrowthHackerAPI.prefabHelper);
            skill.name = names[0];
            
            // Attach model
            Transform visuals = skill.transform.Find("Visuals");
            GameObject modelInstance = UnityEngine.Object.Instantiate(model);
            modelInstance.transform.SetParent(visuals);
            //modelInstance.transform.position += Vector3.up * 0.9f;

            // Assign new material to the model
            Material modelMaterial = new Material(Shader.Find("Custom/CharacterPalette"));
            MeshRenderer renderer = modelInstance.GetComponentInChildren<MeshRenderer>();

            // Copy over the texture
            modelMaterial.CopyPropertiesFromMaterial(templateMaterial);
            //modelMaterial.SetTexture("_MainTex", renderer.material.GetTexture("_MainTex"));
            renderer.material = modelMaterial;

            // Add name and description to localization table
            LocalizedText.mainTable.Add("NAME_" + id, names);
            LocalizedText.mainTable.Add("DESC_" + id, descriptions);

            return skill;
        }


        // Setup the template skill for us to use
        private static void SetupTempalteSkill()
        {
            // Grab an existing skill to use as reference for weapon stats
            Entity existingSkill = GameManager.instance.allSkills[0];
            templateMaterial = existingSkill.GetComponentInChildren<MeshRenderer>().material;

            // Create the skill entity
            templateSkill = GameObject.Instantiate(new GameObject("TemplateSkill", typeof(ItemView)), GrowthHackerAPI.prefabHelper.transform);
            templateSkill.layer = 15;
            // Attach collider
            CapsuleCollider collider = templateSkill.AddComponent<CapsuleCollider>();
            collider.radius = 0.4f;
            collider.height = 1.25f;
            collider.center = Vector3.up * 0.626f;
            collider.direction = 1;
            // Attach ModPickup Entity
            ModPickup modPickup = templateSkill.AddComponent<ModPickup>();
            modPickup.isGrabbable = true;
            modPickup.weaponStats = existingSkill.weaponStats; // Just steal it from another skill

            // Create model wrapper
            GameObject modelParent = new GameObject("Visuals");
            Rotate rotate = modelParent.AddComponent<Rotate>();
            rotate.rotation = Vector3.up;
            rotate.speed = 50;
            modelParent.layer = 15;
            modelParent.transform.SetParent(templateSkill.transform);

            // Create skill holder
            GameObject entityMod = new GameObject("Mod");
            entityMod.layer = 15;
            entityMod.transform.SetParent(templateSkill.transform);
        }

        // Called for every scriptableobject in the game in order to find the haunt's curse list
        private static void SearchHauntScrob(ScriptableObject obj)
        {
            if (obj is EntitySpawnWeights)
            {
                if (obj.name.CompareTo("HauntCurses") == 0)
                {
                    hauntCurseTable = (EntitySpawnWeights)obj;
                    // Unsubscribe once we've found it
                    GrowthHackerAPI.onScrobFound -= SearchHauntScrob;
                }
            }
        }

    }
}
