using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidEnemyOverlap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                float force = 10f;
                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }
}
