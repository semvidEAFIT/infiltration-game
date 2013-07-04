using UnityEngine;
using System.Collections;

public abstract class ThrowableExplosive : IItem {
	
	public float AOERadius;
	public float damage;
	public float time;
	
	
	public ThrowableExplosive(float areaOfEffectRadius, float grenadeDamage, float secondsUntilExplosion){
		this.AOERadius = areaOfEffectRadius;
		this.damage = grenadeDamage;
		this.time = secondsUntilExplosion;
	}
	
	public abstract void Activate();
}
