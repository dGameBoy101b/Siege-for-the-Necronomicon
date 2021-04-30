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
	[Tooltip("The maximum trajectory height of this projectile in units.")]
	[Min(float.Epsilon)]
	public float HEIGHT;
	
	[Header("Debug")]
	
	[SerializeField()]
	[Tooltip("Whehter the debug trajectory should be shown.")]
	public bool SHOW_TRAJECTORY;

	/**
	 * The global position this curved magical projectile started.
	 */
	private Vector3 start_pos;
	
	/**
	 * The global up vector this curved magical projectile started with.
	 */
	private Vector3 start_up;
	
	/**
	 * The total simulated time elapsed since this curved magical projectile spawned.
	 */
	private float timer = 0f;
	
	/**
	 * Calculate the trajectory height.
	 * @param The float progress through the trajectory from 0 to 1.
	 * @return The global height displacement vector.
	 */
	private Vector3 calcHeight(float t)
	{
		Vector3 h = (float)System.Math.Sin((float)System.Math.PI * t) * this.HEIGHT * this.start_up;
		return h;
	}
	
	/**
	 * Calculate the trajectory length.
	 * @param The float progress through the trajectory from 0 to 1.
	 * @return The global length displacement vector.
	 */
	private Vector3 calcLength(float t)
	{
		Vector3 l = (this.TARGET - this.start_pos) * t;
		return l;
	}
	
	/**
	 * Calculate the distance travelled.
	 * @param t The float progress through the trajectory from 0 to 1.
	 * @return The float distance of the trajectory followed by this magical curved projectile in units.
	 */
	private float calcDistance(float t)
	{
		float l = Vector3.Distance(this.start_pos, this.TARGET);
		float d = l * t;
		return d;
	}
	
	/**
	 * Calculate the progress through the trajectory.
	 * @param t The total time elaspsed in seconds.
	 * @return A float progress through the trajectory from 0 to 1.
	 */
	private float calcProgress(float t)
	{
		float p = t * this.SPEED / this.calcDistance(1);
		return p;
	}
	
	/**
	 * Calculate the global position on the trajectory.
	 * @param The float progress through the trajectory from 0 to 1.
	 * @return The global position on the trajectory.
	 */
	private Vector3 calcPos(float t)
	{
		Vector3 p = this.calcLength(t) + this.calcHeight(t) + this.start_pos;
		return p;
	}
	
	/**
	 * Calculate the global rotation on the trajectory.
	 * @param The float progress through the trajectory from 0 to 1.
	 * @return The global rotation on the trajectory.
	 */
	private Quaternion calcRot(float t)
	{
		Vector3 d = this.TARGET - this.start_pos;
		float angle = 180f * (float)System.Math.Atan(this.calcGradient(t)) / (float)System.Math.PI;
		Debug.Log("angle = " + angle.ToString());
		Quaternion r = Quaternion.AngleAxis(angle, Vector3.Cross(d, this.start_up).normalized) * Quaternion.LookRotation(d, this.start_up);
		return r;
	}
	
	/**
	 * Calculate the gradient of the trajectory at the given progress point.
	 * @param The float progress through the trajectory from 0 to 1.
	 * @return The gradient of the trajectory.
	 */
	private float calcGradient(float t)
	{
		float g = (float)System.Math.PI * this.HEIGHT * (float)System.Math.Cos(System.Math.PI * t);
		return g;
	}
	
	/**
	 * Move this curved magical projectile in a parabola towards target.
	 * @param t The float number of seconds since this function was called.
	 */
	private void moveCurved(float t)
	{
		this.timer += t;
		float prog = this.calcProgress(this.timer);
		this.transform.position = this.calcPos(prog);
		this.transform.rotation = this.calcRot(prog);
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
	 * Destroy this curved magical projectile when it hits a sword slash.
	 */
	public override void defeat()
	{
		this.PLAYER_SCORE.AddScore(this.POINTS);
		base.defeat();
	}
	
	/**
	 * Setup the private variables of this projectile.
	 */
	private void setup()
	{
		this.start_pos = this.transform.position;
		this.start_up = Vector3.Cross(this.TARGET - this.start_pos, this.transform.right).normalized;
		this.timer = 0f;
	}
	
	/**
	 * Draw the trajectory of this projectile in the editor.
	 */
	private void drawTrajectoryGizmo()
	{
		float step = System.Math.Min(System.Math.Max(this.calcProgress(this.SPEED * Time.fixedDeltaTime), Time.fixedDeltaTime), .2f);
		for (float i = this.calcProgress(this.timer); i < 1f; i += step)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(this.calcPos(i), this.calcPos(i + step));
			Gizmos.color = Color.green;
			Gizmos.DrawRay(this.start_pos + this.calcLength(i), this.calcHeight(i));
			Gizmos.color = Color.blue;
			Gizmos.DrawRay(this.calcPos(i), this.calcRot(i) * Vector3.forward);
		}
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(this.start_pos, this.TARGET);
		Gizmos.color = Color.magenta;
		Gizmos.DrawRay(Vector3.Lerp(this.start_pos, this.TARGET, .5f), this.start_up * this.HEIGHT);
	}
	
	private void Start()
	{
		this.setup();
	}
	
	private void OnDrawGizmosSelected()
	{
		if (this.SHOW_TRAJECTORY)
		{
			if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
			{
				this.setup();
			}
			this.drawTrajectoryGizmo();
		}
	}
	
	private void Update()
	{
		this.moveCurved(Time.deltaTime);
	}
}
