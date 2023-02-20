using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject point;
    public List<Transform> childBullet;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in point.transform)
        {
            childBullet.Add(child);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }
}
