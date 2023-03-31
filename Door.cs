using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform previousRoom;
    public Transform nextRoom;
    public CameraController camera;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            if (collider.transform.position.x < transform.position.x)
                camera.Move(nextRoom);
            else
                camera.Move(previousRoom);            
        }
    }
}
