using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Category Typ", menuName = "New Category Type")]
public class CategoryType : ScriptableObject
{
	public CategoryItem[] CategoryItems;
}

[Serializable]
public class CategoryItem
{
	public string Name;
	public int Id;
	public Sprite Picture;
}
