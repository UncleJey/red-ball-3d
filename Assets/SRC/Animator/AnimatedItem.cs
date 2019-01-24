using UnityEngine;
using System.Collections;

public class AnimatedItem : MonoBehaviour 
{
	public Sprite[] images;
	SpriteRenderer sr;
	public float FrameTime = 1.0f / 16f;
	float timer = 0f;
	int spriteNo = 0;

	void Awake () 
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void ChangeSprite()
	{
		spriteNo++;
		if (spriteNo >=images.Length)
			spriteNo = 0;
		sr.sprite = images[spriteNo];
	}
	
	void Update () 
	{
		timer -= Time.deltaTime;
		if (timer < 0)
		{
			timer += FrameTime;
			ChangeSprite();
		}
	}
}
