using UnityEngine;
using System.Collections;

public abstract class Explosive : Item {
	
	public float AOERadius;
	public float damage;
	public LayerMask layerAffected;
	
	public AudioClip[] useSounds;
	public AudioClip[] activateSounds;

	protected abstract void Explode();
}
