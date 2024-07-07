using UnityEngine;

public class Run : Skill
{
    private Player player;
    private Rigidbody2D rigitbody;
    private Vector2 direction;

    public bool top, right, down, left;

    public override void Init(){
        title = "Run";
        description = "Базовое движение";   
        cooldown = 0;
        active = false;

        player = gameObject.GetComponent<Player>();
        rigitbody = gameObject.GetComponent<Rigidbody2D>();
    } 

    public override bool Detect(){
        top = Input.GetKey("w");
        right = Input.GetKey("d");
        down = Input.GetKey("s");
        left = Input.GetKey("a");

        if(top || right || down || left){ 
            player.currentAction = Player.Action.Running;
            return true;
        }
        player.currentAction = Player.Action.Idle;
        return false;
    }

    public override void Cast(){
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigitbody.velocity = direction*player.velocity;
    }

    public override void UpdateTimer(){}
}
