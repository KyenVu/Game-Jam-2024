using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLock : MonoBehaviour
{
    
    BoxCollider2D doorCollider;
    public bool gameCompleted = false;
    private void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (gameCompleted == false)
        {
            doorCollider.enabled = false;
        }
        else
        {
            doorCollider.enabled = true;
        }
    }
}
