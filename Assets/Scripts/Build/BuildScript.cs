using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public class BuildScript : MonoBehaviour {
    static EditorBuildSettingsScene[] scenesToBuild = EditorBuildSettings.scenes;

    static BuildPlayerOptions buildOptions = new BuildPlayerOptions {
        scenes = GetScenePaths(scenesToBuild),
        locationPathName = "Dummy/Path",
        target = BuildTarget.StandaloneWindows
    };

    [MenuItem("Build/Create Build Folders")]
    public static void CreateFolders() {
        string path = SetBuildPath();
        string[] pathNames = {"Windows32", "Windows64", "OSXIntel", "Linux64", "Android"};

        foreach (string name in pathNames) {
            if (!Directory.Exists(path + "/" + name)) {
                Directory.CreateDirectory(path + "/" + name);
            }
        }

    }

    [MenuItem("Build/Open Build Folder")]
    public static void OpenBuildFolder() {
        UnityEngine.Debug.Log(Application.dataPath);
        Process explorer = new Process();
        explorer.StartInfo.FileName = Application.dataPath + "/../Build";
        explorer.Start();
    }

    // Build 32-bit Windows version
    [MenuItem("Build/Windows/Build Windows 32")]
    public static void BuildWindows32() {
        string path = SetBuildPath();
        if (!Directory.Exists(path + "/Windows32")) {Directory.CreateDirectory(path + "/Windows32");}

        buildOptions.locationPathName = string.Format("{0}/Windows32/Pet Noci u Krtkuse.exe", path);
        buildOptions.target = BuildTarget.StandaloneWindows;

        BuildPipeline.BuildPlayer(buildOptions);
    }

    // Build 64-bit Windows version
    [MenuItem("Build/Windows/Build Windows 64")]
    public static void BuildWindows64() {
        string path = SetBuildPath();
        if (!Directory.Exists(path + "/Windows64")) {Directory.CreateDirectory(path + "/Windows64");}

        buildOptions.locationPathName = string.Format("{0}/Windows64/Pet Noci u Krtkuse.exe", path);
        buildOptions.target = BuildTarget.StandaloneWindows64;

        BuildPipeline.BuildPlayer(buildOptions);
    }

    // Build macOS version for Intel CPUs
    [MenuItem("Build/OSX/Build OSX Intel")]
    public static void BuildOSXIntel() {
        string path = SetBuildPath();
        if (!Directory.Exists(path + "/OSXIntel")) {Directory.CreateDirectory(path + "/OSXIntel");}

        buildOptions.locationPathName = string.Format("{0}/OSXIntel/Pet Noci u Krtkuse", path);
        buildOptions.target = BuildTarget.StandaloneOSX;

        BuildPipeline.BuildPlayer(buildOptions);
    }

    // Build 64-bit Linux version
    [MenuItem("Build/Linux/Build Linux 64")]
    public static void BuildLinux64() {
        string path = SetBuildPath();
        if (Directory.Exists(path + "/Linux64")) {Directory.CreateDirectory(path + "/Linux64");}

        buildOptions.locationPathName = string.Format("{0}/Linux32/Pet Noci u Krtkuse.x86_64", path);
        buildOptions.target = BuildTarget.StandaloneLinux64;

        BuildPipeline.BuildPlayer(buildOptions);
    }

    // Build Android version
    [MenuItem("Build/Android/Build Android")]
    public static void BuildAndroid() {
        string path = SetBuildPath();
        if (Directory.Exists(path + "/Android")) {Directory.CreateDirectory(path + "/Android");}

        buildOptions.locationPathName = string.Format("{0}/Linux32/Pet Noci u Krtkuse.apk", path);
        buildOptions.target = BuildTarget.Android;

        BuildPipeline.BuildPlayer(buildOptions);
    }

    static string SetBuildPath() {
        string path = EditorUtility.SaveFolderPanel("Set Build Folder path", "", "");
        return path;
    }

    // Helper function, converts EditorBuildSettings.scenes to a string array
    static string[] GetScenePaths(EditorBuildSettingsScene[] scenes) {
        string[] scenePaths = new string[scenes.Length];
        for (int i = 0; i < scenes.Length; i++) {
            scenePaths[i] = scenes[i].path;
        }

        return scenePaths;
    }
}
#endif