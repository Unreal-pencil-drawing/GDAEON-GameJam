using UnityEngine;

[AddComponentMenu("Health controller")]

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    public int currenthp;
    public float AfterHitResistenceTimer;

    private Player player;

    void Start(){ //переделать на доставание макс хп и создание инстансов сердечек
        player = GetComponent<Player>();
        currenthp = hearts.Length;
    }

    void FixedUpdate(){
        if(currenthp <= 0){
            //SMERT
            player.currentAction = Player.Action.Idle;
            Destroy(transform.gameObject);
        }
        UpdateTimers();
    }

    void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.layer == 6 && AfterHitResistenceTimer <= 0){
            hearts[currenthp - 1].SetActive(false);
            currenthp -= 1;
            AfterHitResistenceTimer = player.AfterHitResistenceTime;
        }
    }

    void UpdateTimers(){
        if(AfterHitResistenceTimer > 0) AfterHitResistenceTimer-= Time.deltaTime;
    }

}
