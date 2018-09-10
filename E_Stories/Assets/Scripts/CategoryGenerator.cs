using UnityEngine;
using UnityEngine.UI;

public class CategoryGenerator : Generator
{
	public CategoryItems Items;
	public GameObject MainObject;
	public int ObjectsCount;
   
	private void Start()
	{
		Generate(MainObject, ObjectsCount);
	}
   
	public override void Generate(GameObject obj, int count)
	{   
		for (int i = 0; i < count; i++)
		{
			GameObject temp = Instantiate(obj, transform);
			temp.GetComponentInChildren<Text>().text = Items.Names[i];
		}
   
	}
}
