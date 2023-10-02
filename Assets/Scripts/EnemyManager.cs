using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;

    private int enemiesCount;
    public int maxEnemiesCount;
    public Vector2 minPositon;
    public Vector2 maxPosition;

    private void Start()
    {

    }

    public void LoadEnemies()
    {
        //load enemies data from json 
        //set enemiescount to loaded count
        //if count < max count add new enemies by InvokeEnemies
    }

    public void SaveEnemies()
    {
        //save enemies position end hp
    }

    public void InvokeEnemies(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Vector2 enemyPositon = new Vector2(Random.Range(minPositon.x, maxPosition.x), Random.Range(minPositon.y, maxPosition.y));
            Instantiate(enemies[Random.Range(0, enemies.Length)], enemyPositon, Quaternion.identity, transform);
        }
    }
}
