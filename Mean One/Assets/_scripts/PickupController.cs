using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject heldObject;
    private GameObject currentlyLookedAtObject; // Store the currently looked at object

    [SerializeField]
    private float pickupDistance = 3f;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Cast a ray from the camera's center into the scene
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Check if the ray hits an object within pickupDistance
        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object is interactable
            if (hitObject.CompareTag("Interactable"))
            {
                // Display a prompt to pick up the object
                // You can implement this UI element yourself

                // Check for player input to pick up the object, for example, the 'E' key
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Call a method to handle the pickup (you can implement this method)
                    PickUpObject(hitObject);
                }

                // Enable the Outline script for the hit object
                Outline outline = hitObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                }

                // Store the currently looked at object
                currentlyLookedAtObject = hitObject;
            }
        }
        else
        {
            // If the player is not looking at an object, disable the Outline script of the previously looked at object
            if (currentlyLookedAtObject != null)
            {
                Outline outline = currentlyLookedAtObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = false;
                }
                currentlyLookedAtObject = null;
            }
        }

        // Check for player input to drop the held object, for example, the 'F' key
        if (Input.GetKeyDown(KeyCode.F) && heldObject != null)
        {
            DropObject();
        }
    }

    void PickUpObject(GameObject objectToPickup)
    {
        // You can implement your logic to pick up the object here
        // For example, disabling physics, parenting the object to the player's hand, etc.

        // Remember the currently held object
        heldObject = objectToPickup;

        // Optionally, hide the object visually
        heldObject.SetActive(false);
    }

    void DropObject()
    {
        // You can implement your logic to drop the held object here
        // For example, enabling physics, unparenting, setting its position, etc.

        // Show the object again
        if (heldObject != null)
        {
            heldObject.SetActive(true);
            heldObject = null;
        }
    }
}

