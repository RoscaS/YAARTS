using DG.Tweening;
using Game.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GUI.Panels
{
    public class MessagesPanel : MonoBehaviour
    {
        public Transform messagePanel;
        public GameObject messagePrefab;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            PlayerController.Instance.OnMessage += MessageHandler;
        }

        /*------------------------------------------------------------------*\
        |*							HANDLERS
        \*------------------------------------------------------------------*/

        private void MessageHandler(string message) {
            var go = Instantiate(messagePrefab, messagePanel);
            var messageSlot = go.GetComponentInChildren<Text>();
            messageSlot.text = message;
            go.transform.localScale = Vector3.zero;
            FlashMessage(go);
        }

        private void FlashMessage(GameObject messageContainer) {
            var tr = messageContainer.transform;
            var fadeIn = tr.DOScale(Vector3.one, .5f)
                           .SetEase(Ease.OutBounce);
            var fadeOut = tr.DOScale(Vector3.zero, .5f)
                            .SetEase(Ease.InBounce)
                            .OnComplete(() => Destroy(tr.gameObject));
            DOTween.Sequence().Append(fadeIn).AppendInterval(1).Append(fadeOut);
        }
    }
}
