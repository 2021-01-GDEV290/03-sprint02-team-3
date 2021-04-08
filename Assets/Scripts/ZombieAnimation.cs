using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    public Animator animator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            animator.SetBool("Hit", true);
            Invoke("ResetAnimation", 0.2f);
        }
    }

    void ResetAnimation()
    {
        animator.SetBool("Hit", false);
    }
}
