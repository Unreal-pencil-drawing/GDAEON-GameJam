using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    
    [SerializeField] Player player;
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
        if(collision.gameObject.layer == 7 ){
            Debug.Log(player.GetComponent<Player>().damage);
            hp -= player.GetComponent<Player>().damage;
        }
    }
}
