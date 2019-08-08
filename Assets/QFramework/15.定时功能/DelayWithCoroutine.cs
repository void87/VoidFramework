using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QFramework {

    public partial class MonoBehaviourSimplify : MonoBehaviour {

        public void Delay(float seconds, Action onFinished) {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private IEnumerator DelayCoroutine(float seconds, Action onFinished) {
            yield return new WaitForSeconds(seconds);
            onFinished();
        }

    }

    public class DelayWithCoroutine: MonoBehaviourSimplify {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/15.定时功能")]
        static void MenuClicked() {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject().AddComponent<DelayWithCoroutine>();
        }

        protected override void OnBeforeDestroy() {
            
        }
#endif

        void Start() {
            Delay(5.0f, () => {
                Debug.Log("5s 之后");
            });  
        }
    }
}


