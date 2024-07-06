using UnityEngine;

[AddComponentMenu("Move")]

public class Move : MonoBehaviour
{
    public bool top, right, down, left;
    private Vector2 direction;

    private Player player;
    private Rigidbody2D rigitbody;

    public Skill currentSkill;

    void Start(){
        InitPlayer();
    }

    void Update(){
        if (player.currentAction == Player.Action.Running || player.currentAction == Player.Action.Idle){
            foreach(Skill skill in player.skills){ 
                if(skill.Detect()) currentSkill = skill;
                break;
            }
        }
        if (player.currentAction != Player.Action.Casting && player.currentAction != Player.Action.Attacking) DetectRunning();
    }

    void FixedUpdate(){
        if(player.currentAction == Player.Action.Casting) currentSkill.Cast();
        else if(player.currentAction == Player.Action.Running) Running();
        else rigitbody.velocity = Vector2.zero;

        UpdateTimers();
    }
/*------------------------------------------------------------------------------*/
    void InitPlayer(){
        player = GetComponent<Player>();
        rigitbody = GetComponent<Rigidbody2D>();

        player.currentAction = Player.Action.Idle;
    }

    void DetectRunning(){
        top = Input.GetKey("w");
        right = Input.GetKey("d");
        down = Input.GetKey("s");
        left = Input.GetKey("a");

        if(top || right || down || left) player.currentAction = Player.Action.Running;
        else player.currentAction = Player.Action.Idle;
    }

    void Running(){
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigitbody.velocity = direction*player.velocity;
    }

    void UpdateTimers(){
        foreach(Skill skill in player.skills) skill.UpdateTimer();
    }
}

