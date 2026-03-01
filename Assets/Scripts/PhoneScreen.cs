using UnityEngine;
using UnityEngine.UI;

public class PhoneScreen : MonoBehaviour
{

    public AudioSource phoneAudio;
    public GameObject messageBox;
    public Button backGroundButton;
    public Button textButton;

    // Her tıklamada çağrılacak
    public void OnPhoneClick()
    {
        phoneAudio.Stop();
        messageBox.SetActive(true);
        backGroundButton.interactable = false;
        textButton.interactable = true;
    }
}
