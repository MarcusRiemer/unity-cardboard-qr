using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Class containing application configuration parameters.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Activate location based functions
        /// </summary>
        public static readonly bool locationBased = true;

        /// <summary>
        /// Scene names. Must match the order of <see cref="Config.SceneNames"/> array.
        /// </summary>
        public enum Scenes
        {
            Game = 0,
            Camera = 1,
            Question = 2,
            Help = 3,
            HelpDefault = 4,
            Start = 5,
            None = -1
        }

        /// <summary>
        /// Names of Unity scenes. Must match scene file names.
        /// </summary>
        private static readonly string[] SceneNames =
        {
            "GameScene",
            "CameraScene",
            "QuestionScene",
            "HelpScene",
            "HelpDefaultScene",
            "StartScreen"
        };


        /// <summary>
        /// Returns the name of a given scene.
        /// </summary>
        /// <param name="scene">Scene</param>
        /// <returns>Name of corresponding Unity scene file</returns>
        public static string SceneName(Scenes scene)
        {
            return SceneNames[(int) scene];
        }

        // API
        // Calls to Timo Jürgens FH Wedel hosted backend.
        public const string ApiUrlQuestionCount = "http://stud.fh-wedel.de/~inf9903/api.php/questioncount";
        public const string ApiUrlLocationCount = "http://stud.fh-wedel.de/~inf9903/api.php/locationcount";
        public const string ApiUrlQuestions = "http://stud.fh-wedel.de/~inf9903/api.php/questions/";
        public const string ApiUrlThings = "http://stud.fh-wedel.de/~inf9903/api.php/things/";
        public const string ApiUrlPositions = "http://stud.fh-wedel.de/~inf9903/api.php/positions/";
        public const string ApiUrlLocations = "http://stud.fh-wedel.de/~inf9903/api.php/locations/";
        /// <summary>
        /// Storage path for global state.
        /// </summary>
        public static readonly string StatePath = Application.persistentDataPath + "/globalState.dat";
    }
}