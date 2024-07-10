using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio controller")]

public class Audio : MonoBehaviour
{   
    [SerializeField] AudioSource runSound;
    [SerializeField] AudioSource dashSound;

    private Player player;
    
    void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update(){
        if(player.currentAction == Player.Action.Running){
            if(!runSound.isPlaying) runSound.Play();
        }else runSound.Stop(); 

        if(player.currentAction == Player.Action.Casting){
            if(!dashSound.isPlaying) dashSound.Play();
        }
    }
}
