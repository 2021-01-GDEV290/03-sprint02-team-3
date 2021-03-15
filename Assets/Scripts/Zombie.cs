using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int damage;

    bool runContinueDamage = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Zombie collided with " + collision.gameObject.tag);
        runContinueDamage = true;
        StartCoroutine(ContinueDamage(collision));
        return;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Zombie left collision with " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            runContinueDamage = false;
        }
    }

    IEnumerator ContinueDamage(Collision2D collision)
    {
        while (runContinueDamage)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<HealthTracker>().Damage(damage);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
