using UnityEngine;
using UnityEngine.UI;

public class ToggleUIImagesWithTrigger : MonoBehaviour
{
    [Header("UI Images")]
    public GameObject object1; // Başlangıçta görünen GameObject (Image dahil)
    public GameObject object2; // Başlangıçta gizli GameObject (Image dahil)
    public AudioSource buttonAudioSource;

    [Header("Trigger Collider (object2 için)")]
    public Collider2D object2Trigger;

    // Butona bağlanacak fonksiyon
    public void OnButtonPressed()
    {   
        buttonAudioSource.Play();
        // object1'i gizle (GameObject pasif yap)
        if (object1 != null)
        {
            object1.SetActive(false);
        }

        // object2'yi göster (GameObject aktif yap)
        if (object2 != null)
        {
            object2.SetActive(true);
        }

        // object2 trigger collider'ı aktif et
        if (object2Trigger != null)
            object2Trigger.enabled = true;
    }
}
