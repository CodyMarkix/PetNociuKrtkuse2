using System;
using System.Collections;
using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPCManager : MonoBehaviour {
    public GameTime gameTimeScript;

    [System.NonSerialized]
    internal long clientId = 809031581179052082;

    public Discord.Discord discord;

    Discord.Activity activity;
    private Discord.ActivityManager activityManager;

    int currentScene;
    string hour = "";
    string details = "";
    string state = "";

    void Start() {
        discord = new Discord.Discord(clientId, (UInt64)Discord.CreateFlags.Default);
        activityManager = discord.GetActivityManager();

        currentScene = SceneManager.GetActiveScene().buildIndex;
        OnSceneChange(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex - 1), SceneManager.GetActiveScene());
        
        activityManager.UpdateActivity(activity, (result) => {
            if (result == Discord.Result.Ok) {
                Debug.Log("Updated Discord rich presence");
            } else {
                Debug.Log("Something went wrong while updating!");
            }
        });

        SceneManager.activeSceneChanged += OnSceneChange;

        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update() {
        discord.RunCallbacks();
    }

    void LateUpdate() {
        Discord.Activity activity = new Discord.Activity {
            State = state,
            Details = details,
            Assets = {
                LargeImage = "logo",
                LargeText = "the almighty"
            },
            Instance = true
        };

        activityManager.UpdateActivity(activity, (result) => {
            if (result == Discord.Result.Ok) {
                // Debug.Log("Updated Discord rich presence");
            } else {
                // Debug.Log("Something went wrong while updating!");
            }
        });
    }

    void OnSceneChange(Scene current, Scene next) {
        currentScene = next.buildIndex;
        Debug.Log(currentScene);

        if (currentScene >= 2 && currentScene <= 8) {
            gameTimeScript = GameObject.Find("Clock").GetComponent<GameTime>();
        }

        if (currentScene == 1) {
            details = "In Main Menu";
            state = "";
        } else if (currentScene >= 2 & currentScene <= 8) {
            Debug.Log("balls");
            details = "Surviving the night";
            hour = GameTime.ConvertTimeToHours(gameTimeScript.time);
            state = hour;
            Debug.Log(string.Format("Details: {0}, Hour: {1}, State: {2}", details, hour, state));
        }

        activityManager.UpdateActivity(activity, (result) => {
            if (result == Discord.Result.Ok) {
                Debug.Log("Updated Discord rich presence");
            } else {
                Debug.Log("Something went wrong while updating!");
            }
        });

    }

    void OnApplicationQuit() {
        activityManager.ClearActivity((result) => {
            if (result == Discord.Result.Ok) {
                Console.WriteLine("[OK] Cleared Discord RPC");
            } else {
                Console.WriteLine("[ERROR] Could not clear Discord RPC");
            }
        });
    }
}
