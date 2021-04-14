using UnityEngine;

namespace GUAPI
{
    public class ModdedSkill : Skill
    {
        void Awake()
        {
            if (this.icon == null)
            {
                // Uh oh
                this.icon = SkillAPI.GetIconFix(GetType());
            }
        }

        public void SetIcon(Sprite icon)
        {
            this.icon = icon;
        }
    }
}
