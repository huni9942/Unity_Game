using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



[System.Serializable]
public class GoogleData
{
	public string order, result, msg;
}


public class PM : MonoBehaviour
{
	const string URL = "https://script.google.com/macros/s/AKfycbxLmNi7NneG0Q3QVeEGQFH7nZcr8pyUqpaa68mUt5sRTz_M1Bulg9NdkQi4TdPRuTRC-g/exec";
	public GoogleData GD;
	public InputField ID, Password;
	string id, pass;



	bool SetIDPass()
	{
		id = ID.text.Trim();
		pass = Password.text.Trim();

		if (id == "" || pass == "") return false;
		else return true;
	}


	public void Register()
	{
		if (!SetIDPass())
		{
			print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
			return;
		}

		WWWForm form = new WWWForm();
		form.AddField("order", "register");
		form.AddField("id", id);
		form.AddField("pass", pass);

		StartCoroutine(Post(form));
	}


	public void Login()
	{
		if (!SetIDPass())
		{
			print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
			return;
		}

		WWWForm form = new WWWForm();
		form.AddField("order", "login");
		form.AddField("id", id);
		form.AddField("pass", pass);

		StartCoroutine(Post(form));

		SceneManager.LoadScene("ProgressScene");
	}


	void OnApplicationQuit()
	{
		WWWForm form = new WWWForm();
		form.AddField("order", "logout");

		StartCoroutine(Post(form));
	}


	IEnumerator Post(WWWForm form)
	{
		using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
		{
			www.uploadHandler.Dispose();

			yield return www.SendWebRequest();

			if (www.isDone) Response(www.downloadHandler.text);
			else print("���� ������ �����ϴ�.");
		}
	}


	void Response(string json)
	{
		if (string.IsNullOrEmpty(json)) return;

		GD = JsonUtility.FromJson<GoogleData>(json);

		if (GD.result == "ERROR")
		{
			print(GD.order + "�� ������ �� �����ϴ�. ���� �޽��� : " + GD.msg);
			return;
		}

		print(GD.order + "�� �����߽��ϴ�. �޽��� : " + GD.msg);
	}
}
