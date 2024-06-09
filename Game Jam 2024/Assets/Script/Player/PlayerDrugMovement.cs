using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDrugMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        position.x = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        rb.velocity = position* speed;
    }
   
}
