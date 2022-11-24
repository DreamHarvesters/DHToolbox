using GameFoundations.Runtime.Game;
using UnityEngine;

namespace GameFoundations.Runtime.UI
{
    public class GameStateButton : MonoBehaviour
    {
        [SerializeField] private GameState targetState;

        public void OnClick() => Game.Game.Instance.SetState(targetState);
    }
}