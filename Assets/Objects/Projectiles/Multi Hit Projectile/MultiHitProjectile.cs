using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 05/05/2021
 * A physical projectile that must be hit multiple times.
 */
public sealed class MultiHitProjectile : PhysicalProjectileBase
{
	[Header("Projectile Attribbutes")]
	
	[SerializeField()]
	[Tooltip("The global position this projectile will fly towards.")]
	public Vector3 TARGET;
	
	[SerializeField()]
	[Tooltip("The speed at which this projectile moves in units per second.")]
	[Min(float.Epsilon)]
	public float SPEED;
	
	[SerializeField()]
	[Tooltip("The reverse list of models to render each time this projectile is hit.")]
	public List<MeshRenderer> HIT_MODELS;
	
	[SerializeField()]
	[Tooltip("The number of hits this projectile can take before being defeated.")]
	[Min(1)]
	public int HIT_CAPACITY;
	
	[SerializeField()]
	[Tooltip("The amount of damage this projectile does to the player.")]
	[Min(0)]
	public int DAMAGE;
	
	[SerializeField()]
	[Tooltip("The number of points awarded to the player for defeating this projectile.")]
	[Min(0)]
	public int POINTS;
	
	/**
	 * The number of hit this projectile has taken so far.
	 */
	public int CurrentHits {get; private set;}
	
	/**
	 * Rotate this projectile to face its target.
	 */
	private void faceTarget()
	{
		this.transform.rotation = Quaternion.LookRotation(this.TARGET - this.transform.position);
	}
	
	/**
	 * Move this projectile forward.
	 * @param t The float number of seconds since the previous call to this function.
	 */
	private void moveForward(float t)
	{
		this.transform.position += this.transform.forward * this.SPEED * t;
	}
	
	/**
	 * Change the visible model to the one corresponding to the given index.
	 * @param index The integer index of the model to leave visible.
	 */
	private void switchModel(int index)
	{
		for (int i = 0; i < this.HIT_MODELS.Count; i++)
		{
			this.HIT_MODELS[i].enabled = i == index;
		}
	}
	
	/**
	 * Check that the hit capacity is lesser than or equal to the length of the hit model list.
	 * @throws Exception The hit capacity of this projectile is greater than the length od the hit model list.
	 */
	private void checkHitModels()
	{
		if (this.HIT_CAPACITY > this.HIT_MODELS.Count)
		{
			throw new System.Exception("The hit capacity must be lesser than or equal to the length of the model list.");
		}
	}
	
	public override void defeat()
	{
		this.CurrentHits += 1;
		if (this.CurrentHits >= this.HIT_CAPACITY)
		{
			this.PLAYER_SCORE.AddScore(this.POINTS);
			base.defeat();
		}
		else
		{
			this.switchModel(this.HIT_CAPACITY - this.CurrentHits - 1);
		}
		Debug.Break();
	}
	
	public override void attack()
	{
		this.PLAYER_HEALTH.TakeDamage(this.DAMAGE);
		base.attack();
	}
	
	private void Start()
	{
		this.checkHitModels();
		this.faceTarget();
		this.CurrentHits = 0;
		this.switchModel(this.HIT_CAPACITY - 1);
	}
	
	private void Update()
	{
		this.moveForward(Time.deltaTime);
	}
}