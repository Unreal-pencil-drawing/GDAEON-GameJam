using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Teleport : BossSkill
{
    protected Boss2 _boss;
    private Vector2 velocity;
    private Vector2 targetPosition;
    private Vector2 position;
    private Vector2 vector;
    private bool isDown;

    public override void Init(Boss boss, float cooldown)
    {
        _boss = (Boss2) boss;
        this.cooldown = cooldown;
        cooldownTimer = cooldown;

    }

    public override void Cast()
    {
        base.Cast();
        velocity = new Vector2(0, 0);
        position = _boss.transform.position;
        _boss._rigidbody2D.velocity = new Vector2(0, 0);
        isDown = false;
        StartCoroutine(TeleportUp());
    }

    public override bool IsTriggerCondition() => 
        base.IsTriggerCondition() && (_boss.GetDistanceToClosestArenaPoint() < 3.5f || _boss.GetDistanceToPlayer() < 3.5f);

    private IEnumerator TeleportUp() {
        _boss._spriteRenderer.sprite = _boss.castSprite;
        yield return new WaitForSeconds(0.5f);
        _boss._spriteRenderer.sprite = _boss.sprite;
        _boss._boxCollider2D.enabled = false;
        velocity = new Vector2(0, 100);
        yield return new WaitForSeconds(0.5f);
        velocity = new Vector2(0, 0);
        _boss._rigidbody2D.velocity = new Vector2(0, 0);
        //Debug.Log(_boss.transform.position);
        StartCoroutine(TeleportDown());
    }

    private IEnumerator TeleportDown() {
        _boss.transform.position = new Vector2(0, 50);
        _boss._rigidbody2D.velocity = new Vector2(0, 0);
        //velocity = new Vector2(0, -100);
        isDown = true;
        yield return new WaitForSeconds(0.5f);
        isDown = false;
        //velocity = new Vector2(0, 0);
        //Debug.Log(_boss.transform.position);
        _boss._boxCollider2D.enabled = true;
        GameObject square = Instantiate(_boss._squarePrefab, vector, Quaternion.identity);
        square.transform.localScale *= 2;
        EndSkill();
    }

    public override void UpdateChanges() {
        _boss._rigidbody2D.velocity = velocity;
        if (isDown) {
            _boss.transform.position = Vector2.MoveTowards(_boss.transform.position, new Vector2(0, _boss._boxCollider2D.size.y/2), 100*Time.deltaTime);
        }
    }
}
