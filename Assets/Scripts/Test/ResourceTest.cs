using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class ResourceTest : MonoBehaviour
{

    void Start() {

        // SerializeTest();
        // DeSerializeTest();
        // BinarySerializeTest();
        //BinaryDeserializeTest();
        //ReadTestAssets();
        TestLoadAB();
    }

    void TestLoadAB() {
        //TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(Application.streamingAssetsPath + "/AssetBundleConfig.bytes");

        AssetBundle configAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/assetbundleconfig");
        TextAsset textAsset = configAB.LoadAsset<TextAsset>("AssetBundleConfig");

        MemoryStream stream = new MemoryStream(textAsset.bytes);
        BinaryFormatter bf = new BinaryFormatter();
        AssetBundleConfig assetBundleConfig = (AssetBundleConfig)bf.Deserialize(stream);
        stream.Close();

        string path = "Assets/GameData/Prefabs/Attack.prefab";
        uint crc = CRC32.GetCRC32(path);

        ABBase abBase = null;
        for (int i = 0; i < assetBundleConfig.ABList.Count; i++) {
            if (assetBundleConfig.ABList[i].Crc == crc) {
                abBase = assetBundleConfig.ABList[i];
            }
        }

        for (int i = 0; i < abBase.ABDependency.Count; i++) {
            AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + abBase.ABDependency[i]);
        }
        AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + abBase.ABName);
        GameObject obj = GameObject.Instantiate(assetBundle.LoadAsset<GameObject>(abBase.AssetName));
    }

    //void ReadTestAssets() {
    //    AssetSerialize assets = UnityEditor.AssetDatabase.LoadAssetAtPath<AssetSerialize>("Assets/Scripts/TestAssets.asset");
    //    Debug.Log(assets.Id);
    //    Debug.Log(assets.Name);
    //    foreach (string str in assets.TestList) {
    //        Debug.Log(str);
    //    }
    //}

    //void SerializeTest() {
    //    TestSerialize testSerialize = new TestSerialize();
    //    testSerialize.Id = 1;
    //    testSerialize.Name = "测试";
    //    testSerialize.List = new List<int>();
    //    testSerialize.List.Add(2);
    //    testSerialize.List.Add(3);
    //    XmlSerialize(testSerialize);
    //}

    //void DeSerializeTest() {
    //    TestSerialize testSerialize = XmlDeSerialize();
    //    Debug.Log(testSerialize.Id + " " + testSerialize.Name);
    //    foreach (int a in testSerialize.List) {
    //        Debug.Log(a);
    //    }
    //}

    //void XmlSerialize(TestSerialize testSerialize) {
    //    FileStream fileStream = new FileStream(Application.dataPath + "/test.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
    //    StreamWriter sw = new StreamWriter(fileStream, System.Text.Encoding.UTF8);
    //    XmlSerializer xml = new XmlSerializer(testSerialize.GetType());
    //    xml.Serialize(sw, testSerialize);
    //    sw.Close();
    //    fileStream.Close();
    //}

    //TestSerialize XmlDeSerialize() {
    //    FileStream fs = new FileStream(Application.dataPath + "/test.xml", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
    //    XmlSerializer xs = new XmlSerializer(typeof(TestSerialize));
    //    TestSerialize testSerialize = (TestSerialize)xs.Deserialize(fs);
    //    fs.Close();
    //    return testSerialize;
    //}

    //void BinarySerializeTest() {
    //    TestSerialize testSerialize = new TestSerialize();
    //    testSerialize.Id = 2;
    //    testSerialize.Name = "二进制测试";
    //    testSerialize.List = new List<int>();
    //    testSerialize.List.Add(10);
    //    testSerialize.List.Add(18);
    //    BinarySerialize(testSerialize);

    //}

    //void BinaryDeserializeTest() {
    //    TestSerialize testSerialize = BinaryDeserialize();
    //    Debug.Log(testSerialize.Id + " " + testSerialize.Name);
    //    foreach (int a in testSerialize.List) {
    //        Debug.Log(a);
    //    }
    //}

    //void BinarySerialize(TestSerialize serialize) {
    //    FileStream fs = new FileStream(Application.dataPath + "/test.bytes", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
    //    BinaryFormatter bf = new BinaryFormatter();
    //    bf.Serialize(fs, serialize);
    //    fs.Close();
    //}

    //TestSerialize BinaryDeserialize() {
    //    TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/test.bytes");
    //    MemoryStream stream = new MemoryStream(textAsset.bytes);
    //    BinaryFormatter bf = new BinaryFormatter();
    //    TestSerialize testSerialize = (TestSerialize)bf.Deserialize(stream);
    //    stream.Close();
    //    return testSerialize;
    //}
}
