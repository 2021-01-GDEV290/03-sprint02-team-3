using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.GetComponent<HealthTracker>().health -= damage;
        }
        Destroy(this.gameObject);
    }
}
