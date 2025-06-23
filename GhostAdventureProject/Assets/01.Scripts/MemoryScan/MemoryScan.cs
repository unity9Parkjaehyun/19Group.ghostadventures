using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryScan : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        ScanMemory();
    }
    

    public void ScanMemory()
    {
        float time = 0f;

        if(Input.GetKey(KeyCode.E))
        {
            time += Time.time;
            Debug.Log("Memory scan started at: " + time);

            if(time >=4f)
            {
                Debug.Log("Memory scan completed.");
                time = 0f; // Reset time after scan
            }
        }


    }
}
