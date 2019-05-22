using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private static DontDestroy _instance;

    public void OnEnable()
     {
            _instance = this;
            DontDestroyOnLoad(gameObject);
    }       

        public void Awake()
        {
            if (_instance) DestroyImmediate(gameObject);
        }    
    
}
