using System.Collections;
using TMPro;
using UnityEngine;

namespace GameManagers
{
    public class HintManager : MonoBehaviour
    {
        [SerializeField] private GameObject _hintWindow;
        [SerializeField] private TMP_Text _hintText;
        private WaitForSeconds _hideHintDelay = new WaitForSeconds(2f);
        private Coroutine _hintCoroutine;

        public void ShowHint(string text)
        {
            _hintText.text = text;
            _hintWindow.SetActive(true);
        }

        public void HideHint()
        {
            _hintWindow?.SetActive(false);
        }

        public void ShowAndHideHint(string text)
        {
            if (_hintCoroutine != null)
            {
                StopCoroutine(_hintCoroutine);
            }

            _hintCoroutine = StartCoroutine(ShowAndHideHintCoroutine(text));
        }

        private IEnumerator ShowAndHideHintCoroutine(string text)
        {
            ShowHint(text);
            yield return _hideHintDelay;
            HideHint();
            _hintCoroutine = null;
        }
    }
}