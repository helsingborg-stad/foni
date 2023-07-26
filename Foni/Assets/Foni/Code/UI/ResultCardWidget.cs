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
        public AudioClip AudioClip;
    }

    public class ResultCardWidget : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private Image image;

        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private Image altImage;

        [SerializeField] private AudioSource audioSource;

        public void Set(ResultCardInfo info)
        {
            image.sprite = info.Sprite;

            text.SetText(info.Title);

            altImage.gameObject.SetActive(info.AltImage != null);
            altImage.sprite = info.AltImage;

            audioSource.clip = info.AudioClip;
        }

        public void PlaySound()
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}