using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class BundleEditor {

    private static string m_BundleTargetPath = Application.streamingAssetsPath;

    private static string ABCONFIGPATH = "Assets/Editor/ABConfig.asset";

    // 所有文件夹所属的ab包路径,key是AB包名,value是路径
    private static Dictionary<string, string> m_AllDirABPath = new Dictionary<string, string>();

    // 所有prefab的ab包路径列表
    private static Dictionary<string, List<string>> m_AllPrefabPath = new Dictionary<string, List<string>>();

    // 已经访问过的AB包路径
    private static List<string> m_VisitedABPath = new List<string>();

    // 储存所有有效的路径
    private static List<string> m_ConfigFilter = new List<string>();


    [MenuItem("Tool/打包")]
    public static void Build() {

        m_AllDirABPath.Clear();
        m_AllPrefabPath.Clear();
        m_VisitedABPath.Clear();
        m_ConfigFilter.Clear();
        

        ABConfig abConfig = AssetDatabase.LoadAssetAtPath<ABConfig>(ABCONFIGPATH);

        foreach (ABConfig.FileDirABName fileDir in abConfig.m_AllFileDirAB) {
            if (m_AllDirABPath.ContainsKey(fileDir.ABName)) {
                Debug.LogError("AB包配置名字重复,请检查!");
            } else {
                m_AllDirABPath.Add(fileDir.ABName, fileDir.Path);
                // 
                m_VisitedABPath.Add(fileDir.Path);

                m_ConfigFilter.Add(fileDir.Path);
            }
        }
        
        // 找到所有prefab的guid
        string[] allStr = AssetDatabase.FindAssets("t:Prefab", abConfig.m_AllPrefabPath.ToArray());

        for (int i = 0; i < allStr.Length; i++) {
            string path = AssetDatabase.GUIDToAssetPath(allStr[i]);

            EditorUtility.DisplayProgressBar("查找Prefab", "Prefab:" + path, i * 1.0f / allStr.Length);

            m_ConfigFilter.Add(path);

            // 如果过滤列表里没有这个prefab的路径
            if (!ContainedAllFileAB(path)) {

                // 获取prefab
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                // 获取prefab的所有依赖项路径
                string[] allDepend = AssetDatabase.GetDependencies(path);
                // prefab的未被打包过的依赖项的路径
                List<string> allDependPath = new List<string>();

                for (int j = 0; j < allDepend.Length; j++) {
                    if (!ContainedAllFileAB(allDepend[j]) && !allDepend[j].EndsWith(".cs")) {

                        m_VisitedABPath.Add(allDepend[j]);
                        allDependPath.Add(allDepend[j]);
                    }
                }

                if (m_AllPrefabPath.ContainsKey(prefab.name)) {
                    Debug.LogError("存在相同名字的Prefab!名字: " + prefab.name);
                } else {
                    m_AllPrefabPath.Add(prefab.name, allDependPath);
                }
            }
        }

        // 设置文件夹的ab名字
        foreach (string name in m_AllDirABPath.Keys) {
            SetABName(name, m_AllDirABPath[name]);
        }

        // 设置prefab以及prefab依赖项的ab名字
        foreach (string name in m_AllPrefabPath.Keys) {
            SetABName(name, m_AllPrefabPath[name]);
        }

        BuildAssetBundle();

        // 清除ab名字
        string[] oldABNames = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < oldABNames.Length; i++) {
            AssetDatabase.RemoveAssetBundleName(oldABNames[i], true);
            EditorUtility.DisplayProgressBar("清除AB包名", "名字:" + oldABNames[i], i * 1.0f / oldABNames.Length);
        }

        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
    }

    static void SetABName(string name, List<string> paths) {
        for (int i = 0; i < paths.Count; i++) {
            SetABName(name, paths[i]);
        }
    }

    static void SetABName(string name, string path) {
        AssetImporter assetImporter = AssetImporter.GetAtPath(path);
        if (assetImporter == null) {
            Debug.LogError("不存在此路径文件: " + path);
        } else {
            assetImporter.assetBundleName = name;
        }
    }

    static void BuildAssetBundle() {
        // 获取所有AB名
        string[] allABName = AssetDatabase.GetAllAssetBundleNames();

        // key 为全路径, value为ab名
        Dictionary<string, string> resPathDic = new Dictionary<string, string>();


        for (int i = 0; i < allABName.Length; i++) {
            // 获取所有相同ab名的资源的路径
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(allABName[i]);

            for (int j = 0; j < assetPaths.Length; j++) {
                if (assetPaths[j].EndsWith(".cs")) {
                    continue;
                }

                Debug.Log("此AB包: " + allABName[i] + "下面包含的资源文件路径: " + assetPaths[j]);

                if (ValidPath(assetPaths[j])) {
                    resPathDic.Add(assetPaths[j], allABName[i]);
                }
            }
        }

        DeleteUnusedAB();
        // 生成自己的配置表
        WriteData(resPathDic);

        BuildPipeline.BuildAssetBundles(m_BundleTargetPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);

    }

    static void WriteData(Dictionary<string, string> resPathDic) {
        AssetBundleConfig config = new AssetBundleConfig();
        config.ABList = new List<ABBase>();
        foreach (var path in resPathDic.Keys) {
            ABBase abBase = new ABBase();
            abBase.Path = path;
            abBase.Crc = CRC32.GetCRC32(path);
            abBase.ABName = resPathDic[path];
            abBase.AssetName = path.Remove(0, path.LastIndexOf("/") + 1);

            abBase.ABDependency = new List<string>();
            string[] resDependency = AssetDatabase.GetDependencies(path);
            for (int i = 0; i < resDependency.Length; i++) {
                string tempPath = resDependency[i];
                if (tempPath == path || path.EndsWith(".cs")) {
                    continue;
                }

                string abName = "";

                if (resPathDic.TryGetValue(tempPath, out abName)) {
                    if (abName == resPathDic[path]) {
                        continue;
                    }

                    if (!abBase.ABDependency.Contains(abName)) {
                        abBase.ABDependency.Add(abName);
                    }
                }
            }

            config.ABList.Add(abBase);
        }

        // 写入XMl
        string xmlPath = Application.dataPath + "/AssetBundleConfig.xml";
        if (File.Exists(xmlPath)) {
            File.Delete(xmlPath);
        }
        FileStream fileStream = new FileStream(xmlPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        StreamWriter sw = new StreamWriter(fileStream, System.Text.Encoding.UTF8);
        XmlSerializer xs = new XmlSerializer(config.GetType());
        xs.Serialize(sw, config);
        sw.Close();
        fileStream.Close();

        // 写入二进制
        foreach (var abBase in config.ABList) {
            abBase.Path = "";
        }

        string bytePath = "Assets/GameData/Data/ABData/AssetBundleConfig.bytes";
        fileStream = new FileStream(bytePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fileStream, config);
        fileStream.Close();
    }

    // 删除无用AB包
    static void DeleteUnusedAB() {
        // 获取所有AB名
        string[] allABName = AssetDatabase.GetAllAssetBundleNames();

        DirectoryInfo directory = new DirectoryInfo(m_BundleTargetPath);
        FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++) {
            if (ContainedABName(files[i].Name, allABName) || files[i].Name.EndsWith(".meta")) {
                continue;
            } else {
                Debug.Log("此AB包已经被删除或者改名了: " + files[i].Name);
                if (File.Exists(files[i].FullName)) {
                    File.Delete(files[i].FullName);
                }
            }
        }
    }

    // 遍历文件夹里的文件名与设置的所有AB包进行检查判断
    static bool ContainedABName(string name, string[] strs) {
        for (int i = 0; i < strs.Length; i++) {
            if (name == strs[i]) {
                return true;
            }
        }
        return false;
    }

    // 是否包含在已经有的AB包里, 用来做AB包冗余剔除
    static bool ContainedAllFileAB(string path) {
        for (int i = 0; i < m_VisitedABPath.Count; i++) {
            if (path == m_VisitedABPath[i] || (path.Contains(m_VisitedABPath[i]) && (path.Replace(m_VisitedABPath[i], "")[0] == '/'))) {
                return true;
            }
        }

        return false;
    }

    // 是否有效路径
    static bool ValidPath(string path) {
        for (int i = 0; i < m_ConfigFilter.Count; i++) {
            if (path.Contains(m_ConfigFilter[i])) {
                return true;
            }
        }
        return false;
    }
}
