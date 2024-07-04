using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float velocity;

    float elapsedTime;
    public float cooldownTime;

    bool dodging = false;

    private Vector3 endPosition;
    private Vector3 startPosition;

    void Update(){
        if(cooldownTime > 0) cooldownTime -= Time.deltaTime;
        if(dodging == false){
            if(Input.GetKey("s") && transform.position.y > -9.1f)
                transform.position = transform.position + Vector3.down*velocity;
            if(Input.GetKey("w") && transform.position.y < 10f)
                transform.position = transform.position + Vector3.up*velocity;
            if(Input.GetKey("a") && transform.position.x > -18.9f){
                transform.position = transform.position + Vector3.left*velocity;
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
                transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1); 
            }
            if(Input.GetKey("d") && transform.position.x < 18.9f){
                transform.position = transform.position + Vector3.right*velocity;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1); 
            }

            if(Input.GetKey(KeyCode.Mouse1) && cooldownTime <= 0){
                Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                direction.z = 0;
                direction = direction.normalized;
                endPosition = transform.position + direction*velocity*60;

                if(endPosition.x > 18.9f) endPosition.x = 18.9f;
                else if(endPosition.x < -18.9f) endPosition.x = -18.9f;

                if(endPosition.y > 9.9) endPosition.y = 9.9f;
                else if(endPosition.y < -9.9) endPosition.y = -9.1f;

                elapsedTime = 0;
                dodging = true;
            }
        }else if(dodging == true){
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, velocity*3);

            if(elapsedTime >= 0.5f) dodging = false;
            cooldownTime = 3;
        }
    }
}
