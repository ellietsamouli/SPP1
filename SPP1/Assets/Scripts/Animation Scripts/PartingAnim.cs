using UnityEngine;

public class PartingAnim : GenericBehaviour
{
	public string partingButton = "Parting";
	private int partingBool;
	private bool parting = false;


	void Start()
	{

		partingBool = Animator.StringToHash("Parting");


		behaviourManager.SubscribeBehaviour(this);
	}

	void Update()
	{
		if (Input.GetButtonDown(partingButton) && !behaviourManager.IsOverriding()
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			parting = !parting;

			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);



			if (parting)
			{
				behaviourManager.RegisterBehaviour(this.behaviourCode);
			}
			else
			{
				behaviourManager.GetCamScript.ResetTargetOffsets();

				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
		}

		parting = parting && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

		behaviourManager.GetAnim.SetBool(partingBool, parting);
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
			parting = false;
			behaviourManager.GetAnim.SetBool(partingBool, parting);
			behaviourManager.UnregisterBehaviour(this.behaviourCode);
		}

	}
}
