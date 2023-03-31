using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform playerPosition;
    private float currentPosition;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosition, transform.position.y, transform.position.z),
                                                    ref velocity, speed);

        //Camera Follow Player
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);        
    }

    public void Move(Transform nextRoom)
    {
        currentPosition = nextRoom.position.x;
    }
}
