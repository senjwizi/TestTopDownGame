using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public List<GameObject> enemiesOnScene;

    public int enemiesCount;
    public int maxEnemiesCount;
    public Vector2 minPositon;
    public Vector2 maxPosition;

    public void Init(Data.GameData data)
    {
        enemiesCount = data.enemiesAmount;
        if (enemiesCount > 0)
            for(int i = 0; i < enemiesCount; i++)
                LoadEnemy(data.enemiesPosition[i], data.enemiesHealth[i]);

        InvokeEnemies(maxEnemiesCount - enemiesCount);
    }

    private void GetArrayData(ref Vector2[] outPositions, ref float[] outHealth)
    {
        outPositions = new Vector2[enemiesCount];
        outHealth = new float[enemiesCount];
        Debug.Log(outPositions.Length + "\t" + outHealth.Length);
        for(int i = 0; i < enemiesOnScene.Count; i++)
        {
            outHealth[i] = enemiesOnScene[i].GetComponent<Enemy>().health;
            outPositions[i] = enemiesOnScene[i].transform.position;
        }
    }

    public void RemoveFromList(GameObject enemy)
    {
        enemiesOnScene.Remove(enemy);
    }

    public void GetData(ref Data.GameData data)
    {
        data.enemiesAmount = enemiesOnScene.Count;
        GetArrayData(ref data.enemiesPosition, ref data.enemiesHealth);
    }

    public void LoadEnemy(Vector2 position, float helth)
    {
        GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], position, Quaternion.identity, transform);
        newEnemy.GetComponent<Enemy>().health = helth;
        newEnemy.GetComponent<Enemy>().enemyManager = this;
        enemiesOnScene.Add(newEnemy);
    }

    public void InvokeEnemies(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Vector2 enemyPositon = new Vector2(Random.Range(minPositon.x, maxPosition.x), Random.Range(minPositon.y, maxPosition.y));
            GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], enemyPositon, Quaternion.identity, transform);
            newEnemy.GetComponent<Enemy>().enemyManager = this;
            newEnemy.GetComponent<Enemy>().health = newEnemy.GetComponent<Enemy>().maxHealth;
            enemiesOnScene.Add(newEnemy);
        }
        enemiesCount += count;
    }
}
