using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] AudioSource runSound;
    [SerializeField] AudioSource dashSound;

    private Player player;
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    void Update()
    {
        if(player.currentAction == Player.Action.Running){
            if(!runSound.isPlaying) runSound.Play();
        }else runSound.Pause(); 

        if(player.currentAction == Player.Action.Dashing){
            if(!dashSound.isPlaying) dashSound.Play();
        }
    }
}