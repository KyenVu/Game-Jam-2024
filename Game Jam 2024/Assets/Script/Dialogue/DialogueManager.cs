using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    [Header("Typing Effect")]
    public float typingSpeed = 0.02f;
    public float autoNextDelay = 2.0f;  // Time in seconds to wait before auto-advancing

    private Queue<string> sentences;
    private bool isTyping;
    private PlayerMovement playerMovement;

    private void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        // Start the coroutine to wait and automatically display the next sentence
        StartCoroutine(AutoAdvanceSentence());
    }

    IEnumerator AutoAdvanceSentence()
    {
        yield return new WaitForSeconds(autoNextDelay);

        if (!isTyping && dialogueBox.activeSelf)
        {
            DisplayNextSentence();
        }
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping && dialogueBox.activeSelf)
        {
            StopCoroutine(AutoAdvanceSentence());  // Stop the auto-advance coroutine
            DisplayNextSentence();
        }
    }
}
