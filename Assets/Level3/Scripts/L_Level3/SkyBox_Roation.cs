using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox_Roation : MonoBehaviour
{
    public Skybox skybox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateSkyPoint3();
    }
    // public void RotateSky()
    // {
    //     float num = Camera.main.GetComponent<Skybox>().material.GetFloat("_Rotation");
    //     Camera.main.GetComponent<Skybox>().material.SetFloat("_Rotation", num + 0.002f);
    // }
    public void RotateSkyPoint3()//到达CheckPoin3时，让天空盒从上往下旋转
    {
        if(BulletPool.instance.isCheckPoint3)
        {
        float num = skybox.material.GetFloat("_Rotation");

        
        skybox.material.SetFloat("_Rotation", num - 0.002f);
        }
    }
    }
