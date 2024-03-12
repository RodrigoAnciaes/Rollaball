using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float speed = 5f;
    public int damageAmount = 1; // Amount of damage caused to the player

    // assign the player to the player variable
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Decrease player's health
            PlayerControler PlayerControler = collision.gameObject.GetComponent<PlayerControler>();
            if (PlayerControler != null)
            {
                PlayerControler.TakeDamage(damageAmount);
            }

            // apply force to the player
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = collision.gameObject.transform.position - transform.position;
                rb.AddForce(direction * 50, ForceMode.Impulse);
            }

        }
    }
}
