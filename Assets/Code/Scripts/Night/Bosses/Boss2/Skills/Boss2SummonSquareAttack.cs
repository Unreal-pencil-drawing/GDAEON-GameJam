using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2SummonSquareAttack : BossSkill
{
    protected Boss2 _boss;
    private bool isNotOdd = false;
    private List<List<Vector2>> linesVectors = new List<List<Vector2>>{};
    public override void Init(Boss boss, float cooldown)
    {
        _boss = (Boss2) boss;
        this.cooldown = cooldown;
        cooldownTimer = cooldown;
        float x = -14.445f;
        float y = 10.78f;
        float deltaX = -2*x/7;
        float deltaY = 2*y/5;
        int i = 0;
        while (y >= -12.78f) {
            linesVectors.Add(new List<Vector2>{});
            x = -14.445f;
            while (x <= 16.445f) {
                linesVectors[i].Add(new Vector2(x, y));
                x += deltaX;
            }
            i++;
            y -= deltaY;
            if (linesVectors.Count > 10) {
                throw new Exception("error");
            }
        }
    }

    public override void Cast()
    {
        base.Cast();
        _boss._rigidbody2D.velocity = new Vector2(0, 0);    
        StartCoroutine(SummonFirstLine());
    }

    public override bool IsTriggerCondition() => 
        base.IsTriggerCondition();

    private IEnumerator SummonFirstLine() {
        _boss._spriteRenderer.sprite = _boss.castSprite;
        yield return new WaitForSeconds(0.5f);
        _boss._spriteRenderer.sprite = _boss.sprite;
        
        for (int i = Convert.ToInt32(isNotOdd); i < 6; i+=2) {
            List<Vector2> lineVectors = linesVectors[i]; 
            foreach (Vector2 vector in lineVectors) {
                Instantiate(_boss._squarePrefab, vector, Quaternion.identity);
            }
        }

        yield return new WaitForSeconds(SquareAttack.attackDelay);
        yield return new WaitForSeconds(SquareAttack.existTime);
        EndSkill();
    }

    protected override void EndSkill()
    {
        isNotOdd = !isNotOdd;
        base.EndSkill();
    }

    public override void UpdateChanges() {

    }
}
