using UnityEngine;

public class Run : BaseSkill
{
    private Player player;
    private Animator animator;
    private Rigidbody2D rigitbody;
    private Vector2 direction;

    public bool top, right, down, left;

    public override void Init(){
        player = gameObject.GetComponent<Player>();
        rigitbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    } 

    public override bool Detect(){
        top = Input.GetKey("w");
        animator.SetBool("top", Input.GetKey("w"));

        right = Input.GetKey("d");
        
        down = Input.GetKey("s");
        animator.SetBool("down", Input.GetKey("s"));

        left = Input.GetKey("a");

        if(left || right) animator.SetBool("Horizontal", true);
        else animator.SetBool("Horizontal", false);

        if(top || right || down || left){ 
            player.currentAction = Player.Action.Running;
            return true;
        }
        player.currentAction = Player.Action.Idle;
        return false;
    }

    public override void Cast(){
        if(right) gameObject.transform.localScale = new Vector2(-0.5f, 0.5f);
        else gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
        
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if((left && right) || (top && down)){ 
            rigitbody.velocity = Vector2.zero;
            /* animator.SetBool("Horizontal", false); */
        }
        else rigitbody.velocity = direction*player.velocity;
    }
}
