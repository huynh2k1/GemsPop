using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    private void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.y = Target.position.y;
        transform.position = transformPosition;
    }
}
