using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerPickupDrop : MonoBehaviour
{
    public Transform holdPoint; // Point where the picked object will be held
    private GameObject heldObject;
    public Vector2 dropOffset = new Vector2(0, -1); // Offset to drop the object slightly below the player
    public TMP_Text trashCounterText; // UI text element to show the remaining trash count

    private int trashCount = 20;


    void Start()
    {
        UpdateTrashCount();
    }

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

        

            heldObject = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (heldObject != null && collider.CompareTag("TrashBin")) // Assuming the trash bin has a "TrashBin" tag
        {
            Destroy(heldObject); // Destroy the trash object
            heldObject = null;
            trashCount--; // Decrease the trash count
            UpdateTrashCount(); // Update the UI
        }
    }

    void UpdateTrashCount()
    {
        if (trashCounterText != null)
        {
            trashCounterText.text = "Trash Remaining: " + trashCount;
        }
    }

    public void IncreaseTrashCount()
    {
        trashCount++;
        UpdateTrashCount();
    }

    public void DecreaseTrashCount()
    {
        if (trashCount > 0)
        {
            trashCount--;
            UpdateTrashCount();
        }
    }
}
