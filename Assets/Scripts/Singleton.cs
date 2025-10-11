using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance => _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            if (_instance == null)
            {
                _instance = this.GetComponent<T>();
            }
            else if(instance.GetInstanceID() != this.GetInstanceID())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
