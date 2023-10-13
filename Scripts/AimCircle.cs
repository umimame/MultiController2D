using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCircle : MonoBehaviour
{
    [field: SerializeField] public CircleClamp clamp { get; set; }
    [field: SerializeField] public Vector3 inputAimPos { get; set; }
    [field: SerializeField] public Vector3 aimPosInCamera { get; set; }
    [field: SerializeField] public GameObject parentObj { get; set; }
    [field: SerializeField] public Vector3 lookingPos { get; set; }
    private void Start()
    {
        clamp.Initialize();
    }
    protected virtual void Update()
    {
        transform.position = new Vector3(aimPosInCamera.x, aimPosInCamera.y, 0);
        lookingPos = (transform.TransformPoint(transform.position) - parentObj.transform.position).normalized;
        clamp.Limit();
    }
}

