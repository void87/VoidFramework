using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QFramework {

    public class MathUtil {


        public static bool Percent(int percent) {
            return Random.Range(0, 100) <= percent;
        }
    }

    public class PercentFunction {

#if UNITY_EDITOR

        [MenuItem("QFramework/12.概率函数")]
        static void MenuClicked() {
            Debug.Log(MathUtil.Percent(50));
        }

#endif

    }
}
