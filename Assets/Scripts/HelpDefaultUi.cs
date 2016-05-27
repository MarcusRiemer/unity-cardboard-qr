using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Controls the help menu behavior. Is attached to Unity HelpScene.Canvas.
    /// </summary>
    public class HelpDefaultUi : MonoBehaviour {

        void Start () 
        {
            GameObject.Find ("HelpText").GetComponent<Text> ().text = StringResources.HelpDefaultText;
            GameObject.Find ("TitleText").GetComponent<Text> ().text = StringResources.HelpSceneDefaultHeading;
            GameObject.Find ("BackButton").GetComponentInChildren<Text>().text = StringResources.BackButtonText;
        }
	
        /// <summary>
        /// Returns to MainMenuScene. Is attached to HelpScene.BackButton.
        /// </summary>
        public void OnClickBackButton() 
        {
            SceneManager.LoadScene(Config.SceneName(Config.Scenes.Start));
        }
    }
}
