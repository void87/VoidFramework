using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test20190807 {
    public class GameStart : MonoBehaviour {
        void Awake() {
            AssetBundleManager.Instance.LoadAssetBundleConfig();    
        }
    }

}
