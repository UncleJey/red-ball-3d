using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class Language
{
	public static Dictionary<string, string> rules = new Dictionary<string, string>();
	public static string curLang = "en";

	public static string GetValue(string pKey, string a="", string b="")
	{
		if (rules.Count < 1) 
		{
			if (Application.systemLanguage == SystemLanguage.Russian
				|| Application.systemLanguage == SystemLanguage.Ukrainian
				|| Application.systemLanguage == SystemLanguage.Belarusian)
				toRus ();
			else
				toEng ();
//			toRus ();

		}

		if (rules.ContainsKey (pKey))
			return rules [pKey].Replace ("%a", a).Replace ("%b", b);
		else
			return pKey;
	}

	public static void toEng()
	{
		curLang = "en";
		rules ["play"] = "Play Me";
		rules ["more"] = "View More";
		rules ["menu"] = "Back to Menu";
		rules ["settings"] = "Settings";
		rules ["night"] = "Night\n%a";
		rules ["music"] = "Music Volume %a%";
		rules ["sfx"] = "SFX Volume %a%";

		rules ["control"] = "Control Type";
		rules ["tap"] = "Screen Tap";
		rules ["joy"] = "Virtual Joystick";
		rules ["arr"] = "Virtual Arrows";
		rules ["pad"] = "Game Pad";

		rules ["n1"] = "Falling Asleep";
		rules ["n2"] = "First Row";
		rules ["n3"] = "The Dog";
		rules ["n4"] = "Angry Man";
		rules ["n5"] = "The Nettle";

		rules ["ban"] = "Game version is outdated! Please update!";
		rules ["close"] = "Close in %a";
		rules ["loading"] = "Loading ..";
	}

	public static void toRus()
	{
		curLang = "ru";
		rules ["play"] = "Поиграть";
		rules ["more"] = "Больше";
		rules ["settings"] = "Настройки";
		rules ["night"] = "Ночь\n%a";
		rules ["menu"] = "Назад в Меню";
		rules ["music"] = "Громкоcть музыки %a%";
		rules ["sfx"] = "Громкоcть эффектов %a%";

		rules ["control"] = "Способ Управления";
		rules ["tap"] = "Screen Tap";
		rules ["joy"] = "Virtual Joystick";
		rules ["arr"] = "Virtual Arrows";
		rules ["pad"] = "Game Pad";

		rules ["n1"] = "Провал в сон";
		rules ["n2"] = "Первый скандал";
		rules ["n3"] = "Собака";
		rules ["n4"] = "Злоба";
		rules ["n5"] = "Крапива";

		rules ["close"] = "Закрыть [%a]";
		rules ["loading"] = "Загрузка ..";

		rules ["ban"] = "Версия устарела! Пожалуйста обновите!";
	}
}
