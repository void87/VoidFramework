using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test20190807 {
    [CreateAssetMenu(fileName = "TestAssets", menuName = "CreateAssets", order = 0)]
    public class AssetSerialize : ScriptableObject {
        public int Id;
        public string Name;
        public List<string> TestList;
    }
}

