using UnityEngine;

public class EnemyRangeCollision : MonoBehaviour
{
    [SerializeField] private Enemy enemyScript;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyScript.ChangingStateTo(Enemy.State.Attack);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyScript.ChangingStateTo(Enemy.State.Patrol);
        } 
    }

   
}
