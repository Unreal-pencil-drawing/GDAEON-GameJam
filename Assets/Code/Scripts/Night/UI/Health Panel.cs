using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GameManager.gameManager.player.GetComponent<Player>();
        gameObject.GetComponent<Image>().fillAmount = player.maxhp*0.1f;
        TakeDamage.GotHit += UpdateHealthbar;
    }

    void UpdateHealthbar(){
        gameObject.GetComponent<Image>().fillAmount -= 0.1f;
    }

    void OnDestroy(){
        TakeDamage.GotHit -= UpdateHealthbar;
    }
}
