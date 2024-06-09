using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace for using Slider

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }

    [SerializeField] private Slider progressBar; // Reference to the UI slider
    [SerializeField] private float maxProgress = 100f; // Maximum value for the progress bar
    private float currentProgress = 0f; // Current progress value
    [SerializeField] private float medicineProgressIncrease = 10f; // Value to increase progress bar by

    private void Awake()
    {
        CurrentHealth = startingHealth;
        if (progressBar != null)
        {
            progressBar.maxValue = maxProgress;
            progressBar.value = currentProgress;
        }
    }

    public void TakeDamage(float damage)//receive damage and play animation
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
    }

    public void GetHealth(int health)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + health, 0, startingHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("drug"))
        {
            TakeDamage(1.0f);
            Debug.Log(CurrentHealth);
        }

        if (collision.CompareTag("medicine"))
        {
            UpdateProgress(medicineProgressIncrease);
            Debug.Log("Progress: " + currentProgress);
        }
    }

    private void UpdateProgress(float amount)
    {
        currentProgress = Mathf.Clamp(currentProgress + amount, 0, maxProgress);
        if (progressBar != null)
        {
            progressBar.value = currentProgress;
        }

        if (currentProgress >= maxProgress)
        {

            Debug.Log("Progress bar is full!");
       
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
