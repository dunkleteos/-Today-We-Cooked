using UnityEngine;
using UnityEngine.SceneManagement;
public class startMenu_control : MonoBehaviour
{
    public void onStartButton()
    {
        SceneManager.LoadScene("StartScene");

    }
    public void onExitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void onOptionButton()
    {
        SceneManager.LoadScene("OptionScene");
    }
}
