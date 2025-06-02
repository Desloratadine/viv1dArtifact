using System.IO;
using UnityEngine;

public class AssetBundleLoader : MonoBehaviour
{
    //public static AssetBundleLoader Instance { get; private set; }
    private string assetBundlePath;
    private AssetBundle assetBundle;
    private string BundleName;
    public string[] assetNames;
    public AssetBundleLoader(string bundleName)//���췽����ȡ��Դ������
    {
        BundleName = bundleName;
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else
        //{
        //    //Debug.LogWarning("AssetBundleLoader instance already exists. Using the existing instance.");
        //    return;
        //}

#if UNITY_EDITOR
        assetBundlePath = $"C:/game/viv1dArtifact/AssetBundles/{bundleName}"; 
        
#else
        assetBundlePath = Path.Combine(Application.persistentDataPath, BundleName);
#endif

        LoadAssetBundle();
        FindAsset(bundleName);
    }
    
    void Start()
    {

        
    }
    private void LoadAssetBundle()
    {
        assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
        assetNames = assetBundle.GetAllAssetNames();
    }
    public AssetBundle GetBundle()
    {
        return assetBundle;
    }
    public void FindAsset(string name)
    {
        foreach (string assetName in assetNames)
        {
                Debug.Log("�ҵ���Դ��" + assetName); 

        }
        Debug.Log("��������");
    }
   
    void Update()
    {
        
    }
}
