using UnityEngine;

namespace GrowthHacker
{
    public class ModdedCurse : Curse
    {
        /*
         * The reason this exists is due to a catch with how we handle prefabs.
         * Long story short, we cant create new prefabs, so we instantiate disabled gameobjects that get cloned
         * But in the process, any non-serialized fields are lost, including the icon for a normal skill
         * So we need to override it on awake manually, to make sure there's an icon present
         */
        public Sprite overrideIcon;

        void Awake()
        {
            if (this.icon == null)
            {
                this.icon = overrideIcon;
            }
        }

        public void SetIcon(Sprite icon)
        {
            this.icon = icon;
            this.overrideIcon = icon;
        }
    }
}
