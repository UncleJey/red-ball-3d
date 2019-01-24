using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GotItem : MonoBehaviour 
{
	public Text count;
	public Image sprite;

	public void init(ItemName pName, string pCount)
	{
		ItemType item = PlayerItems.getItem (pName);
		sprite.sprite = item.sprite;
		count.text = pCount;
	}

}
