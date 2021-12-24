using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components {
    public delegate void TileDelegate(int x, int y);

    [RequireComponent(typeof(Button))]
    public class TileComponent : MonoBehaviour
    {
        public static event TileDelegate TileClickedEvent;
    
        private Button _button;
        private TextMeshProUGUI _text;

        private PlayerMark _mark;

        public PlayerMark Mark
        {
            get { return _mark; }
        }

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Init(int x, int y)
        {
            _button = GetComponent<Button>();
        
            _button.onClick.AddListener(
                delegate
                {
                    TileClickedEvent?.Invoke(x, y);
                });
        }

        public void SetMark(PlayerMark mark)
        {
            _mark = mark;
            _text.text = _mark.GetMarkByType();
        
            _button.interactable = false;
        }

        public void ClearTile()
        {
            _mark = PlayerMark.None;
            _text.text = _mark.GetMarkByType();
        
            _button.interactable = true;
        }

        public void BotTurn()
        {
            _button.onClick.Invoke();
        }
    }
}