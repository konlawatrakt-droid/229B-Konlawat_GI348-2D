using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 10f;
    [Range(0, 360)] public float viewAngle = 90f;

    public LayerMask targetMask;   // เลือก Layer "Player"
    public LayerMask obstacleMask; // เลือก Layer "Obstacle" หรือ "Default"

    [HideInInspector] public Transform visibleTarget;

    void Start()
    {
        // เริ่มการค้นหาเป้าหมายเป็นรอบๆ (เพื่อประหยัดทรัพยากร)
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTarget = null;
        // ค้นหาวัตถุในระยะวงกลม
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            // เช็คว่าอยู่ในองศาที่กำหนดไหม
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                // ยิง Raycast เพื่อเช็คว่ามีสิ่งกีดขวางบังไหม
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTarget = target; // เจอตัวแล้ว!
                }
            }
        }
    }

    // ฟังก์ชันช่วยวาดเส้นในหน้า Scene
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) { angleInDegrees += transform.eulerAngles.y; }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}