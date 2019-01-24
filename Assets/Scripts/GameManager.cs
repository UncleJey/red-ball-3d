using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;
	public Text coinText;
	public GameObject donePanel;
	public Button doneButton;
	public GameObject blob;
	public static Dictionary <ItemName, int> lootCount;

	static int _coins = 0;

	public static int coins
	{
		get
		{
			return _coins;
		}
		set
		{
			if (_coins != value)
				instance.coinText.GetComponent<AnimationBase>().Animate();

			_coins = value;
			instance.coinText.text = _coins.ToString();
		}
	}

	void Awake () 
	{
		instance = this;
		QualitySettings.vSyncCount = 1;
		Application.targetFrameRate = 40;
		blob.gameObject.SetActive (true);
	}

	void Start()
	{
		CalcLootCount ();
		coins = 0;
		
		foreach (KeyValuePair<ItemName, int> itm in lootCount)
			Debug.Log (itm.Key.ToString () + "=" + itm.Value.ToString ());

	}

	void CalcLootCount()
	{
		ItemOnField[] items = WorldManager.instance.GetComponentsInChildren<ItemOnField> (true);
		if (lootCount == null)
			lootCount = new Dictionary <ItemName, int> ();
		else
			lootCount.Clear ();

		foreach (ItemOnField item in items) 
		{
			if (lootCount.ContainsKey(item.itemName))
				lootCount[item.itemName]+= item.count;
			else
				lootCount[item.itemName] = item.count;
		}

		ActivatorCoin[] blocks = WorldManager.instance.GetComponentsInChildren<ActivatorCoin> (true);
		foreach (ActivatorCoin block in blocks) 
		{
			if (lootCount.ContainsKey(block.itemName))
				lootCount[block.itemName]+= block.itemCount;
			else
				lootCount[block.itemName] = block.itemCount;
		}
	}

	public static void updateCounts(ItemName pName, int pCount)
	{
		if (pName == ItemName.COIN)
			coins = pCount;
		else if (pName == ItemName.NONE)
			coins = PlayerItems.itemCount (ItemName.COIN);
	}

	public static void loot(GameObject go)
	{
		ItemOnField itm = go.GetComponent<ItemOnField> ();
		if (itm) 
		{
			PlayerItems.AddItem(itm.itemName, itm.count);
			MusicProc.doEvent(ActionEvent.Loot, itm.itemName);
			Destroy (go);
			return;
		}
		CheckPoint cp = go.GetComponent<CheckPoint> ();
		if (cp)
		{
			if (cp.finalDoor)
				instance.LevelDone();
			else
				cp.check();
		}
	}

	void LevelDone()
	{
		blob.gameObject.SetActive (false);
		donePanel.gameObject.SetActive (true);
	}

	void OnEnable()
	{
		doneButton.onClick.AddListener (goMenu);
	}

	void OnDisable()
	{
		doneButton.onClick.RemoveAllListeners ();
	}

	void goMenu()
	{
		Application.LoadLevel ("menu");
	}
}
