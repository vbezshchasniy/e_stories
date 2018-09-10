using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story Item", menuName = "Category Buttons")]
public class CategoryButtons : ScriptableObject
{
	public List<string> Names;
	public List<Sprite> Pictures;
	public int Id;
}
