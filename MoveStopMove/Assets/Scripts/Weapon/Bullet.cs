using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Character attacker;
    protected Action<Character, Character> onHit;

    private Coroutine delayCoroutine;
    private void Awake()
    {
        if(delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
        }
        delayCoroutine = StartCoroutine(DestroyBullet());
    }

    //set bullet data for bullet
    public virtual void OnInit(Character attacker, Action<Character, Character> onHit/*Truyen vao mot Action*/)
    {
        this.attacker = attacker;
        this.onHit = onHit;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Character victim = Cache.GetCharacter(other);
            onHit?.Invoke(attacker, victim);
            
        }
        if (other.CompareTag("Player"))
        {
            Character victim = Cache.GetCharacter(other);
            onHit?.Invoke(attacker, victim);
        }
    }
    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    /*
    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            //attacker.GetComponent<EnemyController>().kills++;
            collider.gameObject.GetComponent<EnemyController>().isDead = true;

        }
        if (collider.gameObject.CompareTag("Player"))
        {
            //attacker.GetComponent<PlayerController>().kills++;
            collider.gameObject.GetComponent<PlayerController>().isDead = true;
        }
    }
    */
}
