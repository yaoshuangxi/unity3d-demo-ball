using UnityEditor;
using System.IO;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

class Builder {

	const string BUNDLE_ID = "-bundleIdentifier";
	const string VERSION = "-version";
	const string VERSION_CODE = "-versionCode";
	const string BUILD_NUMBER = "-buildNumber";


	class ArgumentSetting {
		public string bundleIdentifier;
		public string version;
		public int versionCode;
		public string buildNumber;
	}

	// Helper function for getting the command line arguments
	private static string GetArg (string name) {
		var args = System.Environment.GetCommandLineArgs ();
		for (int i = 0; i < args.Length; i++) {
			if (args [i] == name && args.Length > i + 1) {
				return args [i + 1];
			}
		}
		return null;
	}

	static void ParseArgs (ArgumentSetting setting, string[] args) {
		setting.bundleIdentifier = GetArg (BUNDLE_ID);
		setting.version = GetArg (VERSION);
		setting.versionCode = int.Parse (GetArg (VERSION_CODE));
		setting.buildNumber = GetArg (BUILD_NUMBER);
	}

	static string[] GetBuildScenes () {
		List<string> names = new List<string> ();
		foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes) {
			if (e == null)
				continue;

			if (e.enabled)
				names.Add (e.path);
		}
		return names.ToArray ();
	}

	static string GetBuildPath (BuildTarget target) {
		System.IO.Directory.CreateDirectory ("build");
		if (target == BuildTarget.Android) {
			return "build/android.apk";
		} else if (target == BuildTarget.iOS) {
			return "build/ios";
		}
		return "build/unknow";
	}

	static void Build () {

		ArgumentSetting setting = new ArgumentSetting ();
		string[] args = System.Environment.GetCommandLineArgs ();
		ParseArgs (setting, args);//解析参数

		Debug.Log ("Command line build version\n------------------------");
		Debug.Log (PlayerSettings.bundleIdentifier);
		PlayerSettings.bundleIdentifier = setting.bundleIdentifier;
		PlayerSettings.bundleVersion = setting.version;
		if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android) {
			PlayerSettings.Android.bundleVersionCode = setting.versionCode;
		} else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS) {
			PlayerSettings.iOS.buildNumber = setting.buildNumber;
		} else {
			//Error...
		}

		string[] scenes = GetBuildScenes ();
		if (scenes == null || scenes.Length == 0) {
			Debug.Log (string.Format ("Did not find any scene !!!"));
			return;
		}
		for (int i = 0; i < scenes.Length; ++i) {
			Debug.Log (string.Format ("Scene[{0}]: \"{1}\"", i, scenes [i]));
		}
		string path = GetBuildPath (EditorUserBuildSettings.activeBuildTarget);
		Debug.Log (string.Format ("Path: \"{0}\"", path));
		Debug.Log ("Starting Build...");
		BuildPipeline.BuildPlayer (scenes, path, EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
	}
}