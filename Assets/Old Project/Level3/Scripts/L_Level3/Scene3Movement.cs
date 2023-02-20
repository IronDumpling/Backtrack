using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BulletPool.instance.isCheckPoint3)//到达CheckPoint3时，让场景从上往下移动
        {
            transform.Translate(Vector3.down * 0.1f);
        }
        
    }
}
