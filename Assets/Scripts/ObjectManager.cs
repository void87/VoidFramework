﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Test20190807 {

    public class ObjectManager : Singleton<ObjectManager> {

        protected Dictionary<Type, object> m_ClassPoolDic = new Dictionary<Type, object>();

        /// <summary>
        /// 创建类对象池,创建完成后外面可以保存ClassObjectPool<T>,然后调用Spawn和Recycle来创建和回收类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maxcount"></param>
        /// <returns></returns>
        public ClassObjectPool<T> GetOrCreateClassPool<T>(int maxcount) where T : class, new() {
            Type type = typeof(T);
            object outObj = null;
            if (!m_ClassPoolDic.TryGetValue(type, out outObj) || outObj == null) {
                ClassObjectPool<T> newPool = new ClassObjectPool<T>(maxcount);
                m_ClassPoolDic.Add(type, newPool);
                return newPool;
            }

            return outObj as ClassObjectPool<T>;
        }
    }
}
