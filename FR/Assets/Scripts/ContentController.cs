using System;
using System.Collections;
using System.IO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ContentController : MonoBehaviour
{
    public string URL;
    public Transform ContentPanel;
    public DataDialogues DataDialogues;
    public bool IsShowDialogue = true;
    public int CurrentNode;
    public GameObject CurrentContent;
    public GameObject LoaderPanel;

    //TODO fix this
    //Just for fun
    public Text BytesDownloadedText;

    public Image LoaderImage;
    //

    private WTController WtController;
    private bool IsDownloading;

    public static ContentController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoaderPanel.SetActive(true);
        // ReSharper disable once HeapView.ObjectAllocation
        StartCoroutine(LoadBundle("resbundle"));
    }

    private IEnumerator LoadBundle(string fileName)
    {
        var url = Path.Combine(URL, fileName);

        using (var www = UnityWebRequest.Get(url))
        {
            Debug.Log("Trying the bundle download");
//            IsDownloading = true;
            // ReSharper disable once HeapView.ObjectAllocation
            StartCoroutine(ShowProgress(www, www.isDone));
            yield return www.SendWebRequest();
//            IsDownloading = false;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                #region PrevLogic

                Debug.Log("Bundle downloaded");
                //save the asset bundle
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

                //Prev Logic
                //instantiate the prefab
                var obj = bundle.mainAsset;
                if (obj != null)
                    Instantiate(obj, Vector3.zero, Quaternion.identity);
                else
                    Debug.Log("Couldn't load resource");

                var assetBundle = bundle.LoadAsset<GameObject>("AssetBundle");
                CurrentContent = Instantiate(assetBundle, ContentPanel.transform.position, transform.rotation);
                CurrentContent.transform.SetParent(ContentPanel.transform, false);
                LoaderPanel.SetActive(false);
                WtController = CurrentContent.GetComponent<WTController>();
                UpdateData();

                #endregion

                //////////
                ///SaveData
                /// /////

                var savePath = Path.Combine(Application.persistentDataPath, "AssetData");
                SaveAssetBundleToDisk(www.downloadHandler.data, Path.Combine(savePath, fileName));
            }
        }




//        		AssetBundle assetBundle = download.assetBundle;
//		if (assetBundle != null) {
//			// Alternatively you can also load an asset by name (assetBundle.Load("my asset name"))
//			Object go = assetBundle.mainAsset;
//
//			if (go != null)
//				Instantiate(go);
//			else
//				Debug.Log("Couldn't load resource");
//		} else {
//			Debug.Log("Couldn't load resource");
//		}
//
//
//
//		var myAsset = www.assetBundle;
//		var bundle = myAsset.LoadAsset<GameObject>("AssetBundle");
//		CurrentContent = Instantiate(bundle, ContentPanel.transform.position, transform.rotation);
//		CurrentContent.transform.SetParent(ContentPanel.transform, false);
//		LoaderPanel.SetActive(false);
//		WtController = CurrentContent.GetComponent<WTController>();
//		UpdateData();
//


    }

    private IEnumerator ShowProgress(UnityWebRequest uwr, bool isDone)
    {
        // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
        while (!isDone)
        {
            BytesDownloadedText.text = string.Format("Downloaded {0}", uwr.downloadedBytes);
            LoaderImage.fillAmount = uwr.downloadProgress;
            Debug.Log(string.Format("Downloaded {0}", uwr.downloadedBytes));
            Debug.Log(string.Format("Downloaded {0:P1}", uwr.downloadProgress));

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SaveAssetBundleToDisk(byte[] data, string filePath)
    {
        //Create the Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            if (filePath != null)
                // ReSharper disable once AssignNullToNotNullAttribute
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }

        try
        {
            if (filePath != null)
            {
                File.WriteAllBytes(filePath, data);
                Debug.Log(string.Format("Saved Data to: {0}", filePath.Replace("/", "\\")));
            }
        }
        catch (Exception e)
        {
            if (filePath != null)
                Debug.LogWarning(string.Format("Failed To Save Data to: {0}", filePath.Replace("/", "\\")));
            Debug.LogWarning(string.Format("Error: {0}", e.Message));
        }
    }


    private IEnumerable LoadObject(string path)
    {
        var bundle = AssetBundle.LoadFromFileAsync(path);
        yield return bundle;

        var myLoadedAssetBundle = bundle.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }

        var request = myLoadedAssetBundle.LoadAssetAsync<GameObject>("smth");
        yield return request;

        GameObject obj = request.asset as GameObject;

        Instantiate(obj);

        myLoadedAssetBundle.Unload(false);
    }

//
//		var download = UnityWebRequestAssetBundle.GetAssetBundle(URL);
//
//		while (!Caching.ready)
//		{
//			yield return null;
//			BytesDownloadedText.text = string.Format("Downloaded {0:P1}", download.downloadedBytes);
//			LoaderImage.fillAmount = download.downloadProgress;
//			Debug.Log(string.Format("Downloaded {0:P1}", download.downloadedBytes));
//		}
//
//		download = UnityWebRequest.LoadFromCacheOrDownload(URL, 0);
//		yield return download;
////
//		AssetBundle assetBundle = download.assetBundle;
//		if (assetBundle != null) {
//			// Alternatively you can also load an asset by name (assetBundle.Load("my asset name"))
//			Object go = assetBundle.mainAsset;
//
//			if (go != null)
//				Instantiate(go);
//			else
//				Debug.Log("Couldn't load resource");
//		} else {
//			Debug.Log("Couldn't load resource");
//		}
//
//
//
//		var myAsset = www.assetBundle;
//		var bundle = myAsset.LoadAsset<GameObject>("AssetBundle");
//		CurrentContent = Instantiate(bundle, ContentPanel.transform.position, transform.rotation);
//		CurrentContent.transform.SetParent(ContentPanel.transform, false);
//		LoaderPanel.SetActive(false);
//		WtController = CurrentContent.GetComponent<WTController>();
//		UpdateData();
//	}

    public void Respondent(int answer)
    {
        if (!IsShowDialogue)
            return;

        if (DataDialogues.Nodes[CurrentNode].PlayerAnswer[answer].SpeakEnd)
        {
            IsShowDialogue = false;
        }

        CurrentNode = DataDialogues.Nodes[CurrentNode].PlayerAnswer[answer].ToNode;
        CurrentContent.GetComponent<WTController>().BotText.text = DataDialogues.Nodes[CurrentNode].NpcText;
        WtController.UpdateContent();
        AnimateText();
    }

    public void AnimateText()
    {
        var mySequence = DOTween.Sequence();
        mySequence.Append(WtController.BotText.DOFade(0, .25f));
        mySequence.AppendCallback(UpdateData);
        mySequence.AppendInterval(.5f);
        mySequence.Append(WtController.BotText.DOFade(1, .25f));
    }

    private void UpdateData()
    {
        WtController.BotText.text = DataDialogues.Nodes[CurrentNode].NpcText;
    }
}

public enum ContentType
{
    Model3D,
    Video,
    Image,
    Sound,
    Empty
}