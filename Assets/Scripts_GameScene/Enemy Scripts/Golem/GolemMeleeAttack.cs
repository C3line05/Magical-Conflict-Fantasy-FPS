using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMeleeAttack : MonoBehaviour
{
    private CharacterActions playerStats;
    public GameObject player;
    public GameObject hitZone;
    public Animator animator;
    public GameObject coin;

    public float attackDistance = 2f;
    public int damageAmount = 5; // <--- Danno configurabile da Inspector
    private bool isAttacking = false;

    void Start()
    {
        // Trova il giocatore se non assegnato manualmente
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            playerStats = player.GetComponent<CharacterActions>();
        else
            Debug.LogWarning("GolemMeleeAttack: Nessun player trovato nella scena!");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < attackDistance && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (animator != null)
            animator.SetTrigger("Attack");

        // Ritardo prima del colpo effettivo
        yield return new WaitForSeconds(0.4f);

        // Attiva la zona di danno e controlla subito se colpisce il player
        hitZone.SetActive(true);
        TryDamagePlayer();

        // Disattiva la hitZone dopo breve tempo
        yield return new WaitForSeconds(0.5f);
        hitZone.SetActive(false);

        // Attesa prima del prossimo attacco
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }

    void TryDamagePlayer()
    {
        if (player == null || playerStats == null) return;

        // Controlla la distanza al momento del colpo
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= attackDistance + 0.5f) // piccolo margine
        {
            playerStats.TakeDamage(damageAmount);
            Debug.Log($"Golem ha colpito il giocatore! Danno: {damageAmount}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDamage"))
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}


