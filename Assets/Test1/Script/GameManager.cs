using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Test1 {

    public class GameManager : MonoBehaviour {

        private List<Hero> heroList = new List<Hero>();

        private void Awake() {
            Debug.Log("GameManager Awake");

            EventManager.Instance.StartListening("Test1",Test1Function);
            EventManager.Instance.StartListening("Test1", Test1FunctionPlus);

            EventManager.Instance.StartListening("DestroyHero", DestroyHero);

            for (var i = 0; i < 10; i++) {
                Hero hero = new Hero();
                heroList.Add(hero);
            }
        }

        private void Test1Function(object param) {
            Debug.Log("Test1Function:" + param);
        }

        private void Test1FunctionPlus(object param) {
            Debug.Log("Test1FunctionPlus:" + param);
        }

        private void DestroyHero(object param) {
            foreach (var i in heroList) {
                i.Destroy();
            }
        }
    }
}


