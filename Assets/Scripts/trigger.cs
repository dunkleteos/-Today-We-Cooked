using UnityEngine;
using UnityEngine.SceneManagement; // Scene yönetimi için gerekli

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad; // Yüklenecek sahnenin adı

    // Trigger ile çarpışma kontrolü
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Player tag'ine bak
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}