using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story Item", menuName = "Category Items")]
public class CategoryItems : ScriptableObject
{
	public List<string> Names;
}
