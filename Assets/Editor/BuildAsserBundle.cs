using UnityEditor;
using System.IO;

public class BuildAssetBundle
{
    //�����˵�������ѡ��
    [MenuItem("Asset Bundles/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        //Ҫ�������ļ�������
        string dir = "AssetBundles";
        //��������ڸ��ļ������½���
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }
        // ���ʲ����Ƿ�������Windowsƽ̨����Դ�õ�
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}