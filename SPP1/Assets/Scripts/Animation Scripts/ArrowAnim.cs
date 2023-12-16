using UnityEngine;

public class ArrowAnim : GenericBehaviour
{
	public string arrowButton = "Arrow";
	private int arrowBool;
	private bool arrow = false;


	void Start()
	{

		arrowBool = Animator.StringToHash("Arrow");


		behaviourManager.SubscribeBehaviour(this);
	}

	void Update()
	{
		if (Input.GetButtonDown(arrowButton) && !behaviourManager.IsOverriding()
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			arrow = !arrow;

			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);



			if (arrow)
			{
				behaviourManager.RegisterBehaviour(this.behaviourCode);
			}
			else
			{
				behaviourManager.GetCamScript.ResetTargetOffsets();

				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
		}

		arrow = arrow && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

		behaviourManager.GetAnim.SetBool(arrowBool, arrow);
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
			arrow = false;
			behaviourManager.GetAnim.SetBool(arrowBool, arrow);
			behaviourManager.UnregisterBehaviour(this.behaviourCode);
		}

	}
}
