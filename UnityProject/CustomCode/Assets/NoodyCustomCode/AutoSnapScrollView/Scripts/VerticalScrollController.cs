using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NOOD.ScrollView
{
    [RequireComponent(typeof(ScrollRect))]
    public class VerticalScrollController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private RectTransform _sampleListItem;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutG;

        bool _isDrag;
        bool _isSnapped = false;
        [SerializeField] private float _snappingForce = 1;
        float _snappingSpeed;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDrag = true;
            _snappingSpeed = 0;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDrag = false;
            _isSnapped = false;
        }

        // Update is called once per frame
        void Update()
        {
            int currentItemIndex = Mathf.RoundToInt((0 - _contentPanel.localPosition.y / (_sampleListItem.rect.height + _verticalLayoutG.spacing)));

            Vector3 contentPanelPos = _contentPanel.localPosition;
            float currentScrollSpeed = _scrollRect.velocity.magnitude;

            if(_isDrag == false && _isSnapped == false && currentScrollSpeed < 200)
            {
                _scrollRect.velocity = Vector2.zero;
                _snappingSpeed += _snappingForce * Time.deltaTime;

                float targetY = 0 - (currentItemIndex * (_sampleListItem.rect.height + _verticalLayoutG.spacing));

                float newY = Mathf.MoveTowards(_contentPanel.localPosition.y, targetY, _snappingSpeed);
                _contentPanel.localPosition = new Vector3(contentPanelPos.x, newY, contentPanelPos.z);

                if (_contentPanel.localPosition.y == targetY)
                    _isSnapped = true;
            }
        }
    }
}
