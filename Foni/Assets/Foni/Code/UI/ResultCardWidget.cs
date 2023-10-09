using Foni.Code.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Foni.Code.UI
{
    public struct ResultCardInfo
    {
        public string Title;
        public Sprite Sprite;
        public Sprite AltImage;
        public Sprite HandGesture;
        public AudioClip AudioClip;
    }

    public class ResultCardWidget : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private Image image;

        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private Image altImage;

        [SerializeField] private Image handGestureImage;

        [SerializeField] private AudioSource audioSource;

        private AudioClip _audioClip;

        public void Set(ResultCardInfo info)
        {
            image.sprite = info.Sprite;

            text.SetText(info.Title);

            altImage.gameObject.SetActive(info.AltImage != null);
            altImage.sprite = info.AltImage;

            handGestureImage.sprite = info.HandGesture;

            _audioClip = info.AudioClip;
        }

        public void PlaySound()
        {
            Globals.AudioManager.PlayAudioOneShot("result", _audioClip, true);
        }
    }
}