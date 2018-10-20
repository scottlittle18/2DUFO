using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Overhead2DPlayerController : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    

    private float moveHorizontal, moveVertical;

    int scoreCounter, pickupTotal, pickupsLeft, currentLevel;

    [SerializeField]
    Text score, winText;
    [SerializeField]
    float moveSpeed, levelGoal;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        scoreCounter = 0;
        winText.text = "";
        pickupTotal = GameObject.FindGameObjectsWithTag("PickUp").Length;
        pickupsLeft = GameObject.FindGameObjectsWithTag("PickUp").Length;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        SetScoreText();
    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        myRigidBody.AddForce(movement * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PickUp")
        {
            collision.gameObject.SetActive(false);
            scoreCounter++;
            pickupsLeft = pickupTotal--;
            SetScoreText();
        }               
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {            
            scoreCounter--;
            SetScoreText();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal" && Input.GetKeyDown(KeyCode.Space));
        {
            Debug.Log("NextLevel");
            SceneManager.LoadScene(currentLevel + 1);
        }
    }

    void SetScoreText()
    {
        score.text = "Score: " + scoreCounter.ToString();

        if (scoreCounter >= pickupTotal)
            winText.text = "You Win!!!";
        else if (scoreCounter < levelGoal && pickupsLeft <= pickupTotal * (levelGoal/100) - scoreCounter)
            winText.text = "You Lose!";
    }
}
