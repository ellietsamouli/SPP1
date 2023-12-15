using UnityEngine;

public class Shooting : GenericBehaviour
{
	public string shootButton = "Shoot";              // Default  button.
	private int shootBool;                          // Animator variable related to flying.
	private bool shoot = false;                     // Boolean to determine whether or not the player activated  shooting.

	// Start is always called after any Awake functions.
	void Start()
	{
		// Set up the references.
		shootBool = Animator.StringToHash("Shoot");

		//Subscribe this behavior to the manager.
		behaviourManager.SubscribeBehaviour(this);
	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		// Toggle fly by input, only if there is no overriding state or temporary transitions.
		if (Input.GetButtonDown(shootButton) && !behaviourManager.IsOverriding()
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			shoot = !shoot;

			// Force end jump transition.
			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

			

			if (shoot)
			{
				// Register this behaviour.
				behaviourManager.RegisterBehaviour(this.behaviourCode);
			}
			else
			{
				// Set collider direction to vertical.
				behaviourManager.GetCamScript.ResetTargetOffsets();

				// Unregister this behaviour and set current behaviour to the default one.
				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
		}

		// Assert this is the active behaviour
		shoot = shoot && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

		// Set fly related variables on the Animator Controller.
		behaviourManager.GetAnim.SetBool(shootBool, shoot);
	}

	// This function is called when another behaviour overrides the current one.
	public override void OnOverride()
	{

	}

	// LocalFixedUpdate overrides the virtual function of the base class.
	public override void LocalFixedUpdate()
	{
		//Deal with player movement when shooting
		ShootManagment(behaviourManager.GetH, behaviourManager.GetV);
	}
	// Deal with the player movement when flying.
	void ShootManagment(float horizontal, float vertical)
	{
		if (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !behaviourManager.GetAnim.IsInTransition(0))
        {
			shoot = false;
			behaviourManager.GetAnim.SetBool(shootBool, shoot);
			behaviourManager.UnregisterBehaviour(this.behaviourCode);
        }
			
	}

}
