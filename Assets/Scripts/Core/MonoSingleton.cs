using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static bool bInit = false;
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }

                if (!bInit)
                {
                    bInit = true;
                    _instance.Init();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            DestroySingleton();
            return;
        }

        if (!bInit)
        {
            DontDestroyOnLoad(gameObject);
            bInit = true;
            _instance.Init();
        }
    }

    public virtual void Init() { }

    private void DestroySingleton()
    {
        Debug.Log($"{gameObject.GetComponents<Component>().Length}");
        if (gameObject.GetComponents<Component>().Length <= 2)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private void OnApplicationQuit()
    {
        _instance = null;
    }
}
