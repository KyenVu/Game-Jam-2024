using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CookingMiniGame : MonoBehaviour
{
    private bool isCompleted;
    public Button[] ingredientButtons; // Buttons for selecting ingredients
    public GameObject burgerImageObject; // Object to display the burger image
    public TextMeshProUGUI feedbackText; // Feedback text to show if the dish is correct or wrong
    public Sprite[] burgerSprites; // Array to hold the sprites for the burgers
    public TimerSlider timerSlider; // TimerSlider script to handle the timer gradient

    private string currentInput = ""; // Store the current input sequence
    private List<KeyValuePair<string, string>> recipes = new List<KeyValuePair<string, string>>()
    {
        new KeyValuePair<string, string>("Full Burger", "1354267"),
        new KeyValuePair<string, string>("Tomatoes Cheese Burger", "15427"),
        new KeyValuePair<string, string>("Veggie Burger", "135467")
    };

    private int currentRecipeIndex = 0;
    private float currentTime; // Current time left
    public float timeLimit = 30f; // Time limit for each recipe
    private bool isTimerRunning = true; // Flag to control timer

    void Start()
    {
        // Add listeners to the ingredient buttons
        foreach (Button button in ingredientButtons)
        {
            button.onClick.AddListener(() => OnIngredientSelected(button));
        }

        // Initialize the timer
        ResetTimer();

        // Display the first burger image
        DisplayBurgerImage();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            // Update the timer
            currentTime -= Time.deltaTime;
            timerSlider.SetTime(currentTime);

            if (currentTime <= 0 && isCompleted == false)
            {
                isTimerRunning = false;
                feedbackText.text = "Time's up! Try again!";
                SceneManager.LoadScene(3, LoadSceneMode.Single);
                // Add any additional logic for when time runs out
            }
        }
    }

    void OnIngredientSelected(Button button)
    {
        if (!isTimerRunning)
        {
            return; // Ignore input if the timer is not running
        }

        string ingredient = button.name;
        currentInput += ingredient;

        bool isCorrectSoFar = recipes[currentRecipeIndex].Value.StartsWith(currentInput);
        if (isCorrectSoFar)
        {
            FlashButton(button, Color.green);
        }
        else
        {
            FlashButton(button, Color.red);
            currentInput = ""; // Reset input if incorrect
        }

        // Check if the full sequence is complete and correct
        if (currentInput.Length == recipes[currentRecipeIndex].Value.Length)
        {
            if (currentInput == recipes[currentRecipeIndex].Value)
            {
                feedbackText.text = $"{recipes[currentRecipeIndex].Key} made successfully!";
                Debug.Log($"{recipes[currentRecipeIndex].Key} made successfully!");
                FlashButton(button, Color.yellow);

                // Move to the next recipe
                currentRecipeIndex++;
                if (currentRecipeIndex < recipes.Count)
                {
                    currentInput = ""; // Reset the input sequence for the next try
                    ResetTimer();
                    DisplayBurgerImage();
                }
                else
                {
                    //Win
                    feedbackText.text = "All recipes completed!";
                    isCompleted = true;
                    burgerImageObject.SetActive(false); // Hide the burger image
                    isTimerRunning = false; // Stop the timer since all recipes are completed
                    SceneManager.LoadScene(7, LoadSceneMode.Single);
                }
            }
            else
            {
                feedbackText.text = "Wrong combination, try again!";
                currentInput = ""; // Reset the input sequence for the next try
            }
        }
    }

    void DisplayBurgerImage()
    {
        if (currentRecipeIndex < recipes.Count)
        {
            burgerImageObject.GetComponent<Image>().sprite = burgerSprites[currentRecipeIndex];
            burgerImageObject.SetActive(true);
            feedbackText.text = $"Prepare {recipes[currentRecipeIndex].Key}";
            Debug.Log($"{recipes[currentRecipeIndex].Key}: {recipes[currentRecipeIndex].Value}");
        }
        else
        {
            burgerImageObject.SetActive(false);
        }
    }

    void ResetTimer()
    {
        currentTime = timeLimit;
        timerSlider.SetMaxTime(timeLimit);
        isTimerRunning = true;
    }

    void FlashButton(Button button, Color color)
    {
        StartCoroutine(FlashButtonCoroutine(button, color));
    }

    IEnumerator FlashButtonCoroutine(Button button, Color color)
    {
        Color originalColor = button.image.color;
        button.image.color = color;
        yield return new WaitForSeconds(0.5f);
        button.image.color = originalColor;
    }
}
