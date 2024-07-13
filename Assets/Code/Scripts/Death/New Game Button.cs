using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    public void OnMouseEnter(){
        gameObject.GetComponent<Text>().color = new Color(0.56f, 0.468f, 0.603f, 1);
    }

    public void OnMouseExit(){
        gameObject.GetComponent<Text>().color = Color.black;
    }

    public void OnClick(){
        Destroy(GameObject.Find("OnDestroyPrefab"));
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
