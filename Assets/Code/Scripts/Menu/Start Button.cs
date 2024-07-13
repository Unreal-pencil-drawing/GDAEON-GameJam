using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private bool description;
    public string descriptionText;
    public Text buttonText;
    public GameObject tutorial;
    void Start()
    {
        buttonText = gameObject.GetComponent<Text>(); 
    }

    public void OnMouseEnter(){
        buttonText.color = new Color(0.56f, 0.468f, 0.603f, 1);
    }

    public void OnMouseExit(){
        buttonText.color = Color.black;
    }

    public void OnClick(){
        if(!description){
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 400);
            tutorial.GetComponent<Text>().text = descriptionText;
            description = true;
            buttonText.text = "Начать";
        }else{

            GameManager.gameManager.changeScene();
        }
    }
}
