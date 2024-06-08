using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMoveRef : MonoBehaviour
{
    public int sceneBuildIndex;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            Debug.Log("Trigger Entered");
        }

        if (other.tag == "Player")
        {
            Debug.Log("Trigger Entered");
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }

    void Update()
    {
        
    }
}
