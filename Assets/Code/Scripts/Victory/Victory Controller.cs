using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour
{
    
    public GameObject victoryText;
    public GameObject startButton;
    void Awake()
    {
        if(GameManager.gameManager.Bosses.Count == 0){
            victoryText.GetComponent<Text>().text = "ВСЕ БОССЫ МЕРТВЫ";
            startButton.SetActive(true);
        }else{
            StartCoroutine(toLevelling());
        }
    }

    private IEnumerator toLevelling(){
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Levelling", LoadSceneMode.Single);
        GameManager.gameManager.currentScene = GameManager.Scenes.Day1;
    }
}
