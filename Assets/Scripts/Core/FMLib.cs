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
                // var _scenes = EditorBuildSettings.scenes;
                // string[] scenes = new string[_scenes.Length];

                // for (int i = 0; i < scenes.Length; i++)
                // {
                //     scenes[i] = System.IO.Path.GetFileNameWithoutExtension(_scenes[i].path);
                // }

                // return scenes;
                return null;
            }

            public static void LoadSceneInstant(string s){
                SceneManager.LoadScene(s);
            }
        }
    }

}

