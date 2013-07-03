using UnityEngine;
using System.Collections;

public class EquippedGrenade : ThrowableExplosive {
	
	public EquippedGrenade(float areaOfEffectRadius, float grenadeDamage, float secondsUntilExplosion) : base(areaOfEffectRadius, grenadeDamage, secondsUntilExplosion){
	
	}
	
	public override void Activate(){
		
	}	
}
