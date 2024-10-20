using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Utilities
{
    public class AutoFitContentVetical : MonoBehaviour
    {
        [SerializeField] private RectTransform contentRectTransform;
        [SerializeField] private VerticalLayoutGroup verticalLayout;

        private void Awake() => ValidateRequiredVariables();



        private void OnEnable() => UpdateContentHeight();

        private void FixedUpdate() => UpdateContentHeight();



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
              
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, contentHeight);

            RefreshContentLayout();
        }

        private void RefreshContentLayout()
        {
            verticalLayout.enabled = false;
            verticalLayout.enabled = true;
        }



        private void ValidateRequiredVariables()
        {
            if (contentRectTransform == null) { Debug.LogError("Null References: " + contentRectTransform.name); }
            if (verticalLayout == null) { Debug.LogError("Null References: " + verticalLayout.name); }
        }
    }
}