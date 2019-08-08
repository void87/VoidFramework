using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace QFramework {

    public class CustomShortCut {

#if UNITY_EDITOR

        [MenuItem("QFramework/1.导出UnityPackage %e", false, -10)]
        private static void MenuClicked() {
            Exporter.ExportPakcage("Assets/QFramework", Exporter.GenerateUnityPackageName() + ".unitypackage");
            EditorUtil.OpenInFolder(Path.Combine(Application.dataPath, "../"));
        }
#endif
    }

}
