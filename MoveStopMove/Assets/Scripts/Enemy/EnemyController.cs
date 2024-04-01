using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    public string currentState;
    public NavMeshAgent agent;
    public StateMachine stateMachine;
    public bool canRespawn;
    public NavMeshAgent Agent { get => agent; }

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject target;
    [SerializeField] private List<Transform> locationList = new List<Transform>();
    [SerializeField] private float speed = 1f;

    private void Awake()
    {
        canRespawn = false;
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialize();
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
    private void Update()
    {
        coolDown += Time.deltaTime;
        currentState = stateMachine.activeState.ToString();
    }
    public override void Move()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFireRange"))
        {
            //Debug.Log("In Player Fire Range");
            other.transform.parent.gameObject.GetComponent<PlayerController>().targetList.Add(gameObject);
            gameObject.transform.rotation = Quaternion.Euler(transform.position - other.gameObject.transform.position);
        }
        else
        {
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFireRange"))
        {
            //Debug.Log("Exit Player Fire Range");
            other.transform.parent.gameObject.GetComponent<PlayerController>().targetList.Remove(gameObject);
        }
        else
        {
            return;
        }
    }

    public void Launch()
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            shootDirection = targetList[i].transform.position - transform.position;
            //if (coolDown >= 5 && targetList.Count >= 1)
            //{
            //    Vector3 shootDirection = targetList[i].transform.position - transform.position;
            //    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(shootDirection.x, shootDirection.y, shootDirection.z));
            //    Rigidbody rb = bullet.GetComponent<Rigidbody>();
            //    rb.AddForce(shootDirection * speed, ForceMode.VelocityChange);
            //    coolDown = 0;
            //}
        }
        Throw();
        animator.SetBool("IsIdle", true);
        animator.SetBool("IsAttack", false);
    }
}
