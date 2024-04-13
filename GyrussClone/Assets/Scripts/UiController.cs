using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text = null;

        private int _score;

        public void ChangeScore(int amount)
        {
            _score += amount;
            _text.text = "Score: " + _score;
        }
    }
}