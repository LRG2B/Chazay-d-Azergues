using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator; // Animator du joueur

    public Transform attackPoint; // Point de détection
    public LayerMask enemyLayers; // Layer des ennemis
    public float attackRange = 0.5f; // Rayon d'attaque 
    public int attackDamage = 20; // Damage du joueur

    public Slider slider; // Slider de vie du joueur
    public int maxHealth; // PV max du joueur
    private int currentHealth; // PV temps réel du joueur

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("mouse 0") && animator.GetBool("HaveSword") == true)
        {
            Attack();
        }

        if (currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
            currentHealth = 0;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyControl>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("IsHit");
        slider.value -= damage;
    }

    public void GetHealth(int gethealth)
    {
        currentHealth += gethealth;
        slider.value += gethealth;
    }
}
