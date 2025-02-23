using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Image icon;

    public IEnumerator Animation(float duration, bool isHide)
    {
        var currentScale = transform.localScale;
        var targetScale = isHide ? Vector3.zero : Vector3.one;

        var elapsedTime = 0f;

        while (elapsedTime < duration) 
        {
            var scale = Vector3.Lerp(currentScale, targetScale, elapsedTime/duration);
            transform.localScale = scale;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public virtual void Attach(ItemDescription item)
    {
        label.text = item.name;
        icon.sprite = item.icon;
    }

    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
