using System.Collections;
using UnityEngine;

[AddComponentMenu("Attack")]

public class Attack : BaseModule
{
    public float cooldownTimer, cooldown;
    public GameObject area, player;

    private Vector2 direction;
    private float angle;
    private Quaternion rotation;

    private Animator animator;

    public override void Init()
    {
        player = GameManager.gameManager.player;
        animator = gameObject.GetComponent<Animator>();

        area = gameObject.transform.Find( "Attack Area" ).gameObject;
        area.GetComponent<PolygonCollider2D>().enabled = false;
        cooldown = 0.1f;
    }

    public override bool Detect()
    {
        area.transform.rotation = RotateAttackArea();

        if(Input.GetKey(KeyCode.Mouse0) && cooldownTimer <= 0){
            player.GetComponent<Player>().currentAction = Player.Action.Attacking;
            return true;
        }
        return false;
    }

    public override void Cast()
    {
        StartCoroutine(_Cast());
    }

    private IEnumerator _Cast(){
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        area.GetComponent<PolygonCollider2D>().enabled = true;

        animator.SetBool("Attack", true);
        animator.SetBool("Run", false);

        animator.SetFloat("Angle", area.transform.rotation.z);
        yield return new WaitForSeconds(0.4f);

        cooldownTimer = cooldown;
        animator.SetBool("Attack", false);
        if(Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a")) player.GetComponent<Player>().currentAction = Player.Action.Running;
        else player.GetComponent<Player>().currentAction = Player.Action.Idle;

        
        area.GetComponent<PolygonCollider2D>().enabled = false;
    }

    public override void UpdateTimer()
    {
        if(cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
    }

     private Quaternion RotateAttackArea(){
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg - 90;
        rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        return Quaternion.Slerp(transform.rotation, rotation, 10);
    }

}    
