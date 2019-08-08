using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace QFramework {

    public class ExportUnityPackage {

#if UNITY_EDITOR
        [MenuItem("QFramework/4.导出UnityPackage")]
        private static void MenuClicked() {
            var assetPathName = "Assets/QFramework";
            var fileName = "QFramework_" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
            AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
        }
#endif
    }

}
