using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Collider collider;
    
    void Start()
    {
        collider = GetComponent<Collider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
    }
}
