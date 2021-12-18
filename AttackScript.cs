using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [Header("Damage related")]
    public float damage = 25;
    public int hitRange = 10;
    [Header("gameobjects")]
    public Animator anim;
    public bool CD = false;
    private IEnumerator Coroutine;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CD == false)
        {
            CD = true;
            Coroutine = AttackDelay();
            StartCoroutine(Coroutine);
            anim.SetBool("attack", true);
        }
        

    }
    void Attack()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.left);
        Vector3 origin = transform.position;

        if (Physics.Raycast(origin, forward, out hit, hitRange))
        {

            Debug.DrawRay(transform.position, forward, Color.red, 100);
            if (hit.transform.gameObject.tag == "Enemy")
            {

                hit.transform.gameObject.SendMessage("TakeDamage", damage);
            }
        }
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.2f);
        Attack();
        anim.SetBool("attack", false);
        CD = false;
    }
}
