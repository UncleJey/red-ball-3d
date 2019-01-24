using UnityEngine;
using System.Collections;

public enum ActionEvent : byte 
{
	None	= 0,	// Ничегошеньки
	Jump	= 1,	// Прыжок
	Loot	= 2,	// Лут собран
	Coin	= 3,	// Монетки
	Start	= 4,	// Начало уровня
	End		= 5,	// Уровень пройден
	Bump	= 6,	// Удар
	Open	= 7,	// Открылось
	Landed	= 8,	// Приземлился
	ShowBlock = 9,  // Блок показался

	Fail	= 255	// Провал
}

[System.Serializable]
public class SoundEvent
{
	public string name;
	public ActionEvent type = ActionEvent.None;
	public AudioClip[] clip;
	public ItemName item = ItemName.NONE;
	[System.NonSerialized]
	public float lastTimePlay;
	[System.NonSerialized]
	public int countInThisSery;	// Количество проигранных в этой серии звуков

	public float IntervalBetweenReplay = 0.5f;
	public int maxInSeries = 10;

	public AudioClip GetClip()
	{
		if (clip.Length > 1)
		{
			int r = Random.Range(0, clip.Length);
			if (r>= clip.Length)
				r = 0;
			return clip[r];
		}
		else if (clip.Length == 1)
			return clip[0];
		return null;
	}
}

public class MusicProc : MonoBehaviour 
{
	public static MusicProc instance;
	public SoundEvent[] events;
	AudioSource src;

	void Awake () 
	{
		src = GetComponent<AudioSource> ();
		instance = this;
	}

	SoundEvent getEvent(ActionEvent e, ItemName n)
	{
		foreach(SoundEvent evt in events)
			if (evt.type == e && (n == ItemName.NONE || evt.item == n))
				return evt;
		return null;
	}

	static void PlayEvent(SoundEvent e)
	{
		if (Time.realtimeSinceStartup > e.lastTimePlay + e.IntervalBetweenReplay)
		{
			if (Time.realtimeSinceStartup - e.lastTimePlay < e.IntervalBetweenReplay * 3)
				e.countInThisSery++;
			else
				e.countInThisSery = 0;

			e.lastTimePlay = Time.realtimeSinceStartup;

			if (e.countInThisSery < e.maxInSeries)
			{
				AudioClip c = e.GetClip();
				if (c != null)
					instance.src.PlayOneShot(c, 1f);
			}
		}
	}

	public static void doEvent(ActionEvent e, ItemName n = ItemName.NONE)
	{
		if (instance != null && instance.src != null)
		{
			SoundEvent evt = instance.getEvent(e, n);
			if (evt != null)
				PlayEvent(evt);
			else if (n != ItemName.NONE)	// Если для этого типа лута не нашли звук то ищем дефолтный
			{
				evt = instance.getEvent(e, ItemName.NONE);
				if (evt != null)
					PlayEvent(evt);
			}
		}
	}

}
