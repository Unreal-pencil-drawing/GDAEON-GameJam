using System;
using UnityEngine;

public class TakeDamage : BaseModule
{
    private Player player;
    
    public int currenthp;
    public float AfterHitResistenceTimer;
    public float redTimer;

    public static event Action GotHit;
    public static event Action DEAD;

    public override void Init()
    {
        player = gameObject.GetComponent<Player>();
        RefreshHP();
        AfterHitResistenceTimer = 0;
    }

    public override bool Detect()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        if(currenthp <= 0){
            gameObject.GetComponent<Player>().currentAction = Player.Action.DEAD;
            DEAD.Invoke();
        }  
        return true;
    }

    public override void Cast()
    {
        GotHit.Invoke();
        currenthp -= 1;
        AfterHitResistenceTimer = player.AfterHitResistenceTime;
        redTimer = 0.1f;
    }

    void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.layer == 6 && AfterHitResistenceTimer <= 0){
            Cast();
            Detect();
        }
    }

    public void RefreshHP(){
        currenthp = player.maxhp;
    }

    public override void UpdateTimer(){
        if(AfterHitResistenceTimer > 0) AfterHitResistenceTimer -= Time.deltaTime;
        if(redTimer <= 0) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        else redTimer -= Time.deltaTime;
    }
}
