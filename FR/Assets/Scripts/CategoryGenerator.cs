using UnityEngine;
using UnityEngine.UI;

public class CategoryGenerator : Generator
{
    public DataCategory DataCategory;
	public GameObject MainObject;
   
	private void Start()
	{
		Generate(MainObject, DataCategory.Categories.Length);
	}
   
	public override void Generate(GameObject obj, int count)
	{   
		for (int i = 0; i < count; i++)
		{
			GameObject temp = Instantiate(obj, transform);
			temp.GetComponentInChildren<Text>().text = DataCategory.Categories[i].Name;
			temp.GetComponentInChildren<ButtonElementsGenerator>().ButtonElements = DataCategory.Categories[i].Type;
		}
	}
}
