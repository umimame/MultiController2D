using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private float speed = 0.01f;
    
    private SpriteRenderer renderer;
    

    void Start()
    {
        
        renderer = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        Vector2 position = transform.position;

        if (Input.GetKey("left"))
        {
            position.x -= speed;
            
            renderer.flipX = false;
            
        }
        else if (Input.GetKey("right"))
        {
            position.x += speed;
            
            renderer.flipX = true;
     
        }
        else if (Input.GetKey("up"))
        {
            position.y += speed;
        }
        else if (Input.GetKey("down"))
        {
            position.y -= speed;
        }

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy_Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
