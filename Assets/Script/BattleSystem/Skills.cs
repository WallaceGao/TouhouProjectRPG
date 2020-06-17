using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField]
    string name;
    [SerializeField]
    int range;
    [SerializeField]
    float damage;
    [SerializeField]
    float cost;

    public string Name { get { return name; } }
}
