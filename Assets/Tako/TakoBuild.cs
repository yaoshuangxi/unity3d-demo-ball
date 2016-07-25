
using UnityEditor;
using System.IO;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

class TakoBuild
{
	static string[] GetBuildScenes ()
	{
		List<string> names = new List<string> ();

		foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes) {
			if (e == null)
				continue;

			if (e.enabled)
				names.Add (e.path);
		}
		return names.ToArray ();
	}

	static string GetBuildPath (BuildTarget target)
	{
		if (target == BuildTarget.Android) {
			return "build/android";
		} else if (target == BuildTarget.iOS) {
			return "build/ios";
		}
		return "build/unknow";
	}

	static void Build (BuildTarget target)
	{
		Debug.Log ("Command line build version\n------------------------");

		string[] scenes = GetBuildScenes ();
		if (scenes == null || scenes.Length == 0) {
			Debug.Log (string.Format ("Did not find any scene !!!"));
			return;
		}
		for (int i = 0; i < scenes.Length; ++i) {
			Debug.Log (string.Format ("Scene[{0}]: \"{1}\"", i, scenes [i]));
		}
		string path = GetBuildPath (target);
		Debug.Log (string.Format ("Path: \"{0}\"", path));
		Debug.Log ("Starting iOS Build!");
		BuildPipeline.BuildPlayer (scenes, path, target, BuildOptions.None);
	}

	[UnityEditor.MenuItem ("Tako/Test Command Line Build Step iOS")]
	static void BuildIOS ()
	{
		Build (BuildTarget.iOS);
	}

	[UnityEditor.MenuItem ("Tako/Test Command Line Build Step Android")]
	static void BuildAndroid ()
	{
		Build (BuildTarget.Android);
	}
}