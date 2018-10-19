using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overhead2DPlayerController : MonoBehaviour {

    private Rigidbody2D myRigidBody;

    private float moveHorizontal, moveVertical;

    int scoreCounter;

    [SerializeField]
    Text score;
    [SerializeField]
    float moveSpeed;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
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
        if (collision.tag == "Pickup")
        {
            collision.gameObject.SetActive(false);
            score.text = scoreCounter++.ToString();
        }            
    }
}
