using UnityEngine;

public abstract class ActiveSkill : Skill
{   
    public float cooldown, cooldownTimer; 

    public abstract void Cast();
    public abstract bool Detect();
    public abstract void UpdateTimer();
    public abstract GameObject createUI();
}
