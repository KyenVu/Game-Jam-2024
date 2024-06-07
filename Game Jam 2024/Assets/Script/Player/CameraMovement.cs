using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private void Update()
    {
        transform.position = new Vector2(player.position.x, player.position.y); 
    }

}
