using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool BirdStatus = true;

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
     logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
     audioSource.Play(); 
    }

    // Update is called once per frame
    void Update()
    {

        // myRigidbody.velocity = Vector2.up * 10;
        if (Input.GetKeyDown(KeyCode.Space) == true && BirdStatus == true)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }

        BirdOnScreen();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        BirdStatus = false;
    }

    private void BirdOnScreen()
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            logic.gameOver();
            BirdStatus = false;

        }
    }

}
