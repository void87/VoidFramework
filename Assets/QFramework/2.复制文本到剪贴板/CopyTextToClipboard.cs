using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace QFramework {
    public class CopyTextToClipboard {
#if UNITY_EDITOR
        [MenuItem("QFramework/2.复制文本到剪贴板")]
#endif
        private static void MenuClicked() {
            GUIUtility.systemCopyBuffer = "复制的文本";
        }
    }
}


