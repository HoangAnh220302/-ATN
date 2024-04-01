using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//public class Bullet :
//{
//    protected Character attacker;
//    protected Action<Character attacker, Character victim> onHit;
//    // set bullet data for bullet
//    public virtual void OnInit(Character attacker, Action<Character attacker, Character victim> onHit/*Truyen vao mot Action*/)
//    {
//        this.attacker = attacker;
//        this.onHit = onHit;
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag(Constant.TAG_CHARACTER))
//        {
//            Character victim = Cache.GetCharacter(other);
//            onHit?.Invoke(attacker, victim);
//        }
//    }
//}
//public class Weapon :
//{
//    public void Throw(Character character/*xac dinh nguoi ban la ai*/, Action<Character attacker, Character victim> onHit)
//    {
//        Bullet bullet = LeanPool.Spawn(bullet);
//        bullet.OnInit(character, onHit);
//    }
//}

//public class Character :
//{
//    public void Throw()
//    {
//        currentSkin.Weapon.Throw(this, OnHitVictim/*hanh dong khi va cham*/);
//    }
//    // Logic when bullet hit victim
//    protected virtual OnHitVictim(Character attacker, Character victim)
//    {
//        victim.DoDead();
//        //Do something
//    }
//}
