using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 50f;
    public bool isRight;
    public bool isLeft;
    private bool isMoving = false;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
         
        if (isMoving) {
            //基于自身坐标系
            RightMove();
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            isMoving = true;
        }
    }

    public void RightMove()//基于目标自身坐标系向右移动
    {
        this.transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
