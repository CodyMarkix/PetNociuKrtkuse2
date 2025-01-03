using System;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPCManager : MonoBehaviour {
    public GameTime gameTimeScript;

    [System.NonSerialized]
    internal long clientId = 809031581179052082;

    public Discord.Discord discord;
    private Discord.ActivityManager activityManager;

    public bool isDiscordRunning;

    int currentScene;
    string hour = "";
    string details = "";
    string state = "";

    long beginningUnix;

    void Start() {
        beginningUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (Process.GetProcessesByName("Discord").Length > 0) {
            discord = new Discord.Discord(clientId, (UInt64)Discord.CreateFlags.NoRequireDiscord);
            activityManager = discord.GetActivityManager();
            isDiscordRunning = true;
        } else {
            isDiscordRunning = false;
        }


        SceneManager.activeSceneChanged += OnSceneChange;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (Process.GetProcessesByName("Discord").Length > 0) {
            isDiscordRunning = true;
        } else {
            isDiscordRunning = false;
        }

        if (isDiscordRunning) {
            discord.RunCallbacks();
        }
    }

    void LateUpdate() {
        UpdateActivity(currentScene);
    }

    void OnSceneChange(Scene current, Scene next) {
        currentScene = next.buildIndex;

        UpdateGameScriptReference();
        UpdateActivity(currentScene);
    }

    void OnApplicationQuit() {
        if (isDiscordRunning) {
            activityManager.ClearActivity((result) => {
                if (result == Discord.Result.Ok) {
                    Console.WriteLine("[OK] Cleared Discord RPC");
                } else {
                    Console.WriteLine("[ERROR] Could not clear Discord RPC");
                }
            });
        }
        discord.Dispose();
    }

    void UpdateActivity(int sceneId) {
        if (isDiscordRunning) {
            Discord.Activity activity;
            
            if (sceneId == 1) {
                activity = new Discord.Activity {
                    Details = "Browsing the Main Menu",
                    Assets = {
                        LargeImage = "logo",
                        LargeText = "the almighty"
                    },
                    Timestamps = {
                        Start = beginningUnix
                    }
                };
            } else if (sceneId >= 2 && 8 >= sceneId) {
                activity = new Discord.Activity {
                    Details = "Surviving the Night",
                    State = string.Format("Night: {0} ({1})", sceneId - 1, GameTime.ConvertTimeToHours(gameTimeScript.time, 86)),
                    Assets = {
                        LargeImage = "logo",
                        LargeText = "the almighty"
                    },
                    Timestamps = {
                        Start = beginningUnix
                    }
                };
            } else if (sceneId == 10) {
                activity = new Discord.Activity {
                    Details = "Making bank",
                    Assets = {
                        LargeImage = "logo",
                        LargeText = "the almighty"
                    },
                    Timestamps = {
                        Start = beginningUnix
                    }
                };
            } else {
                activity = new Discord.Activity {
                    Assets = {
                        LargeImage = "logo",
                        LargeText = "the almighty"
                    },
                    Timestamps = {
                        Start = beginningUnix
                    }
                };
            }

            activityManager.UpdateActivity(activity, (result) => {
                if (result == Discord.Result.Ok) {
                    Console.WriteLine("Success!");
                } else {
                    Console.WriteLine("Failed");
                }
            });
        }
    }

    void UpdateGameScriptReference() {
        if (currentScene >= 2 && 8 >= currentScene) {
            gameTimeScript = GameObject.Find("Clock").GetComponent<GameTime>();
        }
    }
}
