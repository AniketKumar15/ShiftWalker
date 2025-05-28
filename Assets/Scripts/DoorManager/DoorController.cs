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
        if (doorOpen != null)  doorOpen.SetActive(true); 
        if (doorClose != null) doorClose.SetActive(false);
    }

    void CloseDoor()
    {
        Debug.Log("Door closed!");
        if (doorOpen != null) doorOpen.SetActive(false);
        if (doorClose != null) doorClose.SetActive(true);
    }
}
