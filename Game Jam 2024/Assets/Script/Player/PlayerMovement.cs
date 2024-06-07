using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D m_rigidbody;
    private Vector2 position;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        position.x = Input.GetAxis("Horizontal");
        position.y = Input.GetAxis("Vertical");
        if(position.magnitude >1 ) { position = position.normalized; }
       
    }
    private void FixedUpdate()
    {
        m_rigidbody.velocity = position * speed;
    }
}



    

