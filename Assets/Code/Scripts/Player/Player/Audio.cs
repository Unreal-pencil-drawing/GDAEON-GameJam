using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio controller")]

public class Audio : MonoBehaviour
{   
    public List<AudioClip> runSound;
    public List<AudioClip> attackSound;
    
    public AudioSource _Run;
    public AudioSource _Attack;
    
    public int runIndex;
    public float timer, maxtimer;

    public Player player;
    
    void Awake(){
        player = GameManager.gameManager.player.GetComponent<Player>();
        runIndex = 0;
        timer = 0;
    }

    void FixedUpdate(){
        if(player.currentAction == Player.Action.Running && timer <= 0){ 
            _Run.PlayOneShot(runSound[runIndex]);
            runIndex += 1;
            timer = maxtimer;
            if(runIndex > (runSound.Count - 1)) runIndex = 0;  
        }else{
            timer -= Time.deltaTime;
        }

        if(player.currentAction == Player.Action.Attacking && !_Attack.isPlaying){
            _Attack.PlayOneShot(attackSound[Random.Range(0, 2)]);
        }
    }
}
