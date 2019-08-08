using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace QFramework {

    public class ReuseMenuItem {

#if UNITY_EDITOR
        [MenuItem("QFramework/6.MenuItem复用")]
        private static void MenuClicked() {
            EditorApplication.ExecuteMenuItem("QFramework/4.导出UnityPackage");
            Application.OpenURL("file://" + Path.Combine(Application.dataPath, "../"));
        }
#endif
    }

}
