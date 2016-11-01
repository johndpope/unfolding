using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Pagination
{
    /// <summary>
    /// An individual page within a PagedRect.
    /// </summary>
    public class Page : MonoBehaviour
    {
        public int PageNumber { get; set; }

        [SerializeField, Tooltip("Sets the text shown on the button if 'ShowPageTitlesOnButtons' is set")]
        public string PageTitle = "";

        [SerializeField, Tooltip("Should this page be accessible?")]
        public bool PageEnabled = true;

        [SerializeField, Tooltip("Should this button be shown on pagination?")]
        public bool ShowOnPagination = true;

        [SerializeField]
        public PageEvent OnShowEvent = new PageEvent();
        [SerializeField]
        public PageEvent OnHideEvent = new PageEvent();

        [Serializable]
        public class PageEvent : UnityEngine.Events.UnityEvent { }

        public bool Initialised { get; protected set; }
        public Animator Animator { get; protected set; }

        public bool Visible { get; protected set; }

        protected PagedRect _pagedRect { get; set; }

        protected Vector3 initialPosition { get; set; }

        protected CanvasGroup _CanvasGroup;
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_CanvasGroup == null)
                {
                    _CanvasGroup = this.GetComponent<CanvasGroup>();
                    if (_CanvasGroup == null)
                    {
                        _CanvasGroup = this.gameObject.AddComponent<CanvasGroup>();
                    }
                }

                return _CanvasGroup;
            }
        }

        public bool UsePageAnimationType = false;
        public PagedRect.eAnimationType AnimationType;
        public bool FlipAnimationDirection = false;
        
        private LayoutElement m_layoutElement;
        protected LayoutElement layoutElement
        {
            get
            {
                if (m_layoutElement == null) m_layoutElement = this.GetComponent<LayoutElement>();

                if (m_layoutElement == null) m_layoutElement = this.gameObject.AddComponent<LayoutElement>();                

                return m_layoutElement;
            }
        }

        private PageOverlay m_pageOverlay;
        protected PageOverlay pageOverlay
        {
            get
            {
                if (m_pageOverlay == null)
                {                    
                    var pageOverlayGameObject = PaginationUtilities.InstantiatePrefab("Page Overlay");
                    pageOverlayGameObject.transform.SetParent(this.transform);

                    m_pageOverlay = pageOverlayGameObject.GetComponent<PageOverlay>();

                    if (!m_pageOverlay.initialised) m_pageOverlay.Initialise(this);                    
                }                

                return m_pageOverlay;
            }
        }

        /// <summary>
        /// Initialise this Page object and attach it to its parent PagedRect.
        /// </summary>
        /// <param name="pagedRect"></param>
        public void Initialise(PagedRect pagedRect)
        {
            if (Initialised) return;

            initialPosition = this.transform.localPosition;

            Initialised = true;

            _pagedRect = pagedRect;

            UpdateDimensions();

            if (Application.isPlaying)
            {
                Animator = this.GetComponent<Animator>();

                if (Animator == null)
                {
                    // setup the animator for this page
                    Animator = this.gameObject.AddComponent<Animator>();                    
                }

                Animator.runtimeAnimatorController = Instantiate(pagedRect.AnimationControllerTemplate) as RuntimeAnimatorController;

                if (pageOverlay != null)
                {
                    pageOverlay.Initialise(this);
                    pageOverlay.Image.sprite = _pagedRect.PagePreviewOverlayImage;
                    pageOverlay.PageOverlayNormalColor = _pagedRect.PagePreviewOverlayNormalColor;
                    pageOverlay.PageOverlayHoverColor = _pagedRect.PagePreviewOverlayHoverColor;

                    if (!_pagedRect.ShowPagePreviews) pageOverlay.gameObject.SetActive(false);
                }
            }                        
        }

        public void UpdateDimensions()
        {
            if (_pagedRect == null) return;
            
            RectTransform rectTransform = _pagedRect.sizingTransform;
            if (rectTransform == null) rectTransform = (RectTransform)_pagedRect.transform;

            var rect = rectTransform.rect;
            if (rect.height > 0) layoutElement.preferredHeight = rect.height;
            if (rect.width > 0) layoutElement.preferredWidth = rect.width;                
        }

        /// <summary>
        /// Called when this Page is shown. Triggers any OnShow events that have been set.
        /// </summary>
        public void OnShow()
        {
            Visible = true;

            if (OnShowEvent != null)
            {
                OnShowEvent.Invoke();
            }

            HideOverlay();            
        }

        /// <summary>
        /// Called when this Page is hidden. Triggers any OnHide events that have been set.
        /// </summary>
        public void OnHide()
        {
            Visible = false;

            if (OnHideEvent != null)
            {
                OnHideEvent.Invoke();
            }

            if(_pagedRect.ShowPagePreviews) ShowOverlay();            
        }        

        /// <summary>
        /// Show a Fade-In animation.
        /// </summary>
        public void FadeIn()
        {
            gameObject.SetActive(true);            
            PlayNewAnimation("FadeIn");            
        }

        /// <summary>
        /// Show a Fade-Out animation.
        /// </summary>
        public void FadeOut()
        {
            PlayNewAnimation("FadeOut");
            StartCoroutine(DisableWhenAnimationIsComplete());            
        }

        /// <summary>
        /// Show a Slide-In animation.
        /// </summary>
        /// <param name="directionFrom"></param>
        /// <param name="vertical"></param>
        public void SlideIn(PagedRect.eDirection directionFrom, bool vertical = false)
        {            
            gameObject.SetActive(true);

            var direction = directionFrom.ToString();

            if (vertical)
            {
                direction = directionFrom == PagedRect.eDirection.Left ? "Top" : "Bottom";
            }

            PlayNewAnimation("SlideIn_" + direction);            
        }

        /// <summary>
        /// Show a Slide-Out animation.
        /// </summary>
        /// <param name="directionTo"></param>
        /// <param name="vertical"></param>
        public void SlideOut(PagedRect.eDirection directionTo, bool vertical = false)
        {            
            var direction = directionTo.ToString();

            if (vertical)
            {
                direction = directionTo == PagedRect.eDirection.Left ? "Top" : "Bottom";
            }

            PlayNewAnimation("SlideOut_" + direction);

            StartCoroutine(DisableWhenAnimationIsComplete());            
        }

        /// <summary>
        /// Used to disable this Page GameObject once a FadeOut/SlideOut animation has completed.
        /// </summary>
        /// <returns></returns>
        protected IEnumerator DisableWhenAnimationIsComplete()
        {            
            yield return new WaitForSeconds(1f / _pagedRect.AnimationSpeed);

            if (_pagedRect.GetCurrentPage() != this)    // if we are the current page, then the user has scrolled back to us
            {
                gameObject.SetActive(false);
                ResetPositionAndAlpha();                
            }            
        }

        /// <summary>
        /// Stop any animations currently being played and switch to a new one.
        /// </summary>
        /// <param name="animationName"></param>
        protected void PlayNewAnimation(string animationName)
        {            
            Animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            Animator.speed = _pagedRect.AnimationSpeed;
            Animator.enabled = true;
            Animator.StopPlayback();

            Animator.Play(animationName);                        
        }

        /// <summary>
        /// Return this Page to its default position and visibility
        /// </summary>
        public void ResetPositionAndAlpha()
        {
            this.transform.localPosition = initialPosition;

            // reset alpha values too
            this.CanvasGroup.alpha = 1;
        }

        /// <summary>
        /// Enable this Page
        /// </summary>
        public void EnablePage()
        {
            this.PageEnabled = true;
            _pagedRect.UpdatePages();
        }

        /// <summary>
        /// Disable this Page (this will cause its pagination button to become disabled)
        /// </summary>
        public void DisablePage()
        {
            this.PageEnabled = false;
            _pagedRect.UpdatePages();
        }

        public void OverlayClicked()
        {
            if (_pagedRect.ShowPagePreviews) _pagedRect.SetCurrentPage(this);
        }

        private Coroutine scaleCoroutine = null;

        public void ScaleToSize(Vector2 size, float animationSpeed = 0.5f)
        {
            if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);

            // if we're already the right size, don't bother
            if (size == ((RectTransform)this.transform).rect.size) return;

            scaleCoroutine = StartCoroutine(ScaleToSizeInternal(size, animationSpeed));
        }

        protected IEnumerator ScaleToSizeInternal(Vector2 size, float animationSpeed)
        {
            float percentageComplete = 0f;
            float timeStartedMoving = Time.time;
            float timeSinceStarted = 0f;
            
            Vector2 initialSize = ((RectTransform)this.transform).rect.size;

            while (percentageComplete < 1f)
            {
                timeSinceStarted = Time.time - timeStartedMoving;

                layoutElement.preferredWidth = Mathf.Lerp(initialSize.x, size.x, percentageComplete);
                layoutElement.preferredHeight = Mathf.Lerp(initialSize.y, size.y, percentageComplete);

                percentageComplete = timeSinceStarted / (0.25f / animationSpeed);
                yield return null;
            }

            layoutElement.preferredWidth = size.x;
            layoutElement.preferredHeight = size.y;            
        }

        public void ShowOverlay()
        {
            if (pageOverlay != null) pageOverlay.gameObject.SetActive(true);
        }

        public void HideOverlay()
        {
            if (pageOverlay != null) pageOverlay.gameObject.SetActive(false);
        }

        #region Menu Items
#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/UI/Pagination/Page")]
        static void AddPagePrefab()
        {
            var page = PaginationUtilities.InstantiatePrefab("Page", typeof(UI.Pagination.Viewport));

            if (page != null)
            {
                var pagedRect = page.GetComponentInParent<PagedRect>();
                if (pagedRect != null)
                {
                    UnityEditor.EditorApplication.delayCall += () =>
                        {
                            pagedRect.UpdateDisplay();
                            pagedRect.ShowLastPage();
                        };
                }
            }
        }
#endif
        #endregion
    }
}
