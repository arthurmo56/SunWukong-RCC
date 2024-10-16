using System.Collections;
using UnityEngine;
using Pathfinding;  // Certifique-se de adicionar isso para acessar o AstarPathfindingProject

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    public float stopTimeAfterHit = 2f;  // Tempo que o inimigo ficará parado após receber dano
    private AIPath aiPath;  // Componente responsável pela movimentação A* (AstarPathfindingProject)

    void Start()
    {
        currentHealth = maxHealth;
        aiPath = GetComponent<AIPath>();  // Obtém o componente de movimentação A*
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;  // Se o inimigo já estiver morto, não faça nada

        currentHealth -= damage;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(StopMovementTemporarily());  // Para a movimentação após o hit
        }
    }

    void Die()
    {
        isDead = true;  // Define o estado como morto
        Debug.Log("Enemy died!");

        // Play death animation
        animator.SetBool("isDead", true);

        // Disable enemy movement and interaction
        GetComponent<Collider2D>().enabled = false;  // Desabilita o colisor
        aiPath.canMove = false;  // Impede a movimentação do inimigo após a morte
        this.enabled = false;  // Desabilita este script para evitar qualquer ação adicional

        StartCoroutine(DisableAfterDeath());
    }

    IEnumerator StopMovementTemporarily()
    {
        aiPath.canMove = false;  // Impede a movimentação temporariamente
        yield return new WaitForSeconds(stopTimeAfterHit);  // Espera o tempo definido
        if (!isDead)  // Apenas reativa se o inimigo ainda não estiver morto
        {
            aiPath.canMove = true;  // Reativa a movimentação
        }
    }

    // Coroutine para aguardar o fim da animação + timer de 1 segundo antes de desabilitar completamente
    IEnumerator DisableAfterDeath()
    {
        // Aguarda o fim da animação de morte
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Adiciona o timer de 1 segundo após a animação de morte
        yield return new WaitForSeconds(1f);

        // Desabilita o GameObject do inimigo completamente
        gameObject.SetActive(false);  // Ou use Destroy(gameObject) para remover completamente
    }
}
