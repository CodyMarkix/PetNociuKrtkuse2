using System.Collections;
using Discord;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;

public class AccountManager : MonoBehaviour
{
    [Header("Scripts")]
    public RPCManager rpcManager;
    public GameObject loadingText;

    [Header("Account display assets")]
    public TMP_FontAsset font;

    GameObject pfp;
    GameObject userNameObj;

    UserManager userManager;

    string userId;
    string userName;
    string avatarUrl;
    string avatarHash;
    byte[] avatar;

    void OnEnable() {
        userManager = rpcManager.discord.GetUserManager();

        pfp = new GameObject();
        pfp.name = "ProfilePicture";
        pfp.SetActive(false);
        Destroy(pfp.GetComponent<Transform>());
        pfp.AddComponent<RectTransform>();
        pfp.AddComponent<CanvasRenderer>();
        pfp.AddComponent<RawImage>();

        userNameObj = new GameObject();
        userNameObj.name = "UserName";
        userNameObj.SetActive(false);
        Destroy(userNameObj.GetComponent<Transform>());
        userNameObj.AddComponent<RectTransform>();
        userNameObj.AddComponent<CanvasRenderer>();
        userNameObj.AddComponent<TMPro.TextMeshPro>();

        userManager.OnCurrentUserUpdate += () => {
            var user = userManager.GetCurrentUser();
            Debug.Log(string.Format("Connected to user {0}", user.Id));
            userId = user.Id.ToString();
            avatarHash = user.Avatar;
            avatarUrl = "https://cdn.discordapp.com/avatars/" + userId + "/" + avatarHash + ".png";
            Debug.Log(string.Format("{0}; {1}", userId, avatarHash));        
            Debug.Log(avatarUrl);

            StartCoroutine(FetchUserAvatar());
        };
    }

    IEnumerator FetchUserAvatar() {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(avatarUrl)) {
            Debug.Log(avatarUrl);
            yield return webRequest.SendWebRequest();

            // Check for errors
            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
            }
            else
            {
                // Process the response
                avatar = webRequest.downloadHandler.data;
            }
        }

        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(avatar);
        pfp.GetComponent<RawImage>().texture = texture;

        pfp.transform.parent = gameObject.transform;
        pfp.GetComponent<RectTransform>().position = new Vector3(10.27643f, 0.8048887f, 110f);
        pfp.GetComponent<RectTransform>().sizeDelta = new Vector2(8f, 8f);

        loadingText.SetActive(false);
        pfp.SetActive(true);

        User user = userManager.GetCurrentUser();
        userName = user.Username;

        userNameObj.GetComponent<TMP_Text>().text = "Hráč: " + userName;
        try {
            userNameObj.GetComponent<TMP_Text>().font = font;
        } catch (NullReferenceException) {
            Debug.Log("It's gonna be fine");
        }
        userNameObj.GetComponent<TMP_Text>().fontSize = 26.58f;
        userNameObj.GetComponent<TMP_Text>().verticalAlignment = VerticalAlignmentOptions.Middle;
        userNameObj.GetComponent<RectTransform>().position = new Vector3(25.2318f, 2.9507f, 110f);
        userNameObj.GetComponent<RectTransform>().sizeDelta = new Vector2(20.2835f, 3.2987f);

        userNameObj.SetActive(true);
    }

}
