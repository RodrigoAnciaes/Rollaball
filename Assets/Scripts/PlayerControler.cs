using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
     public float speed = 0; 
     public TextMeshProUGUI countText;
     public TextMeshProUGUI livesText;
     public TextMeshProUGUI timerText; // Added timer text
     public GameObject winTextObject;
     public GameObject loseTextObject;

     private Rigidbody rb;
     private float movementX;
     private float movementY;
     private int count;
     public int lives;
     private float timer = 20; // Timer set to 60 seconds
     private bool gameOver;
     private bool won;

     // Start is called before the first frame update
     void Start()
     {
          rb = GetComponent<Rigidbody>();
          count = 0; 
          lives = 3; // Initialize the number of lives
          SetCountText();
          winTextObject.SetActive(false);
          loseTextObject.SetActive(false);
          gameOver = false;
          won = false;
     }

     void OnMove(InputValue movementValue)
     {
          Vector2 movementVector = movementValue.Get<Vector2>();
          movementX = movementVector.x; 
          movementY = movementVector.y; 
     }

     void SetCountText() 
     {
          countText.text = "Score: " + count.ToString();
          livesText.text = "Lives: " + lives.ToString(); // Display the number of lives
          timerText.text = "Time: " + timer.ToString("F0"); // Display the timer F0 rounds to the nearest whole number
          if (count >= 12)
          {
               winTextObject.SetActive(true);
               won = true;
          }
     }

     private void FixedUpdate() 
     {
          Vector3 movement = new Vector3(movementX, 0.0f, movementY);
          rb.AddForce(movement * speed); 
     }

     void OnTriggerEnter(Collider other) 
     {

          if (other.gameObject.CompareTag("Pickup")) 
          {
               other.gameObject.SetActive(false);
               if (gameOver)
               {
                    return;
               }
               count++;
               SetCountText();
          }
     }

     public void TakeDamage(int damageAmount)
     {
          if (gameOver || won)
          {
               return;
          }
          lives -= damageAmount;
          SetCountText();
          if (lives <= 0)
          {
               loseTextObject.SetActive(true);
               gameOver = true;
          }
     }

     // Reset the scene when the reset key is pressed
     // the reset key is the "R" key
     void Update()
     {
          if (Keyboard.current.rKey.wasPressedThisFrame)
          {
               SceneManager.LoadScene(SceneManager.GetActiveScene().name);
          }

          // Update the timer
          if (timer > 0 && gameOver == false && won == false)
          {
               timer -= Time.deltaTime;
               SetCountText();
               if (timer <= 0 && won == false)
               {
                    loseTextObject.SetActive(true);
                    gameOver = true;
               }
          }
     }
}
