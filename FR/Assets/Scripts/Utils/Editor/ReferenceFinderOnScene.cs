using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ReferenceFinderOnScene : EditorWindow
{
    Object Source;
    Vector2 ScrollPosition;
    List<ReferenceData> ReferencedBy = new List<ReferenceData>();

    [MenuItem("Window/Tools/Reference finder on scene")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ReferenceFinderOnScene));
    }

    void OnGUI()
    {
        //if (Event.current.type == EventType.MouseUp && new Rect(0, 0, 200, 200).Contains(Event.current.mousePosition))
        //    Application.OpenURL("https://wiki.murka.com/");
        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Documentation : ", GUILayout.Width(100));
        //EditorGUILayout.SelectableLabel("https://wiki.murka.com/");
        //EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Source = EditorGUILayout.ObjectField(Source, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Find!"))
        {
            if (Source == null)
                ShowNotification(new GUIContent("No object selected for searching"));
            else
            {
                ReferencedBy = FindReferencesTo(Source);
                if (ReferencedBy.Count == 0)
                    ShowNotification(new GUIContent("No references have found"));
            }
        }

        ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);
        for (int i = 0; i < ReferencedBy.Count; i++)
        {
            EditorGUILayout.ObjectField(ReferencedBy[i].ReferenceObject, typeof(Object), true);
            EditorGUILayout.LabelField("Script : " + ReferencedBy[i].ReferenceScript);
            EditorGUILayout.LabelField("Field : " + ReferencedBy[i].ReferenceField);
            EditorGUILayout.LabelField("Reference to : " + ReferencedBy[i].ReferenceTo);
        }
        EditorGUILayout.EndScrollView();
    }

    private List<ReferenceData> FindReferencesTo(Object to)
    {
        var referencedBy = new List<ReferenceData>();
        var allObjects = Object.FindObjectsOfType<GameObject>();
        var referenceTo = new List<Object>();
        referenceTo.Add(to);

        //if gameobject, find references to all components of it
        var toGO = to as GameObject;
        if (toGO)
        {
            var components = toGO.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                referenceTo.Add(components[i]);
            }
        }

        foreach (var go in AllSceneObjects())
        {
            if (PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
            {
                if (PrefabUtility.GetCorrespondingObjectFromSource(go) == to)
                {
                    referencedBy.Add(new ReferenceData(go, "Prefab instance", "", ""));
                }
            }

            var components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                var c = components[i];
                if (!c) continue;

                var so = new SerializedObject(c);
                var sp = so.GetIterator();

                while (sp.NextVisible(true))
                    if (sp.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        for (int k = 0; k < referenceTo.Count; k++)
                        {
                            if (sp.objectReferenceValue == referenceTo[k])
                            {
                                referencedBy.Add(new ReferenceData(c.gameObject, c.GetType().ToString(),
                                    sp.propertyPath, referenceTo[k].GetType().ToString()));
                            }
                        }
                    }
            }
        }

        return referencedBy;
    }

    public static IEnumerable<GameObject> SceneRoots()
    {
        var prop = new HierarchyProperty(HierarchyType.GameObjects);
        var expanded = new int[0];
        while (prop.Next(expanded))
        {
            yield return prop.pptrValue as GameObject;
        }
    }

    public static IEnumerable<GameObject> AllSceneObjects()
    {
        var queue = new Queue<GameObject>();

        foreach (var root in SceneRoots())
        {
            queue.Enqueue(root);
        }

        while (queue.Count > 0)
        {
            var curGO = queue.Dequeue();
            for (int i = 0; i < curGO.transform.childCount; i++)
            {
                queue.Enqueue(curGO.transform.GetChild(i).gameObject);
            }

            yield return curGO;
        }
    }

    class ReferenceData
    {
        public ReferenceData(GameObject ro, string rs, string rf, string rt)
        {
            ReferenceObject = ro;
            ReferenceScript = rs;
            ReferenceField = rf;
            ReferenceTo = rt;
        }

        public GameObject ReferenceObject;
        public string ReferenceScript;
        public string ReferenceField;
        public string ReferenceTo;
    }
}