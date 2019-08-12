﻿using UnityEngine;

namespace QFramework
{
    public class DelayWithCoroutine : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/8.定时功能", false, 8)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject().AddComponent<DelayWithCoroutine>();
        }

        protected override void OnBeforeDestroy()
        {
        }
#endif

        void Start()
        {
            Delay(5.0f, () =>
            {
                Debug.Log(" 5 s 之后");
                Hide();
            });
        }
    }
}