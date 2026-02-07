using UnityEngine;

namespace Game.Core {
    public enum GameState { Playing, Paused, GameOver }

    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        public GameState CurrentState { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Giữ lại khi chuyển Scene
            } else {
                Destroy(gameObject);
            }
        }

        public void UpdateState(GameState newState) {
            CurrentState = newState;
        }
    }
}