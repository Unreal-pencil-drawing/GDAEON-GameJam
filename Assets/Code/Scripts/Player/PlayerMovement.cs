using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float elapsedTime;
    float currentCooldown;

    private Vector3 endPosition;
    private Player player;

    void Start(){
        player = GetComponent<Player>();
        player.currentAction = Player.Action.Idle;
        currentCooldown = player.dashCooldown;
    }

    void Update(){
        if(!(player.currentAction == Player.Action.Dashing)){
            player.currentAction = Player.Action.Idle;
            if(Input.GetKey("s") && transform.position.y > -9.1f){
                transform.position = transform.position + Vector3.down*player.velocity;
                player.currentAction = Player.Action.Running;
            }
            if(Input.GetKey("w") && transform.position.y < 10f){
                transform.position = transform.position + Vector3.up*player.velocity;
                player.currentAction = Player.Action.Running;
            }
            if(Input.GetKey("a") && transform.position.x > -18.9f){
                transform.position = transform.position + Vector3.left*player.velocity;
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
                transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
                player.currentAction = Player.Action.Running;
            }
            if(Input.GetKey("d") && transform.position.x < 18.9f){
                transform.position = transform.position + Vector3.right*player.velocity;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                player.currentAction = Player.Action.Running;
            }

            if(Input.GetKey(KeyCode.Mouse1) && currentCooldown <= 0){
                Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                direction.z = 0;
                direction = direction.normalized;
                endPosition = transform.position + direction*player.velocity*60;

                if(endPosition.x > 18.9f) endPosition.x = 18.9f;
                else if(endPosition.x < -18.9f) endPosition.x = -18.9f;

                if(endPosition.y > 9.9) endPosition.y = 9.9f;
                else if(endPosition.y < -9.9) endPosition.y = -9.1f;

                elapsedTime = 0;
                player.currentAction = Player.Action.Dashing;
            }

            if(Input.GetKey(KeyCode.Mouse0)) player.currentAction = Player.Action.Attacking;
        }
        
        if(player.currentAction == Player.Action.Dashing){
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, player.velocity*3);

            if(elapsedTime >= 0.5f)
                player.currentAction = Player.Action.Idle;
            currentCooldown = player.dashCooldown;
        }

        if(currentCooldown > 0) currentCooldown -= Time.deltaTime;
    }
}
