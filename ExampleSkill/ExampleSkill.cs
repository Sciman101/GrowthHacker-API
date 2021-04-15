using UnityEngine;

namespace ExampleSkill
{
    class ExampleSkill : GUAPI.ModdedSkill
    {

        // These methods are called when the event listeners should be added/removed from the game
        protected override void InitializeListeners()
        {
            GameManager.events.OnEntityHit += OnHit;
        }

        protected override void RemoveListeners()
        {
            GameManager.events.OnEntityHit -= OnHit;
        }


        // These methods are called when the skill is granted/taken from the player
        protected override void OnApplied(Entity ent)
        {
        }
        protected override void OnRemoved(Entity ent)
        {
        }


        // Event handler
        private void OnHit(HitInfo info)
        {
            if (info != null && info.attacker == moddedEntity)
            {
                info.damageModAdditional++;
            }
        }

    }
}
