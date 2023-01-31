using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public GameObject cameraon;
    public GameObject camerto;
    public GameObject camerto1;
    public GameObject camerto2;
    public CinemachineBrain cb;
    public GameObject Wall1;
    public GameObject Wall2;
    private float speed = 1f;
    private Vector3 velocity;
    private Vector3 offset;
    [SerializeField]
    private Transform tran;

    // Start is called before the first frame update
    void Start()
    {
        cb=GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tran == null && GameObject.FindGameObjectWithTag("Player"))
        {
            tran = GameObject.FindGameObjectWithTag("Player").transform;
            offset = camerto1.transform.position - tran.position;
        }
        if (BulletPool.instance.isCheckPoint1)
        {
            Wall1.SetActive(false);
            Wall2.SetActive(true);
            cameraon.transform.position = new Vector3(Mathf.SmoothDamp(cameraon.transform.position.x, camerto.transform.position.x,
               ref velocity.x, speed), Mathf.SmoothDamp(cameraon.transform.position.y, camerto.transform.position.y,
               ref velocity.y, speed), Mathf.SmoothDamp(cameraon.transform.position.z, camerto.transform.position.z, ref velocity.z, speed));
            cameraon.transform.rotation = Quaternion.RotateTowards(cameraon.transform.rotation, camerto.transform.rotation, Time.deltaTime * 20);
        }
        
        // if (BulletPool.instance.isCheckPoint2)
        // {
        //     /*cameraon.transform.position = new Vector3(Mathf.SmoothDamp(cameraon.transform.position.x, camerto1.transform.position.x,
        //        ref velocity.x, speed), Mathf.SmoothDamp(cameraon.transform.position.y, camerto1.transform.position.y,
        //        ref velocity.y, speed), Mathf.SmoothDamp(cameraon.transform.position.z, camerto1.transform.position.z, ref velocity.z, speed));*/
        //     cameraon.transform.rotation = Quaternion.RotateTowards(cameraon.transform.rotation, camerto1.transform.rotation, Time.deltaTime * 20);


        //     float posX = Mathf.SmoothDamp(transform.position.x, tran.position.x + offset.x, ref velocity.x, 0.05f);//2
        //     float posY = Mathf.SmoothDamp(transform.position.y, tran.position.y + offset.y, ref velocity.y, 0.05f);
        //     float posZ = Mathf.SmoothDamp(transform.position.z, tran.position.z + offset.z, ref velocity.z, 0.05f);
        //     transform.position = new Vector3(posX, posY, posZ);

        // }
        if(BulletPool.instance.isCheckPoint2&&!BulletPool.instance.isCheckPoint3)//玩家在屏幕中间2*2区域内移动摄像头不跟随
        {
            cb.enabled = true;
        }
        if(BulletPool.instance.isCheckPoint3){
            cb.enabled = false;
            
            this.gameObject.SetActive(false);
        }
        
        
       
    }
 }

