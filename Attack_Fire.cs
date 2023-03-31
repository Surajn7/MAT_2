using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Fire : MonoBehaviour
{
    [SerializeField] private float speed;
    private BoxCollider2D boxCollider;
    private bool hit;
    private float direction = 3;
    private float fireTime;
    public Animator Anime;
    
    private void Awake()
    {
        Anime = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        fireTime = fireTime + Time.deltaTime;

        if (fireTime > 5)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      hit = true;
      boxCollider.enabled = false;
      Anime.SetTrigger("explode");
    }

    public void setDirection(float fireDirection)
    {
        direction = fireDirection;
        gameObject.SetActive(true);                
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;        
        if (Mathf.Sign(localScaleX) != fireDirection)
            localScaleX = -localScaleX;

        transform.position = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
