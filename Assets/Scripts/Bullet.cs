using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "Fast Zombie" ||
            collision.gameObject.tag == "Ranged Zombie" || collision.gameObject.tag == "Boss Zombie")
        {
            collision.gameObject.GetComponent<Zombie>().Damage(damage);
        }
        Destroy(gameObject);
    }
}
