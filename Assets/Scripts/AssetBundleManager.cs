using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Test20190807 {

    public class AssetBundleManager : Singleton<AssetBundleManager> {

        // 资源关系依赖表,可以根据crc来找到对应资源块
        protected Dictionary<uint, ResourceItem> m_ResourceItemDict = new Dictionary<uint, ResourceItem>();

        /// <summary>
        /// 加载AB配置表
        /// </summary>
        /// <returns></returns>
        public bool LoadAssetBundleConfig() {
            m_ResourceItemDict.Clear();


            string configPath = Application.streamingAssetsPath + "/assetbundleconfig";
            AssetBundle configAB = AssetBundle.LoadFromFile(configPath);
            TextAsset textAsset = configAB.LoadAsset<TextAsset>("assetbundleconfig");
            if (textAsset == null) {
                Debug.LogError("AssetBundleConfig is no exist!");
                return false;
            }

            MemoryStream stream = new MemoryStream(textAsset.bytes);

            BinaryFormatter bf = new BinaryFormatter();
            AssetBundleConfig config = (AssetBundleConfig)bf.Deserialize(stream);
            stream.Close();

            for (int i = 0; i < config.ABList.Count; i++) {
                ABBase abBase = config.ABList[i];

                ResourceItem item = new ResourceItem();
                item.m_Crc = abBase.Crc;
                item.m_AssetName = abBase.AssetName;
                item.m_ABName = abBase.ABName;
                item.m_DependABName = abBase.ABDependency;

                if (m_ResourceItemDict.ContainsKey(item.m_Crc)) {
                    Debug.LogError("重复的Crc 资源名: " + item.m_AssetName + " ab包名: " + item.m_ABName);
                } else {
                    m_ResourceItemDict.Add(item.m_Crc, item);
                }

            }


            return true;
        }

        //public ResourceItem LoadResourceAssetBundle(uint crc) {

        //}
    }

    public class ResourceItem {
        public uint m_Crc = 0;
        public string m_AssetName = "";
        public string m_ABName = "";
        public List<string> m_DependABName = null;
        public AssetBundle m_AssetBundle = null;
    }
}
