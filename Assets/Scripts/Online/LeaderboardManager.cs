using System.Collections;
using System.Net.NetworkInformation;
using LightHTTP;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [System.NonSerialized]
    public bool offlineMode = false;
    [System.NonSerialized]
    public bool signedIn = false;
    public string DiscordOauth2Link;

    LightHttpServer server = new LightHttpServer();

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        // if (NetworkInterface.GetIsNetworkAvailable()) {
        //     StartCoroutine(StartWebServer());

        //     if (DiscordOauth2Link != null) {
        //         System.Diagnostics.Process.Start(DiscordOauth2Link);
        //     }
        // } else {
        //     offlineMode = true;
        // }
    }

    IEnumerator StartWebServer() {
        server.HandlesPath("/callback", context => {
            string resposneCode = context.Request.QueryString.Get("code");
            
        });
        
        return null;
    }
}
