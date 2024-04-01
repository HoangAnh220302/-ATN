using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float coolDown;
    public Animator animator;
    public List<GameObject> targetList = new List<GameObject>();
    public bool isDead;
    public float kills;
    public GameObject weapon;
    public Vector3 shootDirection;
    private void Awake()
    {
        kills = 0;
        animator = GetComponent<Animator>();
    }
    public void FixedUpdate()
    {
        Move();
        Shoot();
    }
    public virtual void Move()
    {

    }
    public virtual void Shoot()
    {

    }
    public void Eliminated()
    {
        gameObject.SetActive(false);
    }
    public void DeadAnimDelay()
    {
        animator.SetBool("IsDead", true);
        Invoke("Eliminated", 2);
    }
    public void Grow()
    {
        transform.localScale = new Vector3(transform.localScale.x * kills, transform.localScale.y * kills, transform.localScale.z * kills);
    }
    
    public void Throw()
    {
        weapon.GetComponent<Weapon>().Throw(this, OnHitVictim/*hanh dong khi va cham*/,shootDirection);
    }
    // Logic when bullet hit victim
    protected virtual void OnHitVictim(Character attacker, Character victim)
    {
        if(victim == attacker)
        {
            return;
        }
        else
        {
            DeadAnimDelay();
            victim.isDead = true;
            Debug.Log(victim.isDead);
            attacker.kills++;
            attacker.Grow();
        }
    }

}
