using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour 
{
	public Button playButton;
	public Button moreButton;
	public Button settingsButton;
	public Button backToMenuButton;

	public Text loadingText;

	public ScrollRect menu;
	public RectTransform menuList;
	public RectTransform Settings;
	public GameObject quitWindow;

	void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 40;
	}

	void Start()
	{
		CBArcher.showImp ();
		ShowMenu ();
	}

	void OnEnable () 
	{
		playButton.onClick.AddListener(goStart);
		moreButton.onClick.AddListener(showMore);
		settingsButton.onClick.AddListener (ShowSettings);
		backToMenuButton.onClick.AddListener (ShowMenu);
	}

	void OnDisable()
	{
		playButton.onClick.RemoveAllListeners();
		moreButton.onClick.RemoveAllListeners ();
		settingsButton.onClick.RemoveAllListeners ();
		backToMenuButton.onClick.RemoveAllListeners ();
	}
	
	void goStart()
	{
		loadingText.gameObject.SetActive (true);
		Application.LoadLevel ("main");
	}

	void showMore()
	{
		CBArcher.showMore ();
	}

	void Update()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) // чтобы реклама сразу не закрывалась
			quitWindow.gameObject.SetActive(!quitWindow.gameObject.activeSelf);
	}

	IEnumerator showImp()
	{
		yield return null;
		yield return null;
		yield return null;
		CBArcher.showImp ();
	}

	void ShowSettings()
	{
		menu.content = Settings;
		menuList.gameObject.SetActive (false);
		Settings.gameObject.SetActive (true);
		StartCoroutine (scr ());
	}

	void ShowMenu()
	{
		menu.content = menuList;
		menuList.gameObject.SetActive (true);
		Settings.gameObject.SetActive (false);
		StartCoroutine (scr ());
	}

	IEnumerator scr()
	{
		yield return null;
		yield return null;
		menu.velocity = new Vector2 (0, -2000);
	}
}
