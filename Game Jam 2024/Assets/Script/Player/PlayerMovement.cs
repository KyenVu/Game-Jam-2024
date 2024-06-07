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
        // Rotate the player to face the direction of movement
        if (position != Vector2.zero)
        {
            float angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    private void FixedUpdate()
    {
        m_rigidbody.velocity = position * speed;
    }
}



    

