using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideManager : MonoBehaviour {

    [SerializeField]
    List<RectTransform> pages = new List<RectTransform>();

    public int pageNo = 0;
    public int pageMax = 0;

    void ChangePage(int no)
    {
        //Debug.Log("ChangePage " + no);

        for(int i = 0; i < pageMax; i++)
        {
            pages[i].gameObject.SetActive((no == i));
        }
    }

	// Use this for initialization
	void Start () {
        pageNo = 0;
        pageMax = pages.Count;

        ChangePage(pageNo);
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            pageNo = Mathf.Min(pageNo+1, pageMax - 1);
            ChangePage(pageNo);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pageNo = Mathf.Max(pageNo-1, 0);
            ChangePage(pageNo);
        }
	}
}
