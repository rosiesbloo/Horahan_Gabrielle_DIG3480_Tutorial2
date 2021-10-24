using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;

    Animator anim;

    public float speed;
    public Text score;
    public Text lives;

    public GameObject winTextObject;
    public GameObject loseTextObject;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    private int scoreValue = 0;
    private int livesValue = 3;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        lives.text = "Lives: " + livesValue.ToString();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
            anim.SetBool("isRunning", true);
            }

        else
            {
            anim.SetBool("isRunning", false);
            }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isJumping", true);
        }


        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector3(115.0f, 1.0f, 0.0f);
                livesValue = 3;
                lives.text = "Lives: " + livesValue.ToString();
            }

            if (scoreValue == 8)
            {
                winTextObject.SetActive(true);
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue += -1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);

            if (livesValue <= 0)
            {
                loseTextObject.SetActive(true);
                winTextObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetBool("isJumping", false);

            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

}
