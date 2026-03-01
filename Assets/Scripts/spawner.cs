using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Sahne geçişi için gerekli
using UnityEngine.UI;
using TMPro;
public class EnemySpawner : MonoBehaviour
{
    [Header("Düşman Ayarları")]
    public GameObject[] enemyPrefabs; // Buraya 2 farklı düşman prefabını sürükle
    public int totalEnemiesToSpawn = 20;
    public float spawnInterval = 2f;
    
    [Header("Mesafe Ayarları")]
    public float spawnDistance = 15f;
    public Transform player;
    [Header("Bölüm Sonu UI")]
    public GameObject winPanel;
    public TextMeshProUGUI counterText;
    public GameObject portalObject;
    public AudioClip portalSound;
    public AudioSource stopSound;

    [Header("Geçilecek Sahne Adı")]
    public string nextSceneName; // Bölüm bitince gidilecek sahnenin adı

    private int spawnedCount = 0;
    private int deadEnemyCount = 0;
    private int remainingEnemies;

    void Start()
    {   
        remainingEnemies = totalEnemiesToSpawn;
        UpdateCounterUI();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (spawnedCount < totalEnemiesToSpawn)
        {
            SpawnRandomEnemy();
            spawnedCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomEnemy()
    {
        // 0 ile prefab sayısı arasında rastgele bir düşman seç
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        
        // Rastgele pozisyon hesapla
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        Vector2 spawnOffset = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * spawnDistance;
        Vector3 spawnPos = player.position + (Vector3)spawnOffset;

        Instantiate(enemyPrefabs[randomIndex], spawnPos, Quaternion.identity);
    }

    // Düşmanlar öldüğünde bu fonksiyon çağrılacak
    public void EnemyDied()
    {
      deadEnemyCount++;
        Debug.Log("Ölen düşman sayısı: " + deadEnemyCount);
        remainingEnemies = totalEnemiesToSpawn - deadEnemyCount;
        
        UpdateCounterUI();

        if (deadEnemyCount >= totalEnemiesToSpawn)
        {
            // Sahne değiştirmek yerine UI'yı açan fonksiyonu çağırıyoruz
            Invoke("ShowWinPanel", 2f); 
        }
    }
    void UpdateCounterUI()
    {
        if (counterText != null)
        {
            // Geriye doğru sayan metin
            counterText.text = "Remaining Enemies: " + remainingEnemies.ToString();
        }
    }
    void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true); // Paneli görünür yapar
            // İstersen burada oyunu durdurabilirsin:
            // Time.timeScale = 0f; 
        }
    }
    public void OnWinButtonClick()
    {
        // 1. Win Panelini kapat
        if (winPanel != null)
            winPanel.SetActive(false);

        // 2. Dünyadaki Portal/Image objesini aktif et
        if (portalObject != null)
        {
            portalObject.SetActive(true);
            stopSound.Stop();
            AudioSource.PlayClipAtPoint(portalSound, Camera.main.transform.position);
            Debug.Log("Portal açıldı! İçine girince sahne değişecek.");
        }

        // Eğer oyunu dondurduysan geri açmayı unutma
        Time.timeScale = 1f;
    }
}