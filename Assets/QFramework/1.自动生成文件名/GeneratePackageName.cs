using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace QFramework {

    public class GeneratePackageName {

#if UNITY_EDITOR
        [MenuItem("QFramework/1.生成 UnityPackage 名字")]
#endif
        static void MenuClicked() {
            Debug.Log("QFramework_" + DateTime.Now.ToString("yyyyMMdd_HH"));
        }
    }

}
