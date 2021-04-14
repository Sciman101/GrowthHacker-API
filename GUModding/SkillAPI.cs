using UnityEngine;
namespace GUAPI
{
    public class SkillAPI
    {
        private static GameObject templateSkill;
        private static Material templateMaterial;

        // This skill is used as something we can easily copy when creating our own skills
        public static void setupTemplateSkill()
        {
            // Grab an existing skill to use as reference for weapon stats
            Entity existingSkill = GameManager.instance.allSkills[0];
            templateMaterial = existingSkill.GetComponentInChildren<MeshRenderer>().material;

            // Create the skill entity
            templateSkill = GameObject.Instantiate(new GameObject("TemplateSkill", typeof(ItemView)), GoingUnderApiPlugin.prefabHelper.transform);
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
            GameObject modelParent = new GameObject("Visuals", typeof(Rotate));
            modelParent.layer = 15;
            modelParent.transform.SetParent(templateSkill.transform);

            // Create skill holder
            GameObject entityMod = new GameObject("Mod");
            entityMod.layer = 15;
            entityMod.transform.SetParent(templateSkill.transform);
        }

        /// <summary>
        /// Create a new skill and add it to the game's main pool
        /// </summary>
        /// <typeparam name="T">The skill class to use</typeparam>
        /// <param name="name">Name of the skill</param>
        /// <param name="description">Description of the skill</param>
        /// <param name="icon">Icon displayed in the UI</param>
        /// <param name="model">The model that appears in the world</param>
        /// <param name="rarity">Rarity of the skill</param>
        /// <param name="stackable">Whether or not multiple of this skill can coexist</param>
        /// <returns>The skill entity 'prefab'</returns>
        public static GameObject createSkill<T>(string name, string description, Sprite icon, GameObject model, Rarity rarity = Rarity.Fine, bool stackable=false) where T : ModdedSkill
        {
            GameObject skill = Object.Instantiate(templateSkill,GoingUnderApiPlugin.prefabHelper.transform);
            skill.name = name;

            // Attach model
            Transform visuals = skill.transform.Find("Visuals");
            GameObject modelInstance = Object.Instantiate(model);
            modelInstance.transform.SetParent(visuals);

            // Assign new material to the model
            Material modelMaterial = new Material(Shader.Find("Custom/Character"));
            MeshRenderer renderer = modelInstance.GetComponentInChildren<MeshRenderer>();

            // Copy over the texture
            modelMaterial.CopyPropertiesFromMaterial(templateMaterial);
            modelMaterial.SetTexture("_MainTex", renderer.material.GetTexture("_MainTex"));
            renderer.material = modelMaterial;

            // Attach the actual skill
            GameObject skillHolder = skill.transform.Find("Mod").gameObject;
            ModdedSkill skillInstance = skillHolder.AddComponent<T>();

            // Set skill properties
            skillInstance.DisplayNameIndex = name;
            skillInstance.description = description;
            skillInstance.rarity = rarity;
            skillInstance.stackable = stackable;
            skillInstance.SetIcon(icon);

            // Add the skill instance to the gamemanager's big list
            GameManager.instance.allSkills.Add(skill.GetComponent<ModPickup>());

            // Return the result
            return skill;
        }

    }
}
