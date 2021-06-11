using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private float offset;

    private void Start()
    {
        offset = transform.position.z;
    }

    private void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, offset);
    }
}
