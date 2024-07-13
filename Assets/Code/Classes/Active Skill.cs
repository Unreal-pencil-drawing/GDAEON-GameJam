using UnityEngine;

public abstract class ActiveSkill : Skill
{   
    public float cooldown, cooldownTimer;
    public KeyCode key; 

    public abstract void Cast();
    public abstract bool Detect();
    public abstract void UpdateTimer();
    public abstract GameObject createUI();

    public virtual void Init(KeyCode CastKey){
        key = CastKey;
        Init();
    }
}
