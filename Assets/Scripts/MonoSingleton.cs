using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();

                    var mono = _instance as MonoSingleton<T>;

                    if (mono != null && mono.isDontDestroy)
                    {
                        DontDestroyOnLoad(obj);
                    }
                }
                else
                {
                    var mono = _instance as MonoSingleton<T>;

                    if (mono != null && mono.isDontDestroy)
                    {
                        DontDestroyOnLoad(_instance);
                    }
                }
            }
            return _instance;
        }
    }

    [SerializeField] protected bool isDontDestroy = false;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if (isDontDestroy)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}