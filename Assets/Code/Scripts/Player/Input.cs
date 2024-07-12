using UnityEngine;

[AddComponentMenu("Input controller")]

public class Move : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rigitbody;

    public Skill currentSkill;

    void Start(){
        InitPlayer();
    }

    void Update(){
        if (player.currentAction == Player.Action.Running || player.currentAction == Player.Action.Idle){
            foreach(Skill skill in player.activeSkills){
                //Debug.Log(skill); 
                if(skill.Detect()) currentSkill = skill;
                break;
            }
        }
        if (player.currentAction != Player.Action.Casting && player.currentAction != Player.Action.Attacking) player.baseSkills[0].Detect();
    }

    void FixedUpdate(){
        if(player.currentAction == Player.Action.Casting) currentSkill.Cast();
        else if(player.currentAction == Player.Action.Running) player.baseSkills[0].Cast();
        else rigitbody.velocity = Vector2.zero;

        UpdateTimers();
    }
/*------------------------------------------------------------------------------*/
    void InitPlayer(){
        player = GetComponent<Player>();
        rigitbody = GetComponent<Rigidbody2D>();

        player.currentAction = Player.Action.Idle;
    }

    void UpdateTimers(){
        foreach(Skill skill in player.activeSkills) skill.UpdateTimer();
    }
}

