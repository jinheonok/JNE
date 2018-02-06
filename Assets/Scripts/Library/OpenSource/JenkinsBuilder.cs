using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

public partial class Builder
{
	static string AndroidSDKPath
	{
		get { return EditorPrefs.GetString("AndroidSdkRoot"); }
		set { EditorPrefs.SetString("AndroidSdkRoot", value); }
	}

	static char dir_sep_char = Path.DirectorySeparatorChar;

	static string[] SCENES = FindEnabledEditorScenes();
	static string APP_NAME = "TestApp";
	static string TARGET_DIR = "ExportTest";

	static BuildOptions build_opt_android_release = BuildOptions.None;

	static BuildOptions build_opt_android_debug = BuildOptions.SymlinkLibraries |
		BuildOptions.Development |
		BuildOptions.ConnectWithProfiler |
		BuildOptions.AllowDebugging;

	[MenuItem("Build/Android")]
	public static void Build_Android()
	{
		AndroidSDKPath = @"/Users/jinheonok/Desktop/sdk";
		PlayerSettings.statusBarHidden = true;

		string target_dir = TARGET_DIR + dir_sep_char + APP_NAME + ".apk";
		GenericBuild(SCENES, target_dir, BuildTarget.Android, build_opt_android_release);
	}

	private static string[] FindEnabledEditorScenes()
	{
		List<string> EditorScenes = new List<string>();
		foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
		{
			if (!scene.enabled)
				continue;
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}

	static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
	{
		Debug.Log(string.Format("[Build - {0}] start! path : {1}", build_target.ToString(), target_dir));

		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		string res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
		if (res.Length > 0)
		{
			throw new Exception(string.Format("[Build - {0}] failure! {1}", build_target.ToString(), res));
		}
		else
		{
			Debug.Log(string.Format("[Build - {0}] success! path : {1}", build_target.ToString(), target_dir));
		}
	}
}