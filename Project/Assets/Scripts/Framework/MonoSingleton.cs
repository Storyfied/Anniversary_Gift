using UnityEngine;


public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T m_instance = null;

	public static T Instance
	{
		get
		{
			// Instance required for the first time, we look for it
			if (m_instance == null)
			{
                m_instance = (T)GameObject.FindObjectOfType(typeof(T));
				
				if (m_instance != null)
                {
					m_instance.InitInstance();
					DontDestroyOnLoad(m_instance);
                }
				else
				{
                    Debug.LogError("Missing MonoSingleton: " + typeof(T).ToString());
				}
			}

			return m_instance;
		}
	}

	public static bool IsInitialized
	{
		get { return m_instance != null; }
	}

	// If no other monobehaviour request the instance in an awake function
	// executing before this one, no need to search the object.
	private void Awake()
	{
		if (m_instance == null) 
		{
			m_instance = this as T;
            m_instance.InitInstance();
			DontDestroyOnLoad(this);
		}
		else if (m_instance.gameObject != this.gameObject)
		{
			DestroyImmediate(this.gameObject);
			Debug.Log("Destroyed duplicated MonoSingleton: " + typeof(T).ToString());
		}
	}

	protected virtual void InitInstance()
	{ }

	public virtual void Reset()
	{ }
}