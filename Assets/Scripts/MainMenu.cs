using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject FaceBookScreen;
    public GameObject SpinnerScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenFaceBook()
    {
        
        FaceBookScreen.SetActive(true);
    }

    public void CloseFaceBook()
    {
        
        FaceBookScreen.SetActive(false);
    }

    
    public void StartRun()
    {
        
        GameManager.Instance.StartRun();
    }

    public void OpenSpinner()
    {

        SpinnerScreen.SetActive(true);
    }
    
    public void CloseSpinner()
    {

        SpinnerScreen.SetActive(false);
    }
}
