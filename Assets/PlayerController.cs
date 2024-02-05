using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform missileSpawnPoint; 
    public float missileSpeed = 0.0f;
    public float fireCooldown = 3.0f;
     private float lastFireTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && Time.time - lastFireTime >= fireCooldown)
        {
            FireMissile();
            lastFireTime = Time.time; 
        }
    }

    void FireMissile()
    {
        GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
        
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(missileSpeed, 0.0f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pipe"))
        {
            Destroy(collision.gameObject);
        }
    }
}