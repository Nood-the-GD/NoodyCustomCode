using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NOOD.ScrollView
{
    [RequireComponent(typeof(ScrollRect))]
    public class HorizontalScrollController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private RectTransform _sampleListItem;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutG;

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
            int currentItemIndex = Mathf.RoundToInt((0 - _contentPanel.localPosition.x / (_sampleListItem.rect.width + _horizontalLayoutG.spacing)));

            Vector3 contentPanelPos = _contentPanel.localPosition;
            float currentScrollSpeed = _scrollRect.velocity.magnitude;

            if(_isDrag == false && _isSnapped == false && currentScrollSpeed < 200)
            {
                _scrollRect.velocity = Vector2.zero;
                _snappingSpeed += _snappingForce * Time.deltaTime;

                float targetX = 0 - (currentItemIndex * (_sampleListItem.rect.width + _horizontalLayoutG.spacing));

                float newX = Mathf.MoveTowards(_contentPanel.localPosition.x, targetX, _snappingSpeed);
                _contentPanel.localPosition = new Vector3(newX, contentPanelPos.y, contentPanelPos.z);

                if (_contentPanel.localPosition.x == targetX)
                    _isSnapped = true;
            }
        }
    }
}
