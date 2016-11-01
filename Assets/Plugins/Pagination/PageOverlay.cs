using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Pagination
{    
    public class PageOverlay : MonoBehaviour
    {
        protected Page m_page;

        public bool initialised { get; protected set; }

        public Color PageOverlayNormalColor;
        public Color PageOverlayHoverColor;

        private Image m_Image;
        public Image Image
        {
            get
            {
                if (m_Image == null) m_Image = this.GetComponent<Image>();

                return m_Image;
            }
        }

        public void Initialise(Page page)
        {
            m_page = page;
            initialised = true;

            var rectTransform = this.transform as RectTransform;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.one;
            rectTransform.localScale = Vector3.one;
        }

        public void Clicked()
        {
            m_page.OverlayClicked();
        }

        public void MouseEnter()
        {
            Image.color = PageOverlayHoverColor;
        }

        public void MouseExit()
        {
            Image.color = PageOverlayNormalColor;
        }

        void OnEnable()
        {
            Image.color = PageOverlayNormalColor;
        }

        void OnDisable()
        {
            Image.color = PageOverlayNormalColor;
        }
    }
}
