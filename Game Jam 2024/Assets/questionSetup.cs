using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
public class questionSetup : MonoBehaviour
{
    [SerializeField]
    public List<Question> questions;
    private Question currentQuestion;

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI categoryText;
    [SerializeField]
    private AnswerButton[] answerButtons;
    [SerializeField]
    private int correctAnswerChoice;
    private int currentQuestionID;

    private void Awake()
    {
        currentQuestionID = 0;
        GetQuestionAssets(currentQuestionID);
    }
    //public void Start()
    //{
    //    SelectNewQuestion();
    //    SetQuestionValues();
    //    SetAnswerValues();

    //}
    private void GetQuestionAssets(int questionID)
    {
        // Get all of the questions from the questions folder
        //questions = new List<Question>(Resources.LoadAll<Question>("Question_" + questionID));

        // dm keo' me references di lai con ` random, lam` mau` =))
        // keo vao list -> em random dc ma`

        SelectNewQuestion();
        SetQuestionValues();
        SetAnswerValues();
    }
    private void SelectNewQuestion()
    {

        int randomQuestionIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomQuestionIndex];
        questions.RemoveAt(randomQuestionIndex);
    }
    private void SetQuestionValues()
    {
        // Set the question text
        questionText.text = currentQuestion.questionData;
    }
    private void SetAnswerValues()
    {
        // Randomize the answer button order
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        // Set up the answer buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Create a temporary boolean to pass to the buttons
            bool isCorrect = false;

            // If it is the correct answer, set the bool to true
            if (i == correctAnswerChoice)
            {
                isCorrect = true;
            }

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
            answerButtons[i].onAnswerSelected = OnAnswerSelected;
        }
    }

    private void OnAnswerSelected(bool isCorrect)
    {
        // day, lam luon trong nay`
        // bene click no se chuyen sang day luon, nen khogn can quan tam been answer nua
        // cua em 5 lit' nhe
        // techcombank
        // starbuck thi uong
        // thu 3 nhe 9-12h
        // caramel machiato nong'
        if (isCorrect)
        {
            // day, check neu het cau hoi roi thi no se o day, 
            // yeb, c
            currentQuestionID++;
            if (currentQuestionID >= 5)
            { 
            SceneManager.LoadScene("BaseLevel"); // thay vao day
            }
            
            // lam` gi co,anh xoa roi ma`
            // keo' luon vao` list o ngoai` y
            //yeb
            // cho nay` dang bi 1 loi, no se + out khoi range cua list


            GetQuestionAssets(currentQuestionID);
            Debug.Log("Here");
        }
        else
        {
            SceneManager.LoadScene("Education");
        }
    }

    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;

        List<string> newList = new List<string>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Get a random number of the remaining choices
            int random = Random.Range(0, originalList.Count);

            // If the random number is 0, this is the correct answer, MAKE SURE THIS IS ONLY USED ONCE
            if (random == 0 && !correctAnswerChosen)
            {
                correctAnswerChoice = i;
                correctAnswerChosen = true;
            }

            // Add this to the new list
            newList.Add(originalList[random]);
            //Remove this choice from the original list (it has been used)
            originalList.RemoveAt(random);
        }


        return newList;
    }
}
