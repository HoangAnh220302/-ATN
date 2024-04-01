using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : Character
{
    public VariableJoystick joystick;
    public CharacterController characterController;
    public Canvas inputCanvas;
    public bool isJoystick;
    public float movementSpeed;
    public float rotationSpeed;
    //public ObjectTag objectTag;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject target;
    [SerializeField] private float speed = 1f;

    private bool canShoot;

    public void Start()
    {
        coolDown = 0;
        EnableJoystickInput();
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Die();
    }
    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);

    }
    public override void Move()
    {
        if (joystick)
        {
            Vector3 moveDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            characterController.SimpleMove(moveDirection * movementSpeed);
            if (joystick.Horizontal != 0 && joystick.Vertical != 0)
            {
                canShoot = false;
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsRun", true);
                Vector3 targetDirection = Vector3.RotateTowards(characterController.transform.forward, moveDirection, rotationSpeed * Time.deltaTime, 0.0f);
                characterController.transform.rotation = Quaternion.LookRotation(targetDirection);
            }
            else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
            {
                canShoot = true;
                animator.SetBool("IsIdle", true);
                animator.SetBool("IsRun", false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyFireRange"))
        {
            //Debug.Log("In Enemy Fire Range");
            other.transform.parent.gameObject.GetComponent<EnemyController>().targetList.Add(gameObject);
        }
        else
        {
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyFireRange"))
        {
            //Debug.Log("Exit Enemy Fire Range");
            other.transform.parent.gameObject.GetComponent<EnemyController>().targetList.Remove(gameObject);
        }
        else
        {
            return;
        }
    }
    public override void Shoot()
    {
        coolDown += Time.deltaTime;
        if (canShoot)
        {
            for(int i = 0; i < targetList.Count; i++)
            {
                if (coolDown >= 5 && targetList.Count >=1)
                {
                    animator.SetBool("IsIdle", false);
                    animator.SetBool("IsAttack",true);
                    
                }
            }
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
    public void Die()
    {
        if(isDead)
        {
            DeadAnimDelay();
        }
    }
}
