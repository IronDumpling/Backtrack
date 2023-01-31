using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet instance;
    private float speed = 0.03f;
    private float timeBullet;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BulletPool.instance.isCheckPoint1)
        {
            timeBullet += Time.deltaTime;
            if (this.tag=="Two")
            {
                if(timeBullet>5f)
                    BulletMovement();
            }
            else
            {
                if (timeBullet > 8f)
                {
                    BulletMovement();
                }
            }

        }
        else if(!BulletPool.instance.isCheckPoint3)
        {
            BulletMovement();
            timeBullet += Time.deltaTime;
            if (timeBullet >= 6f)
            {
                this.gameObject.SetActive(false);
                timeBullet = 0;
            }
        }
        else if(BulletPool.instance.isCheckPoint3)
        {
            BulletMovementUp();
            timeBullet += Time.deltaTime;
            if (timeBullet >= 6f)
            {
                this.gameObject.SetActive(false);
                timeBullet = 0;
            }
        }
    }
    public void BulletMovement()
    {
        transform.Translate(Vector3.forward * speed, Space.Self);
    }
    public void BulletMovementUp()
    {
        //绕自身x轴旋转90度
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, this.transform.rotation.y, this.transform.rotation.z), 0.1f);
        transform.Translate(Vector3.forward * speed, Space.Self);
    }
}
