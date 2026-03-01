using UnityEngine;
using UnityEngine.SceneManagement;

public class SosKontrol : MonoBehaviour 
{
    [Header("Sos")]
    public GameObject s1; 
    public GameObject s2; 
    public GameObject s3; 
    public GameObject s4; 
    public AudioSource sauceSound; 
    
    public void SosuAcKapat(GameObject sos) 
    {  
        if (!sos.activeSelf) 
        {
            sauceSound.Play(); 
        }
        sos.SetActive(!sos.activeSelf);
    }

    
    public void ServisEtButonu() 
    {
        bool j = s1.activeSelf; 
        bool p = s2.activeSelf; 
        bool h = s3.activeSelf; 
        bool g = s4.activeSelf; 

        // --- SAHNE GEÇİŞ MANTIĞI ---

       // 1. ÖLÜM ( Hepsi True) - Olasılık: 1
if (g && j && p && h) 
    SceneManager.LoadScene("OlumSahnesi");

// 2. MİDE  - Olasılık: 2
// Sadece h ve p'nin olduğu, altın ve mücevherin olmadığı durumlar
else if (h && !j && !g) 
    SceneManager.LoadScene("MideSahnesi");

// 3. EFSANE (Sadece g ve j olduğu durum) - Olasılık: 1
else if (g && j && !p && !h) 
    SceneManager.LoadScene("EfsaneShnesi");

// 4. İYİ (Mücevher var ama altın yok) - Olasılık: 3 
else if (j && !g) 
    SceneManager.LoadScene("IyiSahnesi");

// 5. YANMA (Altın var ama mücevher yok) - Olasılık: 3 
else if (g && !j) 
    SceneManager.LoadScene("YanmaSahnesi");

// 6. KÖTÜ  - Olasılık: 6
else 
    SceneManager.LoadScene("KotuSahnesi");
    }
}