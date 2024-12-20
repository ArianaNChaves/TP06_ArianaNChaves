using UnityEngine;

public class EnemyRangeCollision : MonoBehaviour
{
    [SerializeField] private Enemy enemyScript;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyScript.CurrentState != Enemy.State.Attack)
        {
            enemyScript.ChangingStateTo(Enemy.State.Attack);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyScript.CurrentState != Enemy.State.Patrol)
        {
            enemyScript.ChangingStateTo(Enemy.State.Patrol);
        } 
    }
   
}
