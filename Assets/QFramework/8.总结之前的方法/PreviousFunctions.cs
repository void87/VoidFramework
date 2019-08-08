using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace QFramework {

    public class PreviousFunctions {

        public static string GenerateUnityPackageName() {
            return "QFramework_" + DateTime.Now.ToString("yyyyMMdd_hh");
        }

        public static void CopyText(string text) {
            GUIUtility.systemCopyBuffer = text;
        }

        public static void ExportPakcage(string assetPathName, string fileName) {
            AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
        }

        public static void OpenInFolder(string folderPath) {
            Application.OpenURL("file:///" + folderPath);
        }

        public static void CallMenuItem(string menuName) {
            EditorApplication.ExecuteMenuItem("QFramework/4.导出UnityPackage");
            Application.OpenURL("file://" + Path.Combine(Application.dataPath, "../"));
        }

#if UNITY_EDITOR

        [MenuItem("QFramework/8.总结之前的方法/1.获取文件名")]
        private static void MenuClicked() {
            Debug.Log(GenerateUnityPackageName());
        }

        [MenuItem("QFramework/8.总结之前的方法/2.复制文本到剪切板")]
        private static void MenuClicked2() {
            CopyText("要复制的文本");
        }

        [MenuItem("QFramework/8.总结之前的方法/3.生成文件名到剪切板")]
        private static void MenuClicked3() {
            CopyText(GenerateUnityPackageName());
        }

        [MenuItem("QFramework/8.总结之前的方法/4.导出UnityPackage")]
        private static void MenuClicked4() {
            ExportPakcage("Assets/QFramework", GenerateUnityPackageName() + ".unitypackage");
        }

        [MenuItem("QFramework/8.总结之前的方法/5.打开所在的文件夹")]
        private static void MenuClicked5() {
            OpenInFolder(Application.dataPath);
        }

        [MenuItem("QFramework/8.总结之前的方法/6. MenuItem 复用")]
        private static void MenuClicked6() {
            CallMenuItem("QFramework/4.导出UnityPackage");
        }

        [MenuItem("QFramework/8.总结之前的方法/7.自定义快捷键")]
        private static void MenuClicked7() {
            Debug.Log("%e ctrl/cmd + e");
        }
#endif

    }

}
