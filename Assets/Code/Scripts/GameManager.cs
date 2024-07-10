using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject player;
    public bool day;

    
    void Awake(){
        if(gameManager == null){
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(player);
            gameManager = this;
            day = true;
        }  
    }

    public void changeScene(){
        Debug.Log(day);
        day = !day;
        Debug.Log(day);

        if(day){
            Debug.Log("SCENE L");  
            SceneManager.LoadScene("Levelling", LoadSceneMode.Single);
            player.SetActive(false);
        }
        else{
            Debug.Log("SCENE F"); 
            SceneManager.LoadScene("Fighting", LoadSceneMode.Single);
            player.SetActive(true); 
        }
    }
}
