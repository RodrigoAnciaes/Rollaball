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
     public GameObject winTextObject;
     public GameObject loseTextObject;

     private Rigidbody rb;
     private float movementX;
     private float movementY;
     private int count;
     public int lives;

     // Start is called before the first frame update
     void Start()
     {
          rb = GetComponent<Rigidbody>();
          count = 0; 
          lives = 3; // Initialize the number of lives
          SetCountText();
          winTextObject.SetActive(false);
          loseTextObject.SetActive(false);
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
          if (count >= 12)
          {
               winTextObject.SetActive(true);
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
               if (lives <= 0)
               {
                    return;
               }
               count++;
               SetCountText();
          }
     }

     public void TakeDamage(int damageAmount)
     {
          if (lives <= 0)
          {
               return;
          }
          lives -= damageAmount;
          SetCountText();
          if (lives <= 0)
          {
               loseTextObject.SetActive(true);
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
     }
}
