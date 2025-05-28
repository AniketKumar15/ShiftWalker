using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int totalPlates = 1; // Number of plates that must be activated
    private int currentActivatedPlates = 0;

    public GameObject doorOpen; // Assign door sprite/animation here
    public GameObject doorClose; // Assign door sprite/animation here

    public void PlateActivated()
    {
        currentActivatedPlates++;
        CheckDoorState();
    }

    public void PlateDeactivated()
    {
        currentActivatedPlates--;
        CheckDoorState();
    }

    void CheckDoorState()
    {
        if (currentActivatedPlates >= totalPlates)
        {
            OpenDoor();
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            AudioManager.instance.Play("DoorUnlock");
        }
        else
        {
            CloseDoor();
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OpenDoor()
    {
        Debug.Log("Door opened!");
        doorOpen.SetActive(true); // or play animation, or destroy(gameObject)
        doorClose.SetActive(false);
    }

    void CloseDoor()
    {
        Debug.Log("Door closed!");
        doorOpen.SetActive(false); // or play animation, or destroy(gameObject)
        doorClose.SetActive(true);
    }
}
