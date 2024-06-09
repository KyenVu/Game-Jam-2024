using UnityEngine;
using UnityEditor;
using System.IO;

public class CSV 
{
    private static string questionsCSVPath = "/Editor/CSVs/Questions.csv";
    private static string questionsPath = "Assets/Resources/Questions/";
    private static int numberOfAnswers = 4;
    private Question questions;
    [MenuItem("Utilities/Generate Questions")]
    public static void GeneratePhrases()
    {

        string[] allLines = File.ReadAllLines(Application.dataPath + questionsCSVPath);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');

            // CSV (COMMA SEPARATED VALUE) DATA FORMAT
            // QUESTION, CATEGORY, CORRECT ANSWER, WRONG ANSWER 1, WRONG ANSWER 2, WRONG ANSWER 3

            Question QuestionData = ScriptableObject.CreateInstance<Question>();
            QuestionData.questionData = splitData[0]; // 1st column from csv file
            QuestionData.category = splitData[1];

            // Initialize the array of answers
            QuestionData.answers = new string[4];

            // Check if the folder for generating questions does not exist
            if (!Directory.Exists(questionsPath))
            {
                // Create the directory as one does not exist (creates a folder)
                Directory.CreateDirectory(questionsPath);
            }

            for (int i = 0; i < numberOfAnswers; i++)
            {
                QuestionData.answers[i] = splitData[2 + i];
            }

            // CREATE THE FILE NAME
            // Remove the "?", file name cannot have that character
            if (QuestionData.questionData.Contains("?"))
            {
                // Questions will be named the same as the question text in this example
                QuestionData.name = QuestionData.questionData.Remove(QuestionData.questionData.IndexOf("?"));
            }
            else // Does not contain an invalid character, no changes required
            {
                QuestionData.name = QuestionData.questionData;
            }
            // Save this in the questionsPathfolder to load them later by script
            AssetDatabase.CreateAsset(QuestionData, $"{questionsPath}/{QuestionData.name}.asset");
        }

        AssetDatabase.SaveAssets();

        Debug.Log($"Generated Questions");
    }
}

