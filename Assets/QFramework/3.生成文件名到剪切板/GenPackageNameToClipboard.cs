using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace QFramework {

    public class GenPackageNameToClipboard {

#if UNITY_EDITOR
        [MenuItem("QFramework/3.生成文件名到剪切板")]
#endif
        private static void MenuClicked() {
            GUIUtility.systemCopyBuffer = "QFramework_" + DateTime.Now.ToString("yyyyMMdd_HH");
        }
    }
}


