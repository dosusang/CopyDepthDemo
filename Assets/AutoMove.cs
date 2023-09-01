using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        transform.Rotate(new Vector3(2, 0, 0));
    }
}
