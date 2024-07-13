using System.Collections;
using UnityEngine;

public class Dash : ActiveSkill
{
    private Player player;
    private Rigidbody2D rigitbody;

    private Vector2 direction;

    public override void Init(){
        cooldown = 3;

        key = KeyCode.Mouse1; /*override KeyCode */
        player = gameObject.GetComponent<Player>();
        rigitbody = gameObject.GetComponent<Rigidbody2D>();
    } 

    public override bool Detect(){
        if(Input.GetKey(key) && cooldownTimer <= 0){
            direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            cooldownTimer = cooldown + 0.45f; //dash time + after dash time + dash cooldown

            player.currentAction = Player.Action.Casting;
            return true;
        }
        return false;
    }
    
    public override void Cast(){
        StartCoroutine(_Cast());
    }

    private IEnumerator _Cast(){
        player.currentAction = Player.Action.Casting;
        rigitbody.velocity = direction*player.velocity*10f;

        yield return new WaitForSeconds(.25f);

        if(Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a")) player.currentAction = Player.Action.Running;
        else player.currentAction = Player.Action.Idle;
    }

    public override void UpdateTimer(){
        if(cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
    }

    public override GameObject createUI(){
        GameObject obj = Instantiate(Resources.Load("Prefabs/DashUI"), Vector3.zero, Quaternion.identity, GameObject.Find("Skills Panel").transform) as GameObject;
        obj.name = "Dash";
        return obj;
    }
}
