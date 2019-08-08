using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QFramework {

    public abstract partial class MonoBehaviourSimplify : MonoBehaviour {

        List<MsgRecord> mMsgRecorder = new List<MsgRecord>();

        class MsgRecord {
            private MsgRecord() {

            }

            static Stack<MsgRecord> mMsgRecordPool = new Stack<MsgRecord>();

            public static MsgRecord Allocate(string msgName, Action<object> onMsgReceived) {

                var retRecord = mMsgRecordPool.Count > 0 ? mMsgRecordPool.Pop() : new MsgRecord();
                retRecord.Name = msgName;
                retRecord.OnMsgReceived = onMsgReceived;

                return retRecord;
            }

            public void Recycle() {
                Name = null;
                OnMsgReceived = null;

                mMsgRecordPool.Push(this);
            }

            public string Name;
            public Action<object> OnMsgReceived;
        }


        public void RegisterMsg(string msgName, Action<object> onMsgReceived) {
            MsgDispatcher.Register(msgName, onMsgReceived);
            mMsgRecorder.Add(MsgRecord.Allocate(msgName, onMsgReceived));
        }

        void OnDestroy() {
            OnBeforeDestroy();

            foreach (var msgRecord in mMsgRecorder) {
                MsgDispatcher.UnRegister(msgRecord.Name, msgRecord.OnMsgReceived);
                msgRecord.Recycle();
            }

            mMsgRecorder.Clear();
        }

        protected abstract void OnBeforeDestroy();
    }

    public class B: MonoBehaviourSimplify {

        void Start() {
            
        }

        void Update() {
            
        }

        protected override void OnBeforeDestroy() {
            
        }
    }

}
