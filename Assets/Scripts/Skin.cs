using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    // Start is called before the first frame update
    public bool visible = false;
    public string skinName;
    public Animator skinAnimator;

    void Start()
    {
        
    }

    public void ShowSkin()
    {
        gameObject.SetActive(true);
        visible = true;
    }


    public void HideSkin()
    {
        gameObject.SetActive(false);
        visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
