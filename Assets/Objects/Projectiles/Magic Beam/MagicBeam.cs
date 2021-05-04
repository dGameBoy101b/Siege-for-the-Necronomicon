using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @auther Rhys Mader 33705134
 * @date 04/05/2021
 * A magical beam which waits for some time and then immediately attacks the player for a duration.
 */
public sealed class MagicBeam : MagicalProjectileBase
{
	[SerializeField()]
	[Tooltip("The transform to manipulate for displaying the beam.")]
	public Transform MODEL;
	
	[Header("Projectile Attributes")]
	
	[SerializeField()]
	[Tooltip("The global position to aim at.")]
	public Vector3 TARGET;
	
	[SerializeField()]
	[Tooltip("The delay between spawning and attacking in seconds.")]
	[Min(0)]
	public float CHARGE_TIME;
	
	[SerializeField()]
	[Tooltip("The delay between hitting the player's gauntlet and being defeated in seconds.")]
	[Min(0)]
	public float STAY_TIME;
	
	[SerializeField()]
	[Tooltip("The number of points per second the player gains while blocking this magic beam.")]
	[Min(0)]
	public int POINTS;
	
	[SerializeField()]
	[Tooltip("The amount of damage to deal to the player on hit.")]
	[Min(0)]
	public int DAMAGE;
	
	/**
	 * A timer to track durations.
	 */
	private float timer;
	
	/**
	 * The sphere collider attached to this beam.
	 */
	private SphereCollider col;
	
	/**
	 * The layer mask used for spherecasting this beam.
	 */
	private LayerMask hit_mask;
	
	/**
	 * The global point at which this magic beam starts.
	 */
	private Vector3 start_pos;
	
	/**
	 * An enumeration used to denote the state of magic beams.
	 */
	public enum State
	{
		None,
		Charging,
		Attacking
	}
	
	/**
	 * The state this magic beam is currently in.
	 */
	public State CurrentState {get; private set;}
	
	/**
	 * Tick dow the timer by the given number of seconds.
	 * @param t The float number of seconds since this method was called.
	 */
	private void tickTimer(float t)
	{
		this.timer -= t;
		if (this.timer < 0f)
		{
			this.timer = 0;
		}
	}
	
	/**
	 * Turn this projectile to face its target.
	 */
	private void faceTarget()
	{
		Vector3 diff = this.TARGET - this.transform.position;
		if (diff.magnitude > 0f)
		{
			this.transform.rotation = Quaternion.LookRotation(diff);
		}
	}
	
	/**
	 * Calculate the layer mask of the collision layers this magic beam can hit.
	 */
	private LayerMask calcHitMask()
	{
		List<string> layers = new List<string>();
		layers.AddRange(this.ATTACK_LAYERS);
		layers.AddRange(this.DEFEAT_LAYERS);
		layers.AddRange(this.DESTROY_LAYERS);
		return LayerMask.GetMask(layers.ToArray());
	}
	
	/**
	 * Calculate the point at which this beam hits next.
	 */
	private Vector3 calcNextHit()
	{
		RaycastHit hit;
		if (Physics.SphereCast(this.start_pos + this.col.center,
			this.col.radius, this.transform.forward,
			out hit, Mathf.Infinity, this.hit_mask,
			QueryTriggerInteraction.Collide))
		{
			return hit.point + hit.normal * (this.col.radius - this.col.contactOffset - hit.collider.contactOffset);
		}
		return this.transform.position;
	}
	
	/**
	 * Move this magic beam forward until it hits something.
	 */
	private void moveToNextHit()
	{
		this.transform.position = this.calcNextHit();
		this.stretchModel();
	}
	
	/**
	 * Stretch the model of this magical beam along its local Z axis between the start point and current position.
	 */
	private void stretchModel()
	{
		this.MODEL.position = (this.transform.position + this.start_pos) / 2f;
		this.MODEL.localScale = new Vector3(1f, 1f, Vector3.Distance(this.transform.position, this.start_pos) + 1f);
		this.MODEL.rotation = Quaternion.LookRotation(this.transform.position - this.start_pos);
	}
	
	/**
	 * Determine if this magic beam should start attacking.
	 * @return True if this magic beam should start attacking, false otherwise.
	 */
	private bool shouldStartAttack()
	{
		return this.CurrentState == State.Charging
			&& this.timer <= 0f;
	}
	
	/**
	 * Determine if this magic beam should stop attacking.
	 * @return True if this magic beam should stop attacking, false otherwise.
	 */
	private bool shouldStopAttack()
	{
		return this.CurrentState == State.Attacking
			&& this.timer <= 0f;
	}
	
	/**
	 * Start the attack phase of this magic beam.
	 */
	public void startAttack()
	{
		this.CurrentState = State.Attacking;
		this.timer = this.STAY_TIME;
	}
	
	/**
	 * Stop the attack phase of this magic beam.
	 */
	public void stopAttack()
	{
		this.CurrentState = State.None;
		this.defeat();
	}
	
	/**
	 * Set this magical beam up.
	 */
	private void setup()
	{
		this.col = this.GetComponent<SphereCollider>();
		this.hit_mask = this.calcHitMask();
		this.start_pos = this.transform.position;
		this.faceTarget();
	}
	
	public override void defeat()
	{
		if (this.CurrentState != State.Attacking)
		{
			this.PLAYER_SCORE.AddScore(this.POINTS);
			base.defeat();
		}
	}
	
	public override void attack()
	{
		this.PLAYER_HEALTH.TakeDamage(this.DAMAGE);
		base.attack();
	}
	
	private void Update()
	{
		this.tickTimer(Time.deltaTime);
		if (this.shouldStartAttack())
		{
			this.startAttack();
		}
		if (this.shouldStopAttack())
		{
			this.stopAttack();
		}
		if (this.CurrentState == State.Attacking)
		{
			this.moveToNextHit();
		}
	}
	
	private void Start()
	{
		this.setup();
		this.timer = this.CHARGE_TIME;
		this.CurrentState = State.Charging;
	}
	
	private void OnDrawGizmosSelected()
	{
		if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
		{
			this.setup();
		}
		Gizmos.color = Color.white;
		Gizmos.DrawLine(this.transform.position, this.TARGET);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(this.calcNextHit(), this.col.radius);
	}
}
