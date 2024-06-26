using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField] private TextMeshProUGUI answerText;

    public delegate void OnAnswerSelected(bool isCorrect);
    public OnAnswerSelected onAnswerSelected;

    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }
    
    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            Debug.Log("CORRECT ANSWER");
        }
        else
        {
            Debug.Log("WRONG ANSWER");
        }
        onAnswerSelected?.Invoke(isCorrect);
    }

}