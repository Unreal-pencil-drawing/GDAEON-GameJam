using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    
    [SerializeField] GameObject[] hearts;
    public int hp;

    void Start()
    {
        hp = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0){
            //SMERT
            Destroy(transform.parent.gameObject);
        }
    }

    public void takeDamage(){
        hearts[hp - 1].SetActive(false);
        hp -= 1;
    }
}
