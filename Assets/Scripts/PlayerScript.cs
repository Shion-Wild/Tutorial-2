using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public GameObject Player;
    public float speed;
    public Text score;
    public Text lives;
    public Text winScreen;
    public Text loseScreen;
    public AudioSource backgroundMusic;
    public AudioSource winMusic;
    public int lifeTotal;
    private int scoreValue;
    private int lifeScore;
    private Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        lifeScore = lifeTotal;

        //BGMusic.Play();

        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + lifeScore.ToString();
        winScreen.enabled = false;
        loseScreen.enabled = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        if(!winScreen.enabled)
        {
        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        }
        else
        {
        rd2d.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            if(scoreValue == 4)
            {
                if(currentScene.name == "SampleScene")
                {
                    SceneManager.LoadScene("Level2");
                    lifeScore = lifeTotal;
                }
                else
                {
                    winScreen.enabled = true;

                    backgroundMusic.Stop();
                    winMusic.Play();

                    Physics2D.gravity = new Vector2(0,0);
                    Player.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            lifeScore -= 1;
            lives.text = "Lives: " + lifeScore.ToString();
            if(lifeScore == 0)
            {
                Player.SetActive(false);
                loseScreen.enabled = true;
            }
        }
    }

    
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Floor")
        {
            if(Input.GetKey(KeyCode.Space))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }
        }
    }
}
