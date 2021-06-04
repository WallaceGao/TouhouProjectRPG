using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Function1", 10);   
        Invoke("Function2", 5);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Function1()
    {
        print("function 1");
    }
    void Function2()
    {
        print("function 2");
    }
}
