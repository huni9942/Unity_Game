using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



[System.Serializable]
public class GoogleData
{
	public string order, result, msg, id, pass, level;
}


public class PM : MonoBehaviour
{
	const string URL = "https://script.google.com/macros/s/AKfycbxLmNi7NneG0Q3QVeEGQFH7nZcr8pyUqpaa68mUt5sRTz_M1Bulg9NdkQi4TdPRuTRC-g/exec";
	public GoogleData GD;
	public InputField ID, Password;
	string level = "2";

	bool SetIDPass()
	{
		if (ID.text.Trim() == "" || Password.text.Trim() == "") return false;
		else return true;
	}


	public void Register()
	{
		if (!SetIDPass())
		{
			print("아이디 또는 비밀번호가 비어있습니다");
			return;
		}

		WWWForm form = new WWWForm();
		form.AddField("order", "register");
		form.AddField("id", ID.text.Trim());
		form.AddField("pass", Password.text.Trim());

		StartCoroutine(Post(form));
	}


	public void Login()
	{
		if (!SetIDPass())
		{
			print("아이디 또는 비밀번호가 비어있습니다");
			return;
		}

		WWWForm form = new WWWForm();
		form.AddField("order", "login");
		form.AddField("id", ID.text.Trim());
		form.AddField("pass", Password.text.Trim());

		StartCoroutine(Post(form));

		SceneManager.LoadScene("ProgressScene");
	}


	void OnApplicationQuit()
	{
		WWWForm form = new WWWForm();
		form.AddField("order", "logout");

		StartCoroutine(Post(form));
	}

	public void SetValue()
	{
		WWWForm form = new WWWForm();
		form.AddField("order", "setValue");
		form.AddField("level", level);

		StartCoroutine(Post(form));
	}


	public void GetValue()
	{
		WWWForm form = new WWWForm();
		form.AddField("order", "getValue");

		StartCoroutine(Post(form));
	}

	IEnumerator Post(WWWForm form)
	{
		using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
		{
			

			yield return www.SendWebRequest();

			if (www.isDone) Response(www.downloadHandler.text);
			else print("웹의 응답이 없습니다.");
		}
	}


	void Response(string json)
	{
		if (string.IsNullOrEmpty(json)) return;

		GD = JsonUtility.FromJson<GoogleData>(json);

		if (GD.result == "ERROR")
		{
			print(GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg);
			return;
		}

		print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);

		if (GD.order == "getValue")
		{
			level = GD.level;
		}
	}
}
