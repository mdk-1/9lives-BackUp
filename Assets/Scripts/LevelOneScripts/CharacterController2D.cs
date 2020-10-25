using UnityEngine;
using UnityEngine.Events;

//script to handle character prefab movement and physics 

public class CharacterController2D : MonoBehaviour
{
	[Header("Player Control Variables")]
	[SerializeField, Tooltip("Amount of force added when the player jumps")] 
	private float jumpForce = 400f;
	[Range(0, 1)]
	[SerializeField, Tooltip("Amount of maxSpeed applied to crouching movement. 1 = 100%")]
	private float crouchSpeed = .36f;
	[Range(0, .3f)] [SerializeField, Tooltip ("How much to smooth out the movement")] 
	private float movementSmooth = .05f;
	[SerializeField, Tooltip ("Whether or not a player can steer while jumping")] 
	private bool airControl = true;                         
	[SerializeField, Tooltip("A mask determining what is ground to the character")] 
	private LayerMask whatIsGround;                          
	[SerializeField, Tooltip("A position marking where to check if the player is grounded.")] 
	private Transform isGroundedPos;                            
	[SerializeField, Tooltip("A position marking where to check for ceilings")] 
	private Transform isCeilingPos;                           
	[SerializeField, Tooltip("A collider that will be disabled when crouching")] 
	private Collider2D disableColliderCrouch;

	// Radius of the overlap circle to determine if grounded // timer to ensure ground is far enough away from radius
	const float groundedRadius = .2f;
	private float delayGroundCheck = 0.25f;
	private float timeBeforeGroundCheck = 0f;
	// Whether or not the player is grounded.
	private bool isGrounded;
	// Radius of the overlap circle to determine if the player can stand up
	const float isCeiling = .2f; 
	// For determining which way the player is currently facing.
	private bool faceRight = true;  
	//references to position and rigidbody
	private Vector3 chkVelocity = Vector3.zero;
	private Rigidbody2D chkRigidBody;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;
	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent onCrouchEvent;
	private bool wasCrouching = false;

	public bool isFalling = false;

	private void Awake()
	{
		chkRigidBody = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (onCrouchEvent == null)
			onCrouchEvent = new BoolEvent();
	}
    private void Update()
    {
        if (!isGrounded)
        {
			timeBeforeGroundCheck -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
	{
		if (timeBeforeGroundCheck > 0f)
        {
			return;
        }
		bool wasGrounded = isGrounded;
		isGrounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(isGroundedPos.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(isCeilingPos.position, isCeiling, whatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (isGrounded || airControl)
		{

			// If crouching
			if (crouch)
			{
				if (!wasCrouching)
				{
					wasCrouching = true;
					onCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= crouchSpeed;

				// Disable one of the colliders when crouching
				if (disableColliderCrouch != null)
					disableColliderCrouch.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (disableColliderCrouch != null)
					disableColliderCrouch.enabled = true;

				if (wasCrouching)
				{
					wasCrouching = false;
					onCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, chkRigidBody.velocity.y);
			// And then smoothing it out and applying it to the character
			chkRigidBody.velocity = Vector3.SmoothDamp(chkRigidBody.velocity, targetVelocity, ref chkVelocity, movementSmooth);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !faceRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && faceRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (isGrounded && jump)
		{
			// Add a vertical force to the player.
			isGrounded = false;
			chkRigidBody.AddForce(new Vector2(0f, jumpForce));

			timeBeforeGroundCheck = delayGroundCheck;
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		faceRight = !faceRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	//detect if player is falling
	//toggle falling bool check
	public void Falling()
    {
		if (chkRigidBody.GetComponent<Rigidbody2D>().velocity.y < -1f)
        {
            {
				isFalling = true;
			}
		}
        else
        {
			isFalling = false;
        }

	}
}