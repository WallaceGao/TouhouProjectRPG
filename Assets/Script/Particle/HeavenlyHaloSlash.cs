using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenlyHaloSlash : MonoBehaviour
{
    [SerializeField]
    public GameObject impactEffect;

    private void Update()
    {
        Create();
    }
    void Create()
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
