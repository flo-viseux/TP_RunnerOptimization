using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehaviour : MonoBehaviour
{
    [SerializeField] private float _RotateSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * _RotateSpeed * Time.deltaTime);
    }
}
