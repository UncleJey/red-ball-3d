using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Foranj.SDK.GUI;

public class DoneWindow : MonoBehaviour 
{
	public GroupLayoutPool pool;

	void Start () 
	{
		foreach (KeyValuePair <ItemName, int> item in GameManager.lootCount) 
		{
			GotItem uItem = pool.InstantiateElement().GetComponent<GotItem>();
			uItem.init (item.Key, PlayerItems.itemCount(item.Key).ToString()+ "/"+ item.Value.ToString());
		}
	}
	
}
