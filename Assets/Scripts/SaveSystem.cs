
//using System.IO;
//using UnityEngine;
//public class information
//{
//    public int round;
//    public float hp;
//    public int money;
//    public 

//}
////namespace SaveSystemTutorial
////{
////    public static class SaveSystem 
////    {
////        public static void SaveByJson(string filename,object data)
////        {
////            var json = JsonUtility.ToJson(data);
////            var path = Path.Combine(Application.persistentDataPath, filename);

////            try
////            {
////            File.WriteAllText(path,json);
////#if UNITY_EDITOR
////            Debug.Log($"Susscessfully save data to{path}. ");
////#endif
////            }
////            catch (System.Exception exception)
////            {
////                Debug.LogError($"Faild to save data to{path}. \n{exception}");
////            }
////        }

////        public static T LoadFromJson<T>(string filename)
////    {

////        var path = Path.Combine(Application.persistentDataPath, filename);

////        try
////        {
////            var json = File.ReadAllText(path);
////            var data = JsonUtility.FromJson<T>(json);
////            return data;
////        }
////        catch(System.Exception e)  
////        {
////#if UNITY_EDITOR
////            Debug.LogError($"Faild to load data from{path}. \n{e}");
////#endif
////            return default;
////        }
////    }

////        public static void DeleteSaveFile(string filename)
////    {
////            var path = Path.Combine(Application.persistentDataPath, filename);
////            try
////            {
////                File.Delete(path);
////            }catch(System.Exception e)
////            {
////#if UNITY_EDITOR
////                Debug.LogError($"Faild to delete {path}. \n{e}");
////#endif
////            }
////        }
////    }

////}

