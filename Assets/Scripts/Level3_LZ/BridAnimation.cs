using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridAnimation : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        _animator.SetFloat("isCurve",Input.GetAxisRaw("Horizontal"));
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            _animator.SetBool("isIdle",true);
        }
        else
        {
            _animator.SetBool("isIdle",false);
        }
        
    }
}
