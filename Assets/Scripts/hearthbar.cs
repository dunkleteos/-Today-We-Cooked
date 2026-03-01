using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthImage;
    public Sprite[] healthSprites; // 5 sprite

    public HealthSystem playerHealth; // Player script referansı

    void Update()
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        int health = playerHealth.Current_Health;

        if (health <= 0)
        {
            healthImage.sprite = healthSprites[0];
        }
        else
        {
            healthImage.sprite = healthSprites[health - 1];
        }
    }
}
