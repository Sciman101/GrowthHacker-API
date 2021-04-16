using UnityEngine;

namespace GUAPI
{
    public class ModdedSkill : Skill
    {

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
