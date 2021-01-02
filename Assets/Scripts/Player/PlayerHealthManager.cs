﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PlayerHealthManager : MonoBehaviour,IDamageable
{
	
	Health playerHealth;

	[SerializeField] private Volume globalVolume;

	private Vignette profileVignette;
	private ChromaticAberration profileChromaticAberration;
	
	
	// Start is called before the first frame update
	void  Awake()
	{
		playerHealth = new Health(100);
		globalVolume.profile.TryGet(out profileVignette);
		globalVolume.profile.TryGet(out profileChromaticAberration);
		
	}


	private void HealHealth(int healAmount)
	{

		playerHealth.currenthealth =
			Mathf.Clamp(playerHealth.currenthealth + healAmount, 0, playerHealth.maxHealth);
		
		if(playerHealth.currenthealth >= 25) HighHPEffects();
		
		
		
	}

	public float GetPlayerHP => playerHealth.currenthealth;
	
	
	public void TakeDamage(int damage)
	{
		Debug.Log(damage);
		playerHealth.currenthealth -= damage;
		
		if(playerHealth.currenthealth <= 25) LowHPEffects();
	}

	private void LowHPEffects()
	{
		
		profileVignette.intensity.value = 0.5f;
		profileChromaticAberration.active = true;
	}

	private void HighHPEffects()
	{
		profileVignette.intensity.value = 0.25f;

		profileChromaticAberration.active = false;
	}
}
