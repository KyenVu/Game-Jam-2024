using UnityEngine;

public class PlayerPickupDrop : MonoBehaviour
{
    public Transform holdPoint; // Point where the picked object will be held
    private GameObject heldObject;
    public Vector2 dropOffset = new Vector2(0, -1); // Offset to drop the object slightly below the player

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Key to pick up or drop the object
        {
            if (heldObject == null)
            {
                // Try to pick up an object
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Pickable")) // Check if the object is pickable
                    {
                        PickUpObject(collider.gameObject);
                        break;
                    }
                }
            }
            else
            {
                // Drop the held object
                DropObject();
            }
        }
    }

    void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        obj.transform.position = holdPoint.position;
        obj.transform.SetParent(holdPoint);
        var rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true; // Disable physics interactions while holding
        }
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            var rb = heldObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false; // Enable physics interactions briefly
            }
            heldObject.transform.SetParent(null);

            // Drop the object at player's position plus offset
            heldObject.transform.position = (Vector2)transform.position + dropOffset;

            // Set the Rigidbody2D back to kinematic after setting the position
            if (rb != null)
            {
                rb.isKinematic = true; // Prevent physics interactions after dropping
            }

            heldObject = null;
        }
    }
}
