using UnityEngine;

namespace QFramework
{
    public class GameObjectSimplifyExample
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/6.GameObject API 简化", false, 6)]
#endif
        private static void MenuClicked()
        {
            GameObject gameObject = new GameObject();

            GameObjectSimplify.Hide(gameObject);

            GameObjectSimplify.Show(gameObject.transform);

        }
    }
}