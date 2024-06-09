using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    private bool isCompleted = false;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField]float remainingTime;
    private void Update()
    {
        if(remainingTime > 0) { remainingTime -= Time.deltaTime; }
        if(remainingTime < 0) 
        { 
            remainingTime = 0;
            SceneManager.LoadScene(2, LoadSceneMode.Single);

        }
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
