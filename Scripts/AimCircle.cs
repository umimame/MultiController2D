using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCircle : MonoBehaviour
{
    [SerializeField] private Vector3 mousePos;
    [SerializeField] private Vector3 mousePosInCamera;
    private void Update()
    {
        mousePos = Input.mousePosition;
        mousePosInCamera = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mousePosInCamera.x, mousePosInCamera.y, 0);
    }
}
