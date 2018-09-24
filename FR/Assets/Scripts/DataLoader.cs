using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
	public string[] Items;

	private void Start ()
	{
		StartCoroutine(ConnectToServer());
	}

	private IEnumerator ConnectToServer()
	{
		WWW itemsData = new WWW("http://127.0.0.1:8080/fr_database/ItemsData.php");
		yield return itemsData;
		
		string itemsDataString = itemsData.text;
		Debug.Log(itemsDataString);

		Items = itemsDataString.Split(';');
		if (Items.Length > 1)
		{
			Debug.Log(GetDataValue(Items[0], "Text:"));
		}
	}

	private string GetDataValue(string data, string index)
	{
		string value = data.Substring(data.IndexOf(index) + index.Length);
		if (value.Contains("|"))
		{
			value = value.Remove(value.IndexOf("|"));
		}
		return value;
	}
}
