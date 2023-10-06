using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 100.0f;
    

    void Update()
    {
        float add_move = moveSpeed;
    }

    public void SetMoveSpeed(float _speed)
    {
        moveSpeed = _speed;
    }

    
}
