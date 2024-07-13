using UnityEngine;

public class Run : BaseModule
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
        right = Input.GetKey("d");
        down = Input.GetKey("s");
        left = Input.GetKey("a");

        if(top || right || down || left){ 
            player.currentAction = Player.Action.Running;
            animator.SetBool("Run", true);
            return true;
        }
        animator.SetBool("Run", false);
        player.currentAction = Player.Action.Idle;
        return false;
    }

    public override void Cast(){
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        if((left && right) || (top && down)){ 
            rigitbody.velocity = Vector2.zero;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
        }
        else rigitbody.velocity = direction*player.velocity;
    }

    public override void UpdateTimer(){
        return;
    }
}
