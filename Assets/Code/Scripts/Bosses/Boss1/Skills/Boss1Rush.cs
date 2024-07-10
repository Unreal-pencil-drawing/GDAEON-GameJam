using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Rush : BossSkill
{
    public float triggerDistance = 5f;
    public float targetDistance = 3f;
    public float delayTime = 0.5f;
    private Boss1 _boss;
    private float elapsedTime = 0;
    private Vector2 direction;
    private float velocity = 20;
    private Vector2 targetPosition;

    public override void Init(Boss boss, float cooldown)
    {
        _boss = (Boss1) boss;
        this.cooldown = cooldown;
        cooldownTimer = cooldown;
    }

    public override bool IsTriggerCondition()
    {
        if (base.IsTriggerCondition())
        {
            return _boss.GetDictanceToPlayer() > triggerDistance;
        }
        return false;
    }

    public override void Cast()
    {
        base.Cast();
        targetPosition = (Vector2)(_boss.GetPlayerPosition() - _boss.transform.position);
        targetPosition = targetPosition.normalized;
        elapsedTime = 0f;
    }

    public override void UpdateChanges()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < delayTime) 
            return;
        _boss.GetRigidbody2D().velocity = (elapsedTime - delayTime) * velocity * targetPosition;
        if (_boss.GetDictanceToPlayer() < Math.Min(triggerDistance, targetDistance) || elapsedTime > 1f + delayTime)
        {
            _boss.isResistance = false;
            isSkillEnd = true;
        }
    }
}
