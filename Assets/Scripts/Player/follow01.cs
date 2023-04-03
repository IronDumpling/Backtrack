using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow01 : MonoBehaviour
{
    private float 跟随速度 = 10f;

    public GameObject 跟随碎片的节点;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, 跟随碎片的节点.transform.position, Time.deltaTime * 跟随速度);
    }
}
