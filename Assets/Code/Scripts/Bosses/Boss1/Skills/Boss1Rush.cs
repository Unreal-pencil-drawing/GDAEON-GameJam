using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Rush : BossSkill
{
    public float triggerDistance = 5f;
    public float targetDistance = 3f;
    public float delayTime = 0.5f;
    public float rotateCofficient = 1f;
    private Boss1 _boss;
    private float elapsedTime = 0;
    private Vector3 direction;
    private float velocity = 20;
    private Vector3 targetPosition;
    private bool isFindTargetPosition;

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
            return _boss.GetDistanceToPlayer() > triggerDistance;
        }
        return false;
    }

    public override void Cast()
    {
        base.Cast();
        elapsedTime = 0f;
        isFindTargetPosition = false;
    }

    public override void UpdateChanges()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < delayTime)
            return;
        else {
            if (!isFindTargetPosition) {
                targetPosition = _boss.GetPlayerPosition();
                isFindTargetPosition = true;
            }
        }

        direction = _boss.GetPlayerPosition() - targetPosition;
        direction = targetPosition - _boss.transform.position + (2f - elapsedTime + delayTime) * rotateCofficient * direction;
        _boss._rigidbody2D.velocity = (elapsedTime - delayTime) * velocity * direction.normalized;
        if (elapsedTime > 2f + delayTime) {
            EndSkill();
        }
    }
}
