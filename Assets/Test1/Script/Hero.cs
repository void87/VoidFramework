using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test1 {
    public class Hero {

        private Transform transform;

        public Hero() {
            Init();
        }

        public void Init() {
            transform = GameObject.Instantiate(Resources.Load<GameObject>("Hero")).transform;

            Debug.Log(transform);
            EventManager.Instance.StartListening("Test2", Test2);

            //transform.SetParent(GameObject.roo)
        }

        public void Destroy() {
            Debug.Log("Hero Destroy");
            EventManager.Instance.StopListening("Test2", Test2);
        }

        private void Test2(object param) {
            //
            Debug.Log(param);
        }
    }
}


