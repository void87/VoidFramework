using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test1 {

    public class UITest : MonoBehaviour {

        private Button btn;
        private Button btn1;
        private Button btn2;
        private Button btn3;

        void Start() {
            btn = GameObject.Find("Button").GetComponent<Button>();
            btn1 = GameObject.Find("Button1").GetComponent<Button>();
            btn2 = GameObject.Find("Button2").GetComponent<Button>();
            btn3 = GameObject.Find("Button3").GetComponent<Button>();

            btn.onClick.AddListener(() => {
                Debug.Log("哈哈哈");
                EventManager.Instance.TriggerEvent("Test1", "1");
            });

            btn1.onClick.AddListener(() => {
                Debug.Log("嘿嘿");
                EventManager.Instance.StopListeningAll("Test1");
            });

            btn2.onClick.AddListener(() => {
                Debug.Log("Hoho");
                EventManager.Instance.TriggerEvent("Test2","");
            });

            btn3.onClick.AddListener(() => {
                Debug.Log("Destroy");
                EventManager.Instance.TriggerEvent("DestroyHero", "");
            });
        }

        // Update is called once per frame
        void Update() {

        }
    }
}

