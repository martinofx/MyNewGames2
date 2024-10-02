using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 10f;
    public int health = 100;
    public int damage = 50;
    private Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Jugador no encontrado. Asegúrate de que el objeto 'Player' tenga la etiqueta correcta.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                TakeDamage(100);
            }
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(35);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
