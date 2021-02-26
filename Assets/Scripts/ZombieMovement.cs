using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject wayPoint;
    public float speed = 6.0f;

    private Vector3 wayPointPos;

    private void Update()
    {
        wayPointPos = new Vector3(wayPoint.transform.position.x, wayPoint.transform.position.y,
            wayPoint.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
    }
}
