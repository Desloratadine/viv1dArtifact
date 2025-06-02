using UnityEditor;
using System.IO;

public class BuildAssetBundle
{
    //在主菜单中增加选项
    [MenuItem("Asset Bundles/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        //要创建的文件夹名称
        string dir = "AssetBundles";
        //如果不存在该文件夹则新建它
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }
        // 该资产包是发布用于Windows平台的资源用的
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}