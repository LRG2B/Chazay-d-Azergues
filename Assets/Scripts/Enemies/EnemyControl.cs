using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public Animator anim_enemy; // Animator de l'ennemi
    public Animator anim_player; // Animator du joueur
    public float moveSpeed; // Vitesse de déplacement de l'ennemi
    public Transform attackPoint; // Point de détection
    public Transform player; // Position du joueur
    public float agroRange; // Rayon d'agro de l'ennemi
    public float attackRange; // Rayon d'attaque de l'ennemi
    public LayerMask playerLayer; // Layer du joueur
    public int damage; // Damage de l'ennemi
    public int maxHealth; // PV max de l'ennemi
    public Slider slider_enemy; //Slider de PV de l'ennemi

    private GameObject self;
    private int currentHealth; // PV temps réel de l'ennemi
    private bool LastCheck;
    private bool BoolIsAttacking;


    private Rigidbody2D rgbd;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        LastCheck = false;
        BoolIsAttacking = false;
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange && distToPlayer > 1.5)
        {
            ChasePlayer();
            anim_enemy.SetBool("IsAttacking", false);
            gameObject.SetActive(slider_enemy);
        }
        else if (distToPlayer < 1.5 && anim_enemy.GetBool("IsHit")==false && anim_player.GetBool("IsDead")==false)
        {
            StopChasePlayer();
            anim_enemy.SetBool("IsAttacking", true);
        }
        else if (anim_player.GetBool("IsDead") == true)
        {
            anim_enemy.SetBool("IsAttacking", false);
        }
        else
        {
            StopChasePlayer();
            LastCheck = false;
            anim_enemy.SetBool("IsAttacking", false);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rgbd.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            rgbd.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        anim_enemy.SetFloat("speed", moveSpeed);
    }
    void AttackPlayer()
    {
        anim_enemy.SetTrigger("Attack");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(damage);
        }
        BoolIsAttacking = false;
    }
    void StopChasePlayer()
    {
        rgbd.velocity = Vector2.zero;
        anim_enemy.SetFloat("speed", 0);
    }
    void SetBoolLastCheck()
    {
        LastCheck = false;
    }

    void SetBoolIsAttacking()
    {
        BoolIsAttacking = true;
        if (anim_player.GetBool("IsDead") == false)
            AttackPlayer();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim_enemy.SetTrigger("IsHit");
        slider_enemy.value -= damage;
        BoolIsAttacking = false;
    }
    void Die()
    {
        Debug.Log("Enemy died!");
        anim_enemy.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        rgbd.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(DestroyEnemy());
        this.enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(5);
        Destroy(self);
        Debug.Log("Enemy destroyed");
    }
}