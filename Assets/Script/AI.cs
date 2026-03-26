using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float speed = 3f;
    public float waitTime = 1f;

    private Transform target;

    void Start()
    {
        target = pointA;
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            // เดินไป target
            while (Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target.position,
                    speed * Time.deltaTime
                );
                yield return null;
            }

            // หยุด
            yield return new WaitForSeconds(waitTime);

            // สลับเป้าหมาย
            target = (target == pointA) ? pointB : pointA;
        }
    }
}