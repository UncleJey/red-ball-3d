using UnityEngine;
using System.Collections;

public enum EqualType: byte {None = 0, More = 1, Less = 2, Equal = 3, NonEqual = 4, InsideX = 5, OutsideX = 6, InsideY = 7, OutsideY = 8}

[System.Serializable]
public class TrState
{
	public EqualType type;
	public Rect area;
	public string TriggerName;
}

public class Traning : MonoBehaviour 
{
	public Animator animator;
	public Transform trObject;
	public TrState[] states;

	int stateNo = -1;

	void Start () 
	{
		nextState();
	}

	void nextState()
	{
		stateNo++;
		if (stateNo >= states.Length)
			gameObject.SetActive(false);
		else
			animator.SetTrigger(states[stateNo].TriggerName);
	}

	void LateUpdate () 
	{
		switch (states[stateNo].type)
		{
			case EqualType.None:
				nextState();
			break;
			case EqualType.InsideX:
				if (trObject.localPosition.x > states[stateNo].area.x && trObject.localPosition.x < states[stateNo].area.width)
					nextState();
			break;
			case EqualType.OutsideX:
				if (trObject.localPosition.x < states[stateNo].area.x || trObject.localPosition.x > states[stateNo].area.width)
					nextState();

			break;
			case EqualType.InsideY:
				if (trObject.localPosition.y > states[stateNo].area.y && trObject.localPosition.y < states[stateNo].area.height)
					nextState();
			break;
			case EqualType.OutsideY:
				if (trObject.localPosition.y < states[stateNo].area.y || trObject.localPosition.y > states[stateNo].area.height)
					nextState();
			break;
			default:
				Debug.LogError("unknown state type : "+states[stateNo].type.ToString());
			break;

		}
	}
}
