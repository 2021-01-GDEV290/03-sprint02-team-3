using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTracker : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int damage;
    public string[] tags;

    bool runContinueDamage = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
        if(this.gameObject.tag == "Zombie")
        {
            runContinueDamage = true;
            StartCoroutine(ContinueDamage(collision));
            return;
        }
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.gameObject.tag == tags[i])
            {
                collision.gameObject.GetComponent<HealthTracker>().Damage(damage);
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            runContinueDamage = false;
        }
    }

    IEnumerator ContinueDamage(Collision2D collision)
    {
        while(runContinueDamage)
        {
            collision.gameObject.GetComponent<HealthTracker>().Damage(damage);
            yield return new WaitForSeconds(1f);
        }
    }
}
