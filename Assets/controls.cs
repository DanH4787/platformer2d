using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public float airControlForce = 10.0f;
    public float airControlMax = 1.5f;
    public bool grounded;
    public AudioSource coinSound;
    public TextMeshProUGUI uiText;
    int totalCoins;
    int coinsCollected;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        // find out how many coins in the level
        coinsCollected = 0;
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }
    // Update is called once per frame
    void Update()
    {
        string uiString = "x " + coinsCollected + "/" + totalCoins;
        uiText.text = uiString;
        {
            float blinkVal = Random.Range(0.0f, 200.0f);
            if (blinkVal < 1.0f)
                animator.SetTrigger("blinktrigger");
            float xSpeed = Mathf.Abs(rigidBody.linearVelocity.x);
            animator.SetFloat("xspeed", xSpeed);
            float ySpeed = Mathf.Abs(rigidBody.linearVelocity.y);
            animator.SetFloat("yspeed", ySpeed);
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
        if ( collision.gameObject.tag == "Death" )
 {
        SceneManager.LoadScene(0);
 }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Coin")
        {
            Destroy(coll.gameObject);
            coinSound.Play();
            coinsCollected++;
        }
    }
   
}