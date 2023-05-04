using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Common
{
    /// <summary>
    /// Dont destroy on Load 版本的 monosingleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NoDestroyMonoSingleton<T> : MonoBehaviour where T: NoDestroyMonoSingleton<T>
    {
        

        private static T instance; 

        public static T Instance
        {

            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<T>();

                if (instance == null)
                {

                    new GameObject("Singleton of "+typeof(T)).AddComponent<T>();
                }
                else instance.Init(); 

                return instance;

            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (instance == null)
            {
                instance = this as T;
                Init();
            }
            else
            {
                Destroy(gameObject);
            }
           
            
        }

        protected virtual void Init()
        {

        }
    }
}