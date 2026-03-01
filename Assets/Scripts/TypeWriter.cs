using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TypewriterDialogue : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogueText;       
    public Button nextButton;           
    public GameObject panelParent;      

    [Header("Dialogue Content")]
    [TextArea(3, 10)]
    public string dialogueContent;      

    [Header("Audio")]
    public AudioSource audioSource;     
    public AudioClip typingSound;       

    [Header("Settings")]
    public float typingSpeed = 0.05f;
    public string nextSceneName;        

    private string[] story;
    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private bool hasStarted = false;

    void Awake()
    {
        
        if (!string.IsNullOrEmpty(dialogueContent))
        {
            story = dialogueContent.Split('|');
            story = System.Array.FindAll(story, s => !string.IsNullOrWhiteSpace(s));
        }
        else
        {
            Debug.LogError("Dialogue content is empty!");
        }

        
        if (dialogueText != null)
            dialogueText.text = "";
    }

    void Start()
    {
        if(nextButton != null)
            nextButton.onClick.AddListener(OnClick);
    }

    void Update()
    {
        
        if (!hasStarted && panelParent != null && panelParent.activeInHierarchy)
        {
            currentIndex = 0;    // Başlangıç cümlesi
            StartTyping();
            hasStarted = true;
        }
    }

    void OnClick()
    {
        if (isTyping)
        {
            
            StopCoroutine(typingCoroutine);
            dialogueText.text = story[currentIndex];
            StopTypingSound();
            isTyping = false;
        }
        else
        {
            
            currentIndex++;
            if (currentIndex < story.Length)
                StartTyping();
            else if (!string.IsNullOrEmpty(nextSceneName))
                SceneManager.LoadScene(nextSceneName);
        }
    }

    void StartTyping()
    {
        if(story == null || story.Length == 0) return;
        typingCoroutine = StartCoroutine(TypeText(story[currentIndex]));
    }

    IEnumerator TypeText(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        if (audioSource != null && typingSound != null)
        {
            audioSource.clip = typingSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        StopTypingSound();
        isTyping = false;
    }

    void StopTypingSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }
    }
}