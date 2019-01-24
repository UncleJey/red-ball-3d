using UnityEngine;
using System.Collections;

public class LootCollector : MonoBehaviour 
{
	float timer = 0;
	public LayerMask lootMask;

	void LateUpdate () 
	{
		timer -= Time.deltaTime;
		if (timer < 0)
		{
			timer = 0.03f;
			Collider2D dc = Physics2D.OverlapCircle(transform.position, 2f, lootMask);
			if (dc)
				GameManager.loot(dc.gameObject);
		}
	}
}
