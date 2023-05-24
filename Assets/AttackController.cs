using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private bool comboReady = false;
    public bool attackReady = true;
    [SerializeField]Animator animator;
    public PlayerController playerController;
    public Rigidbody rb;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (comboReady)
                animator.SetTrigger("Attack2");
            else if (attackReady)
                animator.SetTrigger("Attack1");
        }
    }

    void ComboReady() 
    {
        comboReady = true;
    }
    void ComboOver() 
    {
        comboReady = false;
    }

    private void Attacking() 
    {
        attackReady = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX 
                       | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void AttackReady()
    {
        attackReady = true;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
}
