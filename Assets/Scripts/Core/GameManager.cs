using UnityEngine;

namespace Game.Core
{
    public enum GameState { Start, Playing, Paused, GameOver }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameState currentState;

        private void Awake()
        {
            if (Instance == null) 
            { 
                Instance = this; 
                DontDestroyOnLoad(gameObject); 
            }
            else 
            { 
                Destroy(gameObject); 
            }
        }

        public void UpdateState(GameState newState)
        {
            currentState = newState;
            Debug.Log("Game State Updated to: " + newState);
        }

        public void RestartLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
    }
}