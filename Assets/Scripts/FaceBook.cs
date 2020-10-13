using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;


public class FaceBook : MonoBehaviour
{

    public GameObject LoggedInUI;
    public GameObject NotLoggedInUI;
    public GameObject Friend;
    //public GameObject FaceBookScreenloggedin;

    

    void Awake()
    {
        if(!FB.IsInitialized)
        {
            FB.Init(InitCallBack);
        }
        ShowUi();
    }


    void InitCallBack()
    {
        Debug.Log("FB has been started");
    }


    public void Login()
    {
        if (!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(new List<string> { "user_friends" }, LoginCallBack);
        }
    }

    void LoginCallBack(ILoginResult result)
    {
        if (result.Error == null)
        {
            Debug.Log("FB has logged in.");
            ShowUi();
        }
        else
        {
            Debug.Log("Error during Login:" + result.Error);
        }
    }


    void ShowUi()
    {
        if (FB.IsLoggedIn)
        {
            LoggedInUI.SetActive(true);
            NotLoggedInUI.SetActive(false);
            FB.API("me/picture?width=100&height=100", HttpMethod.GET, PictureCallBack);
            FB.API("me?fields=first_name", HttpMethod.GET, NameCallBack);
            FB.API("me/friends", HttpMethod.GET, FriendCallBack);
        }
        else
        {
            LoggedInUI.SetActive(false);
            NotLoggedInUI.SetActive(true);
        }
    }

    void PictureCallBack(IGraphResult result)
    {
        Texture2D image = result.Texture;
        LoggedInUI.transform.Find("ProfilePicture").GetComponent<Image> ().sprite = Sprite.Create(image, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
    }

    void NameCallBack(IGraphResult result)
    {
        //Debug.Log(result.RawResult);
        IDictionary<string, object> profile = result.ResultDictionary;
       LoggedInUI.transform.Find("Name").GetComponent <Text>().text = "Hello " + profile["first_name"];
    }

    public void LogOut()
    {
        FB.LogOut();
        ShowUi();
    }


    public void Share()
    {
       
        //AW set to proper page
        FB.ShareLink(new System.Uri("http://myapplocation.co.za"), "This game is awesome", "A description of the game", new System.Uri("http://myapplication.co.za/Images/logo.png"));
    }

    public void Invite()
    {
        FB.AppRequest( message: "You should really try this game.", title: "Checkout this super game");
    }

    void FriendCallBack(IGraphResult result)
    {
        // Debug.Log(result.RawResult);
        IDictionary<string, object> data = result.ResultDictionary;
        List<object> friends = (List<object>)data["data"];
        foreach (object obj in friends)
        {
            Dictionary<string, object> dictio = (Dictionary<string, object>)obj;
            Debug.Log(dictio["name"].ToString() + "," + dictio["id"].ToString());
            CreateFriend(dictio ["name"].ToString (), dictio ["id"].ToString ());
        }
    }

    void CreateFriend(string name, string id)
    {
        GameObject myFriend = Instantiate (Friend);
        Transform parent = LoggedInUI.transform.Find ("ListContainer").Find ("FriendList");
        myFriend.transform.SetParent (parent);
        myFriend.GetComponentInChildren<Text> ().text = name;
        FB.API(id + "/picture?width=100&height=100", HttpMethod.GET, delegate (IGraphResult result)
        {
            myFriend.GetComponentInChildren<Image>().sprite = Sprite.Create(result.Texture, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
        });

    }
}
