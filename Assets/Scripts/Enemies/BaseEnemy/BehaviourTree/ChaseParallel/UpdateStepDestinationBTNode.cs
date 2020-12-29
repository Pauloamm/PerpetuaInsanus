﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(FieldOfViewDetection))]
public class UpdateStepDestinationBTNode : BTNode
{
    private bool isAlreadySubscribed;
    private Movement enemyMovement;
    [SerializeField] private PlayerMovement playerMovement;


    private FieldOfViewDetection fovDetection;

    protected override void Awake()
    {
        behaviourTree = GetComponent<BehaviourTree>();
        enemyMovement = GetComponent<Movement>();
        fovDetection = GetComponent<FieldOfViewDetection>();

    }
    
    
    public override void OnInterrupt()
    {
        playerMovement.Stepped -= UpdateDestination;
        isAlreadySubscribed = false;
    }

    public override Result Execute()
    {
        
        if(fovDetection.isPlayerDetected &&
           !isAlreadySubscribed)
        {
            playerMovement.Stepped += UpdateDestination;
            isAlreadySubscribed = true;
        }
        
        else if (!fovDetection.isPlayerDetected &&
                 isAlreadySubscribed)
        {
            playerMovement.Stepped -= UpdateDestination;
            isAlreadySubscribed = false;
        }

        return Result.Success;

    }

    private void UpdateDestination()
    {
       enemyMovement.UpdatePath(playerMovement.lastStepPosition);
    }
    
    
}
