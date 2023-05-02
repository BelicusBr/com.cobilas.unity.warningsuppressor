using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        foreach (var item in Client.List().Result)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
