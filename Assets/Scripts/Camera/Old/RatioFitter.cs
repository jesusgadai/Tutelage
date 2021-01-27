using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class RatioFitter : MonoBehaviour, ILayoutSelfController
{

    private RectTransform canvasTransform;

    void Awake()
    {
        canvasTransform = GetComponentInParent<Canvas>().gameObject.transform as RectTransform;
    }

    public void SetLayoutHorizontal()
    {
        RectTransform rt = transform as RectTransform;
        rt.sizeDelta = new Vector2(canvasTransform.sizeDelta.y, canvasTransform.sizeDelta.x);
    }

    public void SetLayoutVertical()
    {
        RectTransform rt = transform as RectTransform;
        rt.sizeDelta = new Vector2(canvasTransform.sizeDelta.y, canvasTransform.sizeDelta.x);
    }

#if UNITY_EDITOR
    void Update()
    {
        if (canvasTransform == null)
            canvasTransform = GetComponentInParent<Canvas>().gameObject.transform as RectTransform;

        RectTransform rt = transform as RectTransform;
        rt.sizeDelta = new Vector2(canvasTransform.sizeDelta.y, canvasTransform.sizeDelta.x);
    }
#endif
}