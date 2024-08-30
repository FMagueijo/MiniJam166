using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FMLib
{
    namespace FMCore{
        public class FMCore
        {
            
        }

    }

    namespace FMTests
    {
        public enum TestResult { Success, Error, Done }

        public class FMTests {
            public static void CreateTestReport(string test, string msg, TestResult result)
            {
                string reportFinal = $"({test}) Report : {result.ToString()}";
                if (msg.Length > 0) reportFinal += $"\n{(result == TestResult.Error ? "Error" : "Message")}: {msg}";
                Debug.Log(reportFinal);
            }
        }

        public class FMCoreTest : MonoBehaviour
        {
            public static void CheckGameManager()
            {
                GameManager gm = GameManager.instance;
                FMTests.CreateTestReport("Check Game Manager", "", TestResult.Done);
            }

            public static void PrintError()
            {
                GameManager gm = GameManager.instance;
                FMTests.CreateTestReport("Check Game Manager", "Fucking hell man", TestResult.Error);
            }
        }
    }

    namespace FMSceneManagement
    {
        
        public class FMSceneManagement {
            public static string[] GetAllScenesNames(){
                var _scenes = EditorBuildSettings.scenes;
                string[] scenes = new string[_scenes.Length];
                
                for (int i = 0; i < scenes.Length; i++)
                {
                    scenes[i] = System.IO.Path.GetFileNameWithoutExtension(_scenes[i].path);
                }

                return scenes;
            }

            public static void LoadSceneInstant(string s){
                SceneManager.LoadScene(s);
            }
        }
    }

    namespace FMAttributes
    {
        public class ReadOnlyAttribute : PropertyAttribute { }
        [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
        public class ReadOnlyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                // Save the current GUI enabled state
                bool wasEnabled = GUI.enabled;

                // Disable the GUI to make the property readonly
                GUI.enabled = false;

                // Draw the property field
                EditorGUI.PropertyField(position, property, label, true);

                // Restore the GUI enabled state
                GUI.enabled = wasEnabled;
            }
        }
    }
}

