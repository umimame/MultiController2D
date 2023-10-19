using UnityEngine;

public class PlayerTest : MonoBehaviour
{
     float moveSpeed;

    private void Start()
    {
        moveSpeed = 1f;
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 movement = new Vector3(0f, 1f, 0f) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 movement = new Vector3(0f, -1f, 0f) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        if(Input.GetKey(KeyCode.D))
        {
            Vector3 movement = new Vector3(1f, 0f, 0f) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        if(Input.GetKey(KeyCode.A))
        {
            Vector3 movement = new Vector3(-1f, 0, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}
