using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Utilities
{
    public class AutoFitContentGrid : MonoBehaviour
    {
        [SerializeField] private RectTransform contentRectTransform;
        [SerializeField] private GridLayoutGroup gridLayout;

        private void Awake() => ValidateRequiredVariables();

        

        private void OnEnable() => UpdateContentHeight();

        private void FixedUpdate() => UpdateContentHeight();



        private void UpdateContentHeight()
        {
            float contentHeight = 0;

            if (gridLayout != null)
            {
                contentHeight = (((gridLayout.spacing.y + gridLayout.cellSize.y) * gridLayout.transform.childCount) / 4) + gridLayout.cellSize.y;
            }

            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, contentHeight);

            RefreshContentLayout();
        }

        private void RefreshContentLayout()
        {
            gridLayout.enabled = false;
            gridLayout.enabled = true;
        }



        private void ValidateRequiredVariables()
        {
            if (contentRectTransform == null) { Debug.LogError("Null References: " + contentRectTransform.name); }
            if (gridLayout == null) { Debug.LogError("Null References: " + gridLayout.name); }
        }
    }
}