using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float initialHealth;
    public float currentHealth;
    public Animator Anime;

    private void Awake()
    {
        Anime = gameObject.GetComponent<Animator>();
        currentHealth = initialHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, initialHealth);

        if(currentHealth > 0)
        {
            Anime.SetTrigger("Hurt");
        }
        else
        {
            Anime.SetTrigger("Death");
            GetComponent<Player>().enabled = false;
        }
    }
}