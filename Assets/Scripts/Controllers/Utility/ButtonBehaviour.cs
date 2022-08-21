using Controllers.Utility;
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

        protected override void OnHover()
        {
            LeanTween.scale(gameObject, 1.2f * Vector3.one, 0.5f).setEaseSpring();
            SoundManager.PlaySoundOnChannel(HoverSound, Channel);
        }

        protected override void OnUnHover()
        {
            LeanTween.scale(gameObject, Vector3.one, 0.5f).setEaseSpring();
        }

        protected override void OnClick()
        {
            SoundManager.PlaySoundOnChannel(ClickSound, Channel + 1);
        }

        public void PlayGame()
        {
            if (!Enabled) return;
            MainMenu.Instance().LoadGame();
        }
    }
}