using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point3Camera : MonoBehaviour
{
    public new Camera camera;
    public BoxCollider box1;
    public BoxCollider box2;
    public BoxCollider box3;
    public BoxCollider box4;
    public Transform player;
    public Transform startTran;
    public Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        
        camera=GetComponent<Camera>();
        offset = startTran.position-player.position;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //运行时用红线将标注出摄像头与玩家的距离
        Debug.DrawLine(player.position, this.transform.position, Color.red);
        
        if(!BulletPool.instance.isCheckPoint3)
            {
                //跟随玩家，与玩家保持和初始位置相同的距离  
                this.transform.position = player.position + offset;
                
                
            }
        if(BulletPool.instance.isCheckPoint3){
            box1.enabled=true;
            box2.enabled=true;
            box3.enabled=true;
            box4.enabled=true;
            camera.enabled=true;
            
        }
    }
}
