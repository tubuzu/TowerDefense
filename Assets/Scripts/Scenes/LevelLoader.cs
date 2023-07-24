using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
   public class LevelLoader : MonoBehaviour
   {
      private static LevelLoader _instance;
      public static LevelLoader Instance => _instance;

      private void Awake() {
         _instance = this;
      }

      public void LoadGameScene(int lvl)
      {
         Preferences.SetCurrentLvl(lvl); //saving first
         SceneManager.LoadScene("Level_" + lvl);
      }


      public void LoadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

      public void LoadNextLvlScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


      public void LoadMainMenuScene() => SceneManager.LoadScene("MainMenu");
   }
}
