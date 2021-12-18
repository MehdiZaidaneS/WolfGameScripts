using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    public float maxhealth = 100;

    private IEnumerator Coroutine;
    public GameObject healthBarUI;
    public Slider slider;
    public Animator animator;
    public bool alive;

    void Start()
    {
        alive = true;
        health = 100;
        slider.maxValue = maxhealth;
    }

    void Update()
    {
        slider.value = health;

        if (health < 1)
        {
            alive = false;
            
            // add your death things here
            animator.SetBool("Attack1", false);
            animator.SetBool("Flying", false);
            animator.SetBool("BattleIdle", false);
            animator.SetBool("idle", false);
            animator.SetBool("Die", true);
            death();

        }
    }

    void TakeDamage(int damageAmount)
    {
        health = health - damageAmount;
        slider.value = health;
    }

    void death()
    {
        
        StartCoroutine(DestroyOnDeath());
    }

    private IEnumerator DestroyOnDeath()
    {
        yield return new WaitForSeconds(10);
        
        Destroy(gameObject);
    }
}
