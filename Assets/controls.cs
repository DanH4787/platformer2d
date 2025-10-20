using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float speed = 5.0f;
    public float jumpForce = 8.0f;
    public float airControlForce = 10.0f;
    public float airControlMax = 1.5f;
    public bool grounded;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        {
            if (rigidBody.linearVelocity.x * transform.localScale.x < 0.0f)
                transform.localScale = new Vector3(-transform.localScale.x,
                transform.localScale.y, transform.localScale.z);
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        // check if we are on the ground
        if (grounded)
        {
            if (Input.GetAxis("Jump") > 0.0f)
                rigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            else
                rigidBody.linearVelocity = new Vector2(speed * h, rigidBody.linearVelocityY);
            //phoebe did this :P
            // allow a small amount of movement in the air
            float vx = rigidBody.linearVelocityX;
            if (h * vx < airControlMax)
                rigidBody.AddForce(new Vector2(h * airControlForce, 0));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = false;
        }
    }
}