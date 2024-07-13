using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public List<GameObject> Bosses;
    public List<SkillInfo> SkillPool;
    public GameObject player;

    public Scenes currentScene;

    private GameObject _boss;
    public enum Scenes{
        Menu,
        Day1,
        Day2,
        Night,
        Victory
    }

    void Awake(){
        if(gameManager == null){
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(player);

            gameManager = this;
            currentScene = Scenes.Menu;
            player.GetComponent<Player>().Init();
        }
        TakeDamage.DEAD += Death;
    }
    
    public void changeScene(){
        if(currentScene == Scenes.Menu){
            player.SetActive(false);
            SceneManager.LoadScene("Levelling", LoadSceneMode.Single);
            currentScene = Scenes.Day1;
        }
        else if(currentScene == Scenes.Day1){
            SceneManager.LoadScene("Levelling", LoadSceneMode.Single);
            currentScene = Scenes.Day2;
        }
        else if(currentScene == Scenes.Day2){
            Boss.Dead += BossIsDead; 
            SceneManager.LoadScene("Fighting", LoadSceneMode.Single);
            StartCoroutine(CreateNight());
            currentScene = Scenes.Night;
        }
        else if(currentScene == Scenes.Night){
            Boss.Dead -= BossIsDead;
            player.SetActive(false);
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
            currentScene = Scenes.Victory;
        }else if(currentScene == Scenes.Victory){
            SceneManager.LoadScene("Levelling", LoadSceneMode.Single);
            currentScene = Scenes.Day1;
        }
        else{
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            currentScene = Scenes.Menu;
        }
    }
    
    public void Death(){
        TakeDamage.DEAD -= Death;
        Boss.Dead -= BossIsDead;

        player.SetActive(false);

        Debug.Log("YOU ARE DEAD");
        SceneManager.LoadScene("Death", LoadSceneMode.Single);
    }

    public void BossIsDead(){
       StartCoroutine(_BossIsDead());
    }

    public IEnumerator _BossIsDead(){
        yield return new WaitForSeconds(3);
        changeScene();
    }

    public IEnumerator CreateNight(){
        yield return new WaitForSeconds(0.01f);
        player.SetActive(true); 
        player.GetComponent<Player>().GetComponent<TakeDamage>().RefreshHP(); /* refresh hp to max */
        player.transform.position = new Vector3(0, -9, 0);

        _boss = Bosses[Random.Range(0, Bosses.Count)];
        Bosses.Remove(_boss);

        Instantiate(_boss, Vector2.zero, Quaternion.identity);
    }
}
