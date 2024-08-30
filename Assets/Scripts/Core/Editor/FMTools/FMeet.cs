using UnityEngine;
using UnityEditor;
using FMLib.FMTests;
using FMLib.FMSceneManagement;

public class FMeet : EditorWindow
{
    [MenuItem("Tools/FMeet")]
    private static void ShowWindow()
    {
        var window = GetWindow<FMeet>();
        window.titleContent = new GUIContent("FMEET (Francisco Magueijo's Engine Evaluation Tool)");
        window.Show();
    }

    //Styles
    private GUIStyle titleStyle, subTitleStyle, highlightTitleStyle, textStyle;
    private void InitStyles()
    {
        //Init Styles
        titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        subTitleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 16,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };


        highlightTitleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 14,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.Bold
        };

        textStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 12,
            alignment = TextAnchor.MiddleLeft
        };
    }

    private void OnGUI()
    {
        InitStyles();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.LabelField("FMEET", titleStyle, GUILayout.Height(25));
        EditorGUILayout.LabelField("(Francisco Magueijo's Engine Evaluation Tool)", subTitleStyle, GUILayout.Height(25));

        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Made by Francisco Magueijo", subTitleStyle, GUILayout.Height(25));
        EditorGUILayout.EndVertical();

        //Views
        NotPlayingView();
        if (Application.isPlaying) PlayingView();

    }



    private bool SHOW_PV_SM_LOADSCENE = false;

    private void PlayingView()
    {

        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            EditorGUILayout.LabelField("Playing Tests", subTitleStyle, GUILayout.Height(25));

            //CORE CHECKS
            EditorGUILayout.LabelField("Core Checks", highlightTitleStyle, GUILayout.Height(25));
            {
                if (GUILayout.Button("Check Game Manager")) FMCoreTest.CheckGameManager();
                if (GUILayout.Button("Check Game Info")) FMCoreTest.PrintError();
            }

            //SCENE MNG
            EditorGUILayout.LabelField("Scene Management Tools", highlightTitleStyle, GUILayout.Height(25));
            {
                if(SHOW_PV_SM_LOADSCENE = EditorGUILayout.BeginFoldoutHeaderGroup(SHOW_PV_SM_LOADSCENE, "Load Scene"))
                {
                    foreach (string s in FMSceneManagement.GetAllScenesNames())
                    {
                        if (GUILayout.Button($"Load Scene ({s})")) FMSceneManagement.LoadSceneInstant(s);
                    }
                }
                EditorGUILayout.EndFoldoutHeaderGroup();

            }

            
        }
        EditorGUILayout.EndVertical();
    }

    private void NotPlayingView()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            EditorGUILayout.LabelField("Non Playing Tests", subTitleStyle, GUILayout.Height(25));
        }
        EditorGUILayout.EndVertical();
    }


}

