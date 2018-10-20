using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overhead2DPlayerController : MonoBehaviour {

    private Rigidbody2D myRigidBody;

    private float moveHorizontal, moveVertical;

    int scoreCounter, pickupTotal, pickupsLeft;

    [SerializeField]
    Text score, winText;
    [SerializeField]
    float moveSpeed;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        scoreCounter = 0;
        winText.text = "";
        pickupTotal = GameObject.FindGameObjectsWithTag("PickUp").Length;
        pickupsLeft = GameObject.FindGameObjectsWithTag("PickUp").Length;
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
        else if (collision.tag == "Hazard")
        {
            collision.gameObject.SetActive(false);
            scoreCounter--;
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        score.text = "Score: " + scoreCounter.ToString();

        if (scoreCounter >= 12)
            winText.text = "You Win!!!";
        else if (scoreCounter < pickupTotal && pickupsLeft == 0)
            winText.text = "You Lose!";
    }
}
