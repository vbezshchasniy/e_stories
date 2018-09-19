using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object For Category", menuName = "Category Object")]
public class DataCategory : ScriptableObject
{
	public Category[] Categories;
}

[Serializable]
public class Category
{
	public string Name;
	public CategoryType Type;
}
