using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Bu fonksiyonu butonların OnClick kısmına bağlayacağız
    // Parantez içindeki (string sceneName) sayesinde her buton farklı isim gönderecek
    public void OpenSpecificScene(string sceneName)
    {
        // Zamanı sıfırladıysan (Pause menüsü vb.) geri açar
        Time.timeScale = 1f;
        
        // Gönderilen isimdeki sahneyi yükler
        SceneManager.LoadScene(sceneName);
    }
}