using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{// Time delay before the GameObject is destroyed
    public float delay = 3f;
    void Start()
    {

        Invoke("DestroySelf", delay);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
