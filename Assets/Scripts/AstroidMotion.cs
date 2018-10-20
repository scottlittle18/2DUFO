using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMotion : MonoBehaviour {

    Rigidbody2D astroid;    

    [SerializeField]
    float speed, speedInc, maxSpeed;

    private void Start()
    {
        astroid = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate () {
        Vector2 movement = new Vector2(astroid.position.x, astroid.position.y);
        astroid.AddForce(movement * speed);
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && speed < maxSpeed)
            speed += speedInc;
        else if (collision.gameObject.tag == "Player" && speed < maxSpeed)
            speed += speedInc;
    }
}
