using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCounter : MonoBehaviour
{
    public int counter;
    //public string favoriteColour;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        //Debug.Log("OnClick called");

        // Increase the click counter by 1
        counter = counter + 1;
        //counter += 1;
        //counter++;
    }
}
