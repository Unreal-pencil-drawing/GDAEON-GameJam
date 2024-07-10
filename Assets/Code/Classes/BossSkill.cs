using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[AddComponentMenu("BossSkill")]

public abstract class BossSkill : MonoBehaviour
{
    protected float cooldown;
    public float cooldownTimer;

    public bool isSkillEnd { get; protected set; }

    public virtual void Init(Boss boss, float cooldown) {
        cooldownTimer = cooldown;
    }

    public virtual bool IsTriggerCondition() {
        if (IsOnCooldown()) {
            return false;
        }
        return true;
    }

    public virtual void Cast() {
        cooldownTimer = cooldown;
        isSkillEnd = false;
    }

    public virtual void UpdateTimer() {
        if (cooldownTimer > 0) {
            cooldownTimer -= Time.deltaTime;
        }
    }

    protected virtual void OnCooldown() {
        cooldownTimer = cooldown;
    }

    protected virtual void EndSkill() {
        isSkillEnd = true;
        OnCooldown();
    }

    protected virtual bool IsOnCooldown() => cooldownTimer > 0;
 
    public abstract void UpdateChanges();
}
