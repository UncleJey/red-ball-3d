using UnityEngine;
using System.Collections;


/// <summary>
/// Может ли перемещаться объект
/// </summary>
public enum movement:byte 
{
	 NONE		= 0	// Не может двигаться
	,HORISONTAL	= 1	// Только горизонтально
	,VERTICAL	= 2	// Только вертикально
	,BOTH		= 3	// Сфера
}

/// <summary>
/// Состояния объекта
/// </summary>
public enum stateName:byte
{
	 IDLE	= 0
	,MOVE	= 1
	,ATACK	= 2
	,DIE	= 3
}

[System.Serializable]
public class stateType
{
	public stateName state;			// Тип состояния
	public string animationName;	// Имя анимации
	public float time;				// Продолжительность
}

public class enemy : MonoBehaviour
{
	Vector3 basePos;
	public float moveRadius = 0f;
	public movement canMove = movement.NONE;
	public bool canAtack = false;
	public stateType[] states;
	Animation animator;

	float timer;			// Таймер до окончания состояния
	Vector3 destPos;		// Куда движемся
	stateType curState;		// Текущее состояние
	bool activated = false;

	void Awake()
	{
		basePos = transform.localPosition;
		animator = GetComponent<Animation>();
		if (animator == null)
			animator = GetComponentInChildren<Animation>();
		StartCoroutine(changeState());
	}

	IEnumerator changeState()
	{
		//yield return new WaitForSeconds(0.5f);
		if (states != null && states.Length > 0)
		{
			int n = Random.Range(1,states.Length+1);
			int i = 0;
			stateType st = null;
			while (n>0)
			{
				if (i>=states.Length)
					i = 0;

				st = states[i];
				if ((canMove != movement.NONE && st.state == stateName.MOVE) || st.state == stateName.IDLE)
					n--;
				i++;
				yield return null;
			}

			// Выбрали новое состояние случайным образом
			if (animator != null)
				animator.Play(st.animationName);

			if (st.state == stateName.IDLE)
				timer = st.time;
			else
			{
				destPos = new Vector3(basePos.x, basePos.y, basePos.z);
				if (canMove == movement.BOTH || canMove == movement.HORISONTAL)
					destPos.x += Random.value * moveRadius / 2f - Random.value * moveRadius / 2f;
				if (canMove == movement.BOTH || canMove == movement.VERTICAL)
					destPos.y += Random.value * moveRadius / 2f - Random.value * moveRadius / 2f;
			}
			curState = st;
			activated = true;
		}
		yield return null;
	}

	void Update()
	{
		if (activated)
		{
			if (curState.state == stateName.IDLE)
			{
				timer -= Time.deltaTime;
				if (timer < 0)
				{
					activated = false;
					StartCoroutine(changeState());
				}
			}
			else
			{
				transform.LookAt(destPos);
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, destPos, Time.deltaTime);
				if (Vector3.SqrMagnitude(transform.localPosition - destPos) < 0.1f)
				{
					activated = false;
					StartCoroutine(changeState());
				}
			}
		}
	}

}
