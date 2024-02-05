using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour
{

    private void onCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BottomPipe"))
                {
            Destroy(collision.gameObject); // Destroy the pipe
        }

        if (collision.gameObject.CompareTag("TopPipe"))
                {
            Destroy(collision.gameObject);
        }
    }


    private void Update()
    {
        if (transform.position.x > Screen.width)
        {
            Destroy(gameObject);
        }
    }
}

