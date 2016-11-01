﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UI.Pagination
{    
    /// <summary>
    /// Contains utility functions used to, for example, add prefabs to a scene
    /// </summary>
    public static class PaginationUtilities
    {
        public static GameObject InstantiatePrefab(string name, Type requiredParentType = null)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/" + name);

            if (prefab == null)
            {
                throw new UnityException(String.Format("Could not find prefab '{0}'!", name));
            }

            Transform parent = null;
#if UNITY_EDITOR
            parent = Selection.activeTransform;
            if (requiredParentType != null)
            {
                if (parent == null || parent.gameObject.GetComponent(requiredParentType) == null)
                {
                    UnityEditor.EditorUtility.DisplayDialog("Warning", "Pages may only be added to PagedRect objects - please select the Viewport object of a PagedRect.", "Okay");
                    return null;
                }
            }
#endif

            var gameObject = GameObject.Instantiate(prefab) as GameObject;
            gameObject.name = name;

            if (parent == null || !(parent is RectTransform))
            {
                parent = GetCanvasTransform();
            }

            gameObject.transform.SetParent(parent);

            var transform = (RectTransform)gameObject.transform;
            var prefabTransform = (RectTransform)prefab.transform;

            FixInstanceTransform(prefabTransform, transform);

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Undo.RegisterCreatedObjectUndo(gameObject, "Created " + name);
            }
#endif

            return gameObject;
        }

        public static Transform GetCanvasTransform()
        {
            Canvas canvas = null;
            
#if UNITY_EDITOR
            // Attempt to locate a canvas object parented to the currently selected object
            if (!Application.isPlaying && Selection.activeGameObject != null)
            {
                canvas = Selection.activeTransform.GetComponentInParent<Canvas>();
            }
#endif

            if (canvas == null)
            {
                // Attempt to find a canvas anywhere
                canvas = UnityEngine.Object.FindObjectOfType<Canvas>();

                if (canvas != null) return canvas.transform;
            }

            // if we reach this point, we haven't been able to locate a canvas
            // ...So I guess we'd better create one

            GameObject canvasGameObject = new GameObject("Canvas");
            canvasGameObject.layer = LayerMask.NameToLayer("UI");
            canvas = canvasGameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGameObject.AddComponent<CanvasScaler>();
            canvasGameObject.AddComponent<GraphicRaycaster>();

#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(canvasGameObject, "Create Canvas");
#endif

            var eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();

            if (eventSystem == null)
            {
                GameObject eventSystemGameObject = new GameObject("EventSystem");
                eventSystem = eventSystemGameObject.AddComponent<EventSystem>();
                eventSystemGameObject.AddComponent<StandaloneInputModule>();

                #if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
                eventSystemGameObject.AddComponent<TouchInputModule>();
                #endif

#if UNITY_EDITOR
                Undo.RegisterCreatedObjectUndo(eventSystemGameObject, "Create EventSystem");
#endif
            }

            return canvas.transform;
        }

        public static void FixInstanceTransform(RectTransform baseTransform, RectTransform instanceTransform)
        {            
            instanceTransform.localPosition = baseTransform.localPosition;
            instanceTransform.position = baseTransform.position;
            instanceTransform.rotation = baseTransform.rotation;
            instanceTransform.localScale = baseTransform.localScale;
            instanceTransform.anchoredPosition = baseTransform.anchoredPosition;
            instanceTransform.sizeDelta = baseTransform.sizeDelta;      
        }
    }
}
