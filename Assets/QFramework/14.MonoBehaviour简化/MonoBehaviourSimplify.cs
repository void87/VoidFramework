﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework {

    public partial class MonoBehaviourSimplify : MonoBehaviour {
        
        public void Show() {
            GameObjectSimplify.Show(gameObject);
        }

        public void Hide() {
            GameObjectSimplify.Hide(gameObject);
        }

        public void Identity() {
            TransformSimplify.Identity(transform);
        }

    }

    public class Hide: MonoBehaviourSimplify {
        private void Awake() {
            Hide();
        }



#if UNITY_EDITOR

        [UnityEditor.MenuItem("QFramework/14. MonoBehaviour 简化", false ,10)]
        static void MenuClicked() {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject().AddComponent<Hide>();
            UnityEditor.EditorApplication.isPlaying = false;
        }

        protected override void OnBeforeDestroy() {
            
        }
#endif

    }
}


