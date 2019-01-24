using UnityEngine;
using System.Collections;

public class ActivatorCoin : ActivatorBase
{
	public ItemName itemName;
	public int itemCount;
	public Sprite itemSprite;
	public ActionEvent eventName;

	float lastTimeCollision = 0;
	Animation animator;

	void Awake()
	{
		animator = GetComponent<Animation> ();
	}

	public override void Init ()
	{
	}

	public override void doAction()
	{
		if (Time.timeSinceLevelLoad - lastTimeCollision > 0.5f) 
		{
			lastTimeCollision = Time.timeSinceLevelLoad;
			if (BlobBehaviour.instance.transform.position.y < transform.position.y)
			{
				if (itemCount-->0)
				{
					if (animator != null)
						animator.Play();
					MusicProc.doEvent(eventName);
					PlayerItems.AddItem(itemName);
				}
			}
		}
	}

}
