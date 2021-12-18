using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // References
    public Transform player;
    public Transform Enemytransform;
    public Enemy enemy;
    public Animator animator;
    public Rigidbody rb;
    public EnemyHealth health;

    // Variables
    public bool CD = false;
    private IEnumerator Coroutine;
    private float dist;
    public float moveSpeed = 140;
    public float howclose;
    public float meleeRange;
    public float damage;
    public int hitRange = 10;


    // Start is called before the first frame update
    public void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health.alive)
        {
            dist = Vector3.Distance(player.position, transform.position);

            if (dist <= howclose)
            {
                rb.isKinematic = false;
                animator.SetBool("idle", false);
                animator.SetBool("BattleIdle", true);
                animator.SetBool("Flying", true);
                rb.constraints = RigidbodyConstraints.None;
                rb.isKinematic = false;
                transform.LookAt(player);
                if (meleeRange < dist)
                {
                    GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
                }

            }
            
            if (dist >= howclose)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                animator.SetBool("Flying", false);
                animator.SetBool("BattleIdle", false);
            }

            if (dist <= 5 && CD == false)
            {
                // Do damage when close to player
                GetComponent<Animator>().SetTrigger("Attack1");
                Attack();
                CD = true;
                Coroutine = CoolDown();
                StartCoroutine(Coroutine);
            }
        }      
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(3);
        CD = false;
    }

    void Attack()
    {
        RaycastHit hit;
        Vector3 playertarget = player.position - transform.position;
        Vector3 origin = transform.position;

        if (Physics.Raycast(origin, playertarget, out hit, hitRange))
        {
            Debug.DrawRay(origin, playertarget, Color.blue, 100);
            if (hit.transform.gameObject.tag == "Player")
            {

                hit.transform.gameObject.SendMessage("TakeDamage", 10);
                Coroutine = CoolDown();
                StartCoroutine(Coroutine);

            }
        }
    }
}