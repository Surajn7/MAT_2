using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField]  private float speed;
    public Health health;
    private bool movingLeft;
    private float leftSide;
    private float rightSide;

    private void Awake()
    {
        leftSide = transform.position.x - movementDistance;
        rightSide = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if(movingLeft)
        {
            if (transform.position.x > leftSide)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightSide)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Player>() != null)
        {
            health.takeDamage(1);
        }
    }
}
