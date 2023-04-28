using System;
using Common;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    [Header("动画Param，与Animator中参数配套")]
    [SerializeField] public string animParam_Speed = "speed";

    [SerializeField] public string animParam_XSpeed = "xSpeed";

    [SerializeField] public string animParam_Eat = "triggerEat";
    private bool canPlayAnim;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        canPlayAnim = true;
    }

    public void SetFloat(String name, float f)
    {
        if (canPlayAnim)
        {
            
            animator.SetFloat(name,f);
        }
    }

    public void SetTrigger(String name)
    {
        if (canPlayAnim)
        {
            animator.SetTrigger(name);
        }
    }


}

