using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QFramework {

    public class TransformIdentity {

#if UNITY_EDITOR

        [MenuItem("QFramework/11. Transform重置")]
        static void MenuClicked() {
            GameObject gameObject = new GameObject();
            Identity(gameObject.transform);
        }

#endif

        public static void Identity(Transform transform) {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
    }
}
