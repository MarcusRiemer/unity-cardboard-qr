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
        /// BSSIDs and names of the Wifi-Accesspoints.
        /// </summary>
        public static readonly Dictionary<string, string> BSSIDs = new Dictionary<string, string>()
        {
              { "f0:b0:52:23:f6:50", "SR9 1.OG" },
             { "f0:b0:52:1f:54:a0", "vor SR1" },
             { "f0:b0:52:20:9c:10", "vor HS2" },
             { "f0:b0:52:20:a2:70", "über HS2 1.OG" },
             { "f0:b0:52:1f:4b:c0", "über HS2 2.OG" },
              { "f0:b0:52:20:9d:90", "Audimax 1. OG" },
              { "f0:b0:52:1f:29:30", "Audimax Brücke 1. OG" },
              { "f0:b0:52:23:e6:d0", "RZ2" },
              { "f0:b0:52:20:97:10", "Lernboxen" },
              { "f0:b0:52:24:21:d0", "HS5" },
              { "f0:b0:52:20:97:60", "vor HS5" },
              { "f0:b0:52:20:9d:70", "Serverraum" },
              { "f0:b0:52:23:f1:90", "RZ3" },
              { "f0:b0:52:20:9f:b0", "zwischen HS1 und HS2" },
              { "f0:b0:52:1f:43:20", "RZ5" },
              { "f0:b0:52:1f:55:e0", "Kreuzung HS4" },
              { "f0:b0:52:20:99:50", "Lesemax" },
              { "f0:b0:52:1f:24:70", "Audimax" } ,
              { "f0:b0:52:20:a1:90", "VR Lab 1.OG" },
              { "f0:b0:52:1f:3f:30", "Cafeteria" },
              { "f0:b0:52:1f:45:10", "Netztechnik" },
              { "f0:b0:52:20:97:80", "Untergeschoss" },
              { "f0:b0:52:20:9a:10", "SR 11" } ,
              { "f0:b0:52:24:08:e0", "HS1" },
              { "f0:b0:52:20:9f:c0", "über HS1 1.OG" },
              { "f0:b0:52:1f:43:f0", "über HS1 2.OG" },
              { "f0:b0:52:29:3a:b", "Altbau ganz links" },
              { "f0:b0:52:1f:68:c0", "Altbau links" },
              { "f0:b0:52:23:de:80", "Altbau links 1.OG" },
              { "f0:b0:52:24:24:a0", "Altbau links 2.OG" },
              { "f0:b0:52:1f:63:f0", "Altbau rechts" },
              { "f0:b0:52:24:30:50", "Altbau rechts 1.OG" },
              { "f0:b0:52:25:93:d0", "Altbau rechts 2.OG" },
              { "f0:b0:52:23:8c:c0", "Altbau ganz rechts 2.OG" }


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