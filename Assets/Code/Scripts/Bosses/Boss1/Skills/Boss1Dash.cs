using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss1Dash : BossSkill
{
    /* #region  */

    public float triggerDistance = 15;
    public float velocity = 20;
    protected Boss1 _boss;
    protected float elapsedTime;
    private Vector2 direction;
    
    /* #endregion */

    public override void Init(Boss boss, float cooldown)
    {
        _boss = (Boss1)boss;
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
    }

    public override void UpdateChanges()
    {
        elapsedTime += Time.deltaTime;
        direction = (Vector2)(_boss.GetPlayerPosition() - _boss.transform.position);
        _boss.GetRigidbody2D().velocity = direction.normalized * velocity;
        //if (elapsedTime >= 0.5f)
        if (_boss.GetDictanceToPlayer() < 4.0f)
        {
            _boss.isResistance = false;
            EndSkill();
        }
    }
}
