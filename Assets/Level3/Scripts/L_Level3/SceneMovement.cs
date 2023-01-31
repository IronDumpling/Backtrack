using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovement : MonoBehaviour
{
    public Transform playerTran;
    public BoxCollider box1;
    public BoxCollider box2;
    public BoxCollider box3;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        playerTran=GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!BulletPool.instance.isCheckPoint2&&!BulletPool.instance.isCheckPoint3)
            Movement();
        else if(BulletPool.instance.isCheckPoint2&&!BulletPool.instance.isCheckPoint3)
        {
                currentTime+=Time.deltaTime;
                    if(currentTime>=3f)
                    {
                        box1.enabled=true;
                        box2.enabled=true;
                        box3.enabled=true;
                    }
            
        }
    }
    void Movement()//相对playerTran向后移动
    {
        transform.Translate(Vector3.back * Time.deltaTime * 3f, Space.Self);
        
    }
   
    
}
