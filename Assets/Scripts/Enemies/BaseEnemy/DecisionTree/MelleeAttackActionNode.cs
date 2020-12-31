﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Enemy))]
public class MelleeAttackActionNode : ActionNode
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip meleeAttackAnimation;
    [SerializeField] private float impactMomentOfAnimation;
    [SerializeField] private LayerMask enemyMask;

    private bool attackFinished;

    private void Awake()
    {
        anim = this.GetComponent<Animation>();
        enemyScript = GetComponent<Enemy>();
        attackFinished = false;
    }

    protected override void ExecuteAction()
    {

        if (attackFinished)
        {
            attackFinished = false;
            return;
        }

        // StartCoroutine(MeleeAttack());
        TEST();
        
        
        // MELEE ATTACK LOGIC HERE
         Debug.Log("MELLEE ATTACK");
    }
    
    private void TEST()
    {

        RaycastHit hit;

        Vector3 rayDirection = playerTransform.position - transform.position;
        
        if (Physics.Raycast 
            (transform.position,rayDirection,out hit,enemyScript.MeleeAttackRange))
        {
           
            
            GameObject objectHit = hit.collider.gameObject;
            IDamageable temp = objectHit.GetComponent<IDamageable>();

            if(temp != null) Debug.Log("HIT PLAYER");
            temp?.TakeDamage(enemyScript.MeleeAttackDamage);
            
            
        }

        attackFinished = true;

    }
   

   

}
