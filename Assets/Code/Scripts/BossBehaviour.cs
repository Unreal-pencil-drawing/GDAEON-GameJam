using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    
    [SerializeField] Player player;
    [SerializeField] GameObject healthManager;
    [SerializeField] float velocity;

    Vector3 endPosition;

    public int hp;

    void Start(){
        hp = 100;
    }

    void Update()
    {
        if(transform.position.x > player.transform.position.x){
            transform.localScale = new Vector3(-1, 1, 1);
        }else{
            transform.localScale = new Vector3(1, 1, 1);
        }
        endPosition = player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, endPosition, velocity);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == 3){
            Debug.Log("Hero collision");
            healthManager.GetComponent<HealthManager>().takeDamage();
        }else if(collision.gameObject.layer == 7 && player.GetComponent<Player>().currentAction == Player.Action.Attacking){
            Debug.Log("Sword collision");
            Debug.Log(player.GetComponent<Player>().damage);
            hp -= player.GetComponent<Player>().damage;
        }
    }
}
