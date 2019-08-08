using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QFramework {

    public class MsgDispatcher {

        private static Dictionary<string, Action<object>> mRegisteredMsgs = new Dictionary<string, Action<object>>();

        public static void Register(string msgName, Action<object> onMsgReceived) {
            if (!mRegisteredMsgs.ContainsKey(msgName)) {
                mRegisteredMsgs.Add(msgName, onMsgReceived);
            } else {
                mRegisteredMsgs[msgName] += onMsgReceived;
            }
            
        }

        public static void UnRegisterAll(string msgName) {
            mRegisteredMsgs.Remove(msgName);
        }

        public static void UnRegister(string msgName, Action<object> onMsgReceived) {
            if (mRegisteredMsgs.ContainsKey(msgName)) {
                mRegisteredMsgs[msgName] -= onMsgReceived;
            }
        }

        public static void Send(string msgName, object data) {
            if (mRegisteredMsgs.ContainsKey(msgName)) {
                mRegisteredMsgs[msgName](data);
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/15.简易消息机制")]
        static void MenuClicked() {

            Register("消息1", OnMsgReceived);
            Register("消息1", OnMsgReceived);

            Send("消息1", "Hello World");

            UnRegister("消息1", OnMsgReceived);
            UnRegister("消息1", OnMsgReceived);

            Send("消息1", "Hello");
        }
#endif
        static void OnMsgReceived(object data) {
            Debug.LogFormat("消息1:{0}", data);
        }
    }
}
