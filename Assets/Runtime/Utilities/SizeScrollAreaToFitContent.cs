using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Utilities
{
    public class SizeScrollAreaToFitContent : MonoBehaviour
    {
        [SerializeField] private RectTransform contentRectTransform;
        [SerializeField] private VerticalLayoutGroup verticalLayout;
        [SerializeField] private GridLayoutGroup gridLayout;

        private void Awake() => ValidateRequiredVariables();

        private void OnEnable() => UpdateContentHeight();

        private void FixedUpdate()
        {
            UpdateContentHeight();
        }

        private void UpdateContentHeight()
        {
            float contentHeight = 0;

            if (verticalLayout != null)
            {
                float childrenHeight = 0;

                foreach (Transform child in transform)
                    childrenHeight = childrenHeight + child.gameObject.GetComponent<RectTransform>().sizeDelta.y;

                contentHeight = childrenHeight + (verticalLayout.spacing * transform.childCount);
            }
               

            if (gridLayout != null)
            {
                contentHeight = (((gridLayout.spacing.y + gridLayout.cellSize.y) * transform.childCount) / 4) + gridLayout.cellSize.y;
            }


            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, contentHeight);

            RefreshContentLayout();
        }

        private void RefreshContentLayout()
        {
            if (verticalLayout != null)
            {
                verticalLayout.enabled = false;
                verticalLayout.enabled = true;
            }

            if (gridLayout != null)
            {
                gridLayout.enabled = false;
                gridLayout.enabled = true;
            }
        }



        private void ValidateRequiredVariables()
        {
            if (contentRectTransform == null) { Debug.LogError("Null References: " + contentRectTransform.name); }
        }
    }
}