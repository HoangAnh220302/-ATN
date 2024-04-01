using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bulletprefab;

    [SerializeField] private float speed = 10f;
    public void Throw(Character character/*xac dinh nguoi ban la ai*/, Action<Character, Character> onHit,Vector3 shootDirection)
    {
        Bullet bullet = LeanPool.Spawn(bulletprefab,transform.position,Quaternion.Euler(character.shootDirection.x, character.shootDirection.y, character.shootDirection.z));
        bullet.OnInit(character, onHit);
        for (int i = 0; i < character.targetList.Count; i++)
        {
            if (character.coolDown >= 5 && character.targetList.Count >= 1)
            {
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(shootDirection * speed, ForceMode.VelocityChange);
                character.coolDown = 0;
            }
        }
    }
}
