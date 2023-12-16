using UnityEngine;

public class KnockingOut : GenericBehaviour
{
	public string knockOutButton = "KnockOut";              
	private int knockOutBool;                          
	private bool knockOut = false;                     

	
	void Start()
	{
		
		knockOutBool = Animator.StringToHash("KnockOut");

		
		behaviourManager.SubscribeBehaviour(this);
	}

	void Update()
	{
		if (Input.GetButtonDown(knockOutButton) && !behaviourManager.IsOverriding()
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			knockOut = !knockOut;

			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);



			if (knockOut)
			{
				behaviourManager.RegisterBehaviour(this.behaviourCode);
			}
			else
			{
				behaviourManager.GetCamScript.ResetTargetOffsets();

				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
		}

		knockOut = knockOut && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

		behaviourManager.GetAnim.SetBool(knockOutBool, knockOut);
	}

	
	public override void OnOverride()
	{

	}

	public override void LocalFixedUpdate()
	{
		ShootManagment(behaviourManager.GetH, behaviourManager.GetV);
	}
	
	void ShootManagment(float horizontal, float vertical)
	{
		if (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !behaviourManager.GetAnim.IsInTransition(0))
		{
			knockOut = false;
			behaviourManager.GetAnim.SetBool(knockOutBool, knockOut);
			behaviourManager.UnregisterBehaviour(this.behaviourCode);
		}

	}
}
