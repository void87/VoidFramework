using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QFramework {

    public class GameObjectSimplify {

        public static void Show(GameObject gameObj) {
            gameObj.SetActive(true);
        }

        public static void Hide(GameObject gameObj) {
            gameObj.SetActive(false);
        }
    }

    public class GameObjectActiveImprovements {

#if UNITY_EDITOR

        [MenuItem("QFramework/13.GameObject的显示,隐藏简化")]
        static void MenuClicked() {
            var gameObj = new GameObject();
            GameObjectSimplify.Hide(gameObj);
        }

#endif
    }
}
