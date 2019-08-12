using System;
using UnityEngine;

namespace QFramework
{
    public class NewClass
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/4.Transform API 简化", false, 4)]
#endif
        static void MenuClicked()
        {
            GameObject gameObject = new GameObject();

            TransformSimplify.SetLocalPosX(gameObject.transform, 5.0f);
            TransformSimplify.SetLocalPosY(gameObject.transform, 5.0f);
            TransformSimplify.SetLocalPosZ(gameObject.transform, 5.0f);

            TransformSimplify.Identity(gameObject.transform);

            var parentTrans = new GameObject("ParentTransform").transform;
            var childTrans = new GameObject("ChildTransform").transform;

            TransformSimplify.AddChild(parentTrans, childTrans);
        }
    }
}
