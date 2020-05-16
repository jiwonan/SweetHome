using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTime : MonoBehaviour
{
	public float LimitTime = 300f;
	public Text text;

	// Update is called once per frame
	void Update()
	{
		if (LimitTime <= 0)
		{
			text.GetComponent<Text>().text = "제한시간 끝남";
			return;
		}
		else
		{
			LimitTime -= Time.deltaTime;

			int time_ = (int)LimitTime;
			string text_;
			string time = time_.ToString();

			text_ = String.Concat("제한시간 : ", time);
			text.GetComponent<Text>().text = text_;
		}
	}
}
