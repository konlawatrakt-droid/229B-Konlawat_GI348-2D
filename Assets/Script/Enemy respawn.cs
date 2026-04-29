using UnityEngine;
using System.Collections.Generic;

public class EnemyRespawner : MonoBehaviour
{
    public List<GameObject> enemiesInArea;

    public void RespawnAllEnemies()
    {
        foreach (GameObject enemy in enemiesInArea)
        {
            if (enemy != null)
            {
                EnemyHP hp = enemy.GetComponent<EnemyHP>();
                if (hp != null)
                {
                    hp.CheckAndRespawn();
                }
            }
        }
    }
}