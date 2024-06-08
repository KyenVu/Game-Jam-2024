using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }
    private void Awake()
    {
        CurrentHealth = startingHealth;
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
        //if (collision.CompareTag("health"))
        //{
            //GetHealth(1);
        //}
        if (collision.CompareTag("medicine"))
        {
            //update score and progressbar
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
