using TMPro;
using UnityEngine;

namespace GameManagers
{
    public class HintManager : MonoBehaviour
    {
        [SerializeField] private GameObject _hintWindow;
        [SerializeField] private TMP_Text _hintText;

        public void ShowHint(string text)
        {
            _hintText.text = text;
            _hintWindow.SetActive(true);
        }

        public void HideHint()
        {
            _hintWindow.SetActive(false);
        }
    }
}