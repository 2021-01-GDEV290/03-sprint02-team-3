using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTracker : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int damage;
    public string[] tags;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
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
}
