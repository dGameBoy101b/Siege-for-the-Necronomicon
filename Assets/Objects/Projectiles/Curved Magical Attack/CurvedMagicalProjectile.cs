using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedMagicalProjectile : MagicalProjectileBase
{
	[Header("Projectile Attributes")]
	
	[SerializeField()]
	[Tooltip("The amount of damage to do to the player when hit.")]
	[Min(0)]
	public int DAMAGE;
	
	[SerializeField()]
	[Tooltip("The number of point to add to the score when this simple physical projectile is slashed.")]
	[Min(0)]
	public int POINTS;
	
	[SerializeField()]
	[Tooltip("The global position this simple physical projectile will fly towards.")]
	public Vector3 TARGET;
	
	[SerializeField()]
	[Tooltip("The speed in units per second at which this simple physical projectile moves forward.")]
	[Min(float.Epsilon)]
	public float SPEED;

	[SerializeField()]
	[Tooltip("The height that the arc will reach.")]
	public float ARCHEIGHT = 50;

	private Vector3 STARTPOS;

	/**
	 * Rotate this simple physical projectile to face the target.
	 */
	private void faceTarget()
	{
		this.transform.rotation = Quaternion.LookRotation(this.TARGET - this.transform.position);
	}
	
	/**
	 * Move this simple physical projectile in a parabola towards target.
	 * @param t The float number of seconds since this function was called.
	 */
	private void moveCurved(float t)
	{
		float x0 = STARTPOS.x;
		float x1 = TARGET.x;
		float dist = x1 - x0;
		float nextX = Mathf.MoveTowards(transform.position.x, x1, SPEED* t);
		
		float z1 = TARGET.z;
		float nextZ = Mathf.MoveTowards(transform.position.z, z1, SPEED* t);

		float baseY = Mathf.Lerp(STARTPOS.y, TARGET.y, (nextX - x0)/(dist));
		float arc = ARCHEIGHT * (nextX -x0) * (nextX -x1) / (-0.25f * dist *dist);

		Vector3 nextPos = new Vector3(nextX, baseY + arc, nextZ);
		
		this.transform.position = nextPos;
	}
	
	
	/**
	 * Attack the player when hit.
	 */
	public override void attack()
	{
		this.PLAYER_HEALTH.TakeDamage(DAMAGE);
		base.attack();
	}
	
	/**
	 * Destroy this simple physical projectile when it hits a sword slash.
	 */
	public override void defeat()
	{
		this.PLAYER_SCORE.AddScore(this.POINTS);
		base.defeat();
	}
	
	private void Start()
	{
		this.faceTarget();
		STARTPOS = this.transform.position;
	}
	
	private void Update()
	{
		this.moveCurved(Time.deltaTime);
	}
}
