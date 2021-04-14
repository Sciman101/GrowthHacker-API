using UnityEngine;

namespace GUAPI
{
    public class ModdedSkill : Skill
    {
        public void SetIcon(Sprite icon)
        {
            this.icon = icon;
        }

        public override Sprite GetIcon()
        {
            Debug.LogError(base.GetIcon().name);
            return base.GetIcon();
        }
    }
}
