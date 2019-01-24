using UnityEngine;
using System.Collections;

public class ActivatorShow : ActivatorBase
{
	public override void doAction()
	{
		if (!this.gameObject.activeSelf)
		{
			this.gameObject.SetActive (true);
			MusicProc.doEvent(ActionEvent.Open);
		}
	}

	public override void Init ()
	{
		if (this.gameObject.activeSelf)
			this.gameObject.SetActive (false);
	}
}
