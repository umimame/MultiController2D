using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy_Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}