using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BridControl : MonoBehaviour
{
    public static BridControl instance;
    private Rigidbody rb;
    private bool isVerital=true;
    
    private float downTime;
    
    private bool isHorizontal = true;
    [SerializeField]
    private float speed = 0.001f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Movement();
    }
    private void FixedUpdate()
    {
        if (!BulletPool.instance.isCheckPoint2&&!BulletPool.instance.isCheckPoint3)
        {
            Movement();
        }
        if (BulletPool.instance.isCheckPoint2&&!BulletPool.instance.isCheckPoint3)
        {
            downTime += Time.fixedDeltaTime;
            if (downTime < 3f)
                transform.Translate(Vector3.down * Time.fixedDeltaTime * speed);
            else if(downTime<3.05f)
                rb.velocity = new Vector3(0, 0, 0);
            else
            {
                TwoMovement();
            }

        }
        if(BulletPool.instance.isCheckPoint3)
        {
            //围绕自身x轴旋转-90度
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, 60.64f,0), 0.1f);
            UpMovement();
        }
    }
    public void Movement()
    {
        
        if (Input.GetAxisRaw("Vertical") != 0 && isVerital == true)
        {
            //rb.velocity=new Vector3(0,rb.velocity.y,rb.velocity.z);
            isVerital = true;
            isHorizontal = false;
            //rb.AddForce(Vector3.forward * speed * Time.deltaTime * Input.GetAxisRaw("Vertical"),ForceMode.Impulse);
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical") * speed, Space.Self);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 && isHorizontal == true)
        {
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            isVerital = false;
            isHorizontal = true;
            //rb.AddForce(Vector3.right * speed*Time.deltaTime * Input.GetAxisRaw("Horizontal"), ForceMode.Impulse);
            transform.Translate(Vector3.right * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal") * speed, Space.Self);
        }

        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            isHorizontal = true;
            isVerital = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet"|| collision.gameObject.tag == "Two")
        {
            collision.gameObject.SetActive(false);
        }





    }

    private void TwoMovement()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime *speed, Space.Self);
        if (Input.GetAxisRaw("Vertical")!=0)
        {
            
            transform.Translate(Vector3.up * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical") * speed, Space.Self);
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {

            transform.Translate(Vector3.right * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal") * speed, Space.Self);
        }
    }
    public void UpMovement()
    {
        
        if (Input.GetAxisRaw("Vertical") != 0 && isVerital == true)
        {
            //rb.velocity=new Vector3(0,rb.velocity.y,rb.velocity.z);
            isVerital = true;
            isHorizontal = false;
            //rb.AddForce(Vector3.forward * speed * Time.deltaTime * Input.GetAxisRaw("Vertical"),ForceMode.Impulse);
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical") * speed, Space.Self);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 && isHorizontal == true)
        {
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            isVerital = false;
            isHorizontal = true;
            //rb.AddForce(Vector3.right * speed*Time.deltaTime * Input.GetAxisRaw("Horizontal"), ForceMode.Impulse);
            transform.Translate(Vector3.right * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal") * speed, Space.Self);
        }

        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            isHorizontal = true;
            isVerital = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Point3")
        {
            rb.velocity=new Vector3(0,0,0);
            BulletPool.instance.isCheckPoint3 = true;
            
        }
        
    }
    
}
