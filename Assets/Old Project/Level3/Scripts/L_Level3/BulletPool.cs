using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    private float currentTime;//计时器
    private float finalTime = 2f;//生成子弹的时间间隔
    private int sendNumber = 0;
    private int finalNumber = 10;
    private Transform Pooltran;
    private Transform Playertran;
    private float pointTime;
    private bool noPool=true;
    
    public bool isCheckPoint1=false;//是否到达第一个检查点
    public bool isCheckPoint2=false;
    public bool isCheckPoint3=false;
    public GameObject point1;//第一个检查点
    public Transform tran1;//生成子弹的位置
    public Transform tran2;//生成子弹的位置
    public Transform tran3;//生成子弹的位置
    public Transform tran4;//生成子弹的位置
    public Transform tran5;//生成子弹的位置
    public Transform tran6;//生成子弹的位置
    public Transform tran7;//生成子弹的位置
    
    public Transform tran8;//生成子弹的位置
    public Transform tran9;//生成子弹的位置
    public Transform tran10;//生成子弹的位置
    

    
    
    
    
    
    public BulletControl bulletControl;
    
    
    public GameObject bullet;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Playertran=GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCheckPoint2&&!isCheckPoint3)
        {
            currentTime+=Time.deltaTime;
            if(currentTime>=3f)
            {
                currentTime=0f;
                GeneratePool_3D();
                
            }
            
            
            Pooltran=PointScene();
        }
        if(isCheckPoint3){
            currentTime+=Time.deltaTime;
            if(currentTime>=3f)
            {
                currentTime=0f;
                GeneratePool_point3();
                
            }
        }
       
        Debug.DrawLine(this.transform.position, this.transform.position + Vector3.up * 10, Color.red);

        if (sendNumber == finalNumber)
        {
            
            isCheckPoint1 = true;
            point1.SetActive(true);
        }
        currentTime += Time.deltaTime;
        if (currentTime >= finalTime && sendNumber<finalNumber)
        {
            GeneratePool();
            currentTime = 0f;
            sendNumber++;
        }
        if (isCheckPoint1==true)//到达第一个检查点12s后isCheckPoint1=false,isCheckPoint2=true
        {
            pointTime += Time.deltaTime;
            if (pointTime >= 12f)
            {
                isCheckPoint1 = false;
                isCheckPoint2 = true;
                

            }
        }
        
    }
    public void GeneratePool()//生成子弹
    {
        
        int index = Random.Range(1,4);
        switch (index)
        {
            case 1:
                JudgePool(tran1);
                if(noPool)
                    bulletControl.pool.Add(Instantiate<GameObject>(bullet, tran1));
                break;
            case 2:
                JudgePool(this.transform);
                if (noPool)
                    bulletControl.pool.Add(Instantiate<GameObject>(bullet, this.transform));              
                break;
            case 3:
                JudgePool(tran2);
                if (noPool)//如果子弹池中没有子弹
                    bulletControl.pool.Add(Instantiate<GameObject>(bullet, tran2));
                break;
        }
            
        
    }
    public void GeneratePool_3D()
    {
        int index = Random.Range(1,9);
        switch (index)
        {
            case 1:
                Instantiate<GameObject>(bullet, tran1);
                break;
            case 2:
                Instantiate<GameObject>(bullet, this.transform);
                
                break;
            case 3:
                Instantiate<GameObject>(bullet, tran2);
                break;
            case 4:
                Instantiate<GameObject>(bullet, tran3);
                break;
            case 5:
                Instantiate<GameObject>(bullet, tran4);
                
                break;
            case 6:
                Instantiate<GameObject>(bullet, tran5);
                break;
            case 7:
                Instantiate<GameObject>(bullet, tran6);
                break;
            case 8:
                Instantiate<GameObject>(bullet, tran7);
                
                break;
            
        }
    }

    public void JudgePool(Transform tran)//判断子弹池中是否有子弹
    {
        foreach (var item in bulletControl.pool)
        {
            if (!item.activeSelf)//如果子弹池中没有子弹
            {
                item.SetActive(true);
                item.transform.position = tran.position;
                               
                noPool = false;//子弹池中有子弹
                break;
            }
        }
    }
    public void GeneratePool_point3()
    {
        int index = Random.Range(1,4);
        switch (index)
        {
            case 1:
                Instantiate<GameObject>(bullet, tran8);
                break;
            case 2:
                Instantiate<GameObject>(bullet, tran9);
                
                break;
            case 3:
                Instantiate<GameObject>(bullet, tran10);
                break;
        }
    }
    // public void MovePool()//判断是否到达checkPoint2,如果到达让该池子跟随玩家移动
    // {
    //     if (isCheckPoint2)//到达checkPoint2
    //     {
    //         if(noPool)//如果子弹池中没有子弹
    //             bulletControl.pool.Add(Instantiate<GameObject>(bullet, this.transform));
    //     }
    //     else{
    //         foreach (var item in bulletControl.pool)
    //         {
    //             if (!item.activeSelf)//如果子弹池中没有子弹
    //             {
    //                 item.SetActive(true);
    //                 item.transform.position = this.transform.position;
    //                 noPool = false;//子弹池中有子弹
    //                 break;
    //             }
    //         }
    //     }
    // }
     public Transform PointScene()//获得相对于玩家z轴前方6f的位置
     {
         Vector3 point = Playertran.position + Playertran.forward * 6f;
         this.transform.position = point;
         return this.transform;
     }
     
    
}
