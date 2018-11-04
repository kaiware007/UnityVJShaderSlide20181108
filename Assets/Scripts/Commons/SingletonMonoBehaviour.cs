using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance = null;

    //public static bool hasInstance { get { return instance != null; } }

    private void Awake()
    {
        instance = (T)FindObjectOfType(typeof(T));
    }

    public static T Instance {
		get {
			if (instance == null) {
				instance = (T)FindObjectOfType(typeof(T));
				
				if (instance == null) {
					Debug.LogError (typeof(T) + " is nothing");
				}
			}
			
			return instance;
		}
	}

    static bool first = true;
    public static T GetInstance()
    {
        if (first && instance == null){
            first = false;
            instance = (T)FindObjectOfType(typeof(T));
        }
        return instance;
    }
	
}