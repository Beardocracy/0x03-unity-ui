using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 900f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (health == 0)
        {
            //Debug.Log("Game Over!");
            SetGameOver();
            StartCoroutine(LoadScene(3));
        }
        if (Input.GetKey(KeyCode.Escape))
        {
        SceneManager.LoadScene("menu");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }      
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            rb.AddForce(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap")
        {
            health -= 1;
            SetHealthText();
            //Debug.Log("Health: " + health);
        }
        if (other.tag == "Pickup")
        {
            Destroy(other.gameObject);
            score += 1;
            SetScoreText();
            //Debug.Log("Score: " + score);
        }
        if (other.tag == "Goal")
        {
            //Debug.Log("You win!");
            SetGoalReached();
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }

    void SetGoalReached()
    {
        GameObject parent = winLoseText.transform.parent.gameObject;
        Image img = parent.GetComponent<Image>();

        winLoseText.text = "You Win!";
        winLoseText.color = Color.black;
        img.color = Color.green;
        parent.SetActive(true);
    }

    void SetGameOver()
    {
        GameObject parent = winLoseText.transform.parent.gameObject;
        Image img = parent.GetComponent<Image>();

        winLoseText.text = "Game Over!";
        winLoseText.color = Color.white;
        img.color = Color.red;
        parent.SetActive(true);
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
