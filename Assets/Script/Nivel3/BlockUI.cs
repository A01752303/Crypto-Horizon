using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Nivel3Block
{
    public class BlockUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public string blockID;
        public TextMeshProUGUI idText;

        private RectTransform rectTransform;
        private Canvas canvas;
        private Vector3 originalPosition;
        private Transform originalParent;
        private bool isDraggable = true;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            originalParent = transform.parent;
            originalPosition = rectTransform.position;
        }

        public void SetID(string id)
        {
            blockID = id;
            if (idText != null)
                idText.text = id;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isDraggable) return;
            originalParent = transform.parent;
            transform.SetParent(canvas.transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDraggable) return;
            rectTransform.position += (Vector3)eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        }

        public void ReturnToOriginal()
        {
            transform.SetParent(originalParent);
            rectTransform.position = originalPosition;
        }

        public void DisableDrag()
        {
            isDraggable = false;
        }
    }
}
