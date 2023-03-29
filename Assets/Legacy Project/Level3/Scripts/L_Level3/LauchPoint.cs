using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauchPoint : MonoBehaviour
{
    public Transform player;
    public Transform startTran;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Player").transform;
        startTran=this.transform;
        offset = startTran.position-player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!BulletPool.instance.isCheckPoint3)
            {
                //跟随玩家，与玩家保持和初始位置相同的距离  
                this.transform.position = player.position + offset;
            }
    }
}
