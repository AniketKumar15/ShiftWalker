using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool isPressed = false;
    public DoorController connectedDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isPressed && other.CompareTag("Box"))
        {
            isPressed = true;
            connectedDoor.PlateActivated();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isPressed && other.CompareTag("Box"))
        {
            isPressed = false;
            connectedDoor.PlateDeactivated();
        }
    }
}
