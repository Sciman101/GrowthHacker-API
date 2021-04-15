using GUAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BringHomeTheBaconTable
{
    class BaconTableSkill : ModdedSkill
    {
        // These methods are called when the event listeners should be added/removed from the game
        protected override void InitializeListeners()
        {
            GameManager.events.OnEntityBroken += OnBroken;
        }

        protected override void RemoveListeners()
        {
            GameManager.events.OnEntityBroken -= OnBroken;
        }


        // Event handler
        private void OnBroken(KillInfo info)
        {
            if (info.killHit.attacker == moddedEntity && info.killHit.target.DisplayName == "Desk")
            {
                if (UnityEngine.Random.value > 0.75)
                {
                    int amt = UnityEngine.Random.Range(1, 5);
                    ParticleManager.instance.SpawnMoney(amt, info.killHit.target.transform.position);
                    base.ProcVisual();
                }
            }
        }
    }
}
