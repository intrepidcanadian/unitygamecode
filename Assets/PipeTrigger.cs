using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTrigger : MonoBehaviour
{
    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    
    {

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // FlyingBird is Layer 3
        if (collision.gameObject.layer == 3)
        {
            logic.addScore(2);
        }
        Debug.Log("Pipe Triggered");
    }
}
