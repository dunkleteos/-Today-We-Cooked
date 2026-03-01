using UnityEngine;
using UnityEngine.SceneManagement; // Sahne değişimi için gerekli
using TMPro; // Eğer UI'da yazı göstermek istersen

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 180f; // 3 dakika (180 saniye)
    public string gameOverSceneName = "GameOver"; // Game Over sahnesinin adı
    public TextMeshProUGUI timerText; // Ekranda süreyi görmek için (Opsiyonel)

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateDisplay();
        }
        else
        {
            timeRemaining = 0;
            GameOver();
        }
    }

    void UpdateDisplay()
    {
        if (timerText != null)
        {
            // Süreyi dakika:saniye formatına çevirir
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void GameOver()
    {
        // Sahneyi yükle
        SceneManager.LoadScene(gameOverSceneName);
    }
}