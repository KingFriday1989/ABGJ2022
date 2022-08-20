using Controllers.Utility;
using Helpers;
using Helpers.Audio;
using Helpers.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DoubleAgent.Controllers.Utility
{
    public sealed class ButtonBehaviour : ButtonMechanicsBase, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private int Channel;
        [SerializeField] private AudioClip HoverSound;
        [SerializeField] private AudioClip ClickSound;

        public void OnValidate()
        {
            button.OrNull(GetComponent<Button>()).interactable = IsEnabled;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsEnabled || !button.interactable) return;
            LeanTween.scale(gameObject, 1.2f * Vector3.one, 0.5f).setEaseSpring();
            SoundManager.PlaySoundOnChannel(HoverSound, Channel);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsEnabled || !button.interactable) return;
            LeanTween.scale(gameObject, Vector3.one, 0.5f).setEaseSpring();
        }

        protected override void OnClick()
        {
            if (!IsEnabled || !button.interactable) return;
            SoundManager.PlaySoundOnChannel(ClickSound, Channel + 1);
        }

        public async void PlayGame()
        {
            MainMenu.Instance().ShowLoadingScreen();
            await Timer.WaitForFrame();
            await MainMenu.Instance().LoadScene(2);
        }
    }
}