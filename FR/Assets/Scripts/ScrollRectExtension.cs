﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollRectExtension : ScrollRect
{
    public bool RouteToParent;
    
    private void DoForParents<T>(Action<T> action) where T : IEventSystemHandler
    {
        Transform parent = transform.parent;
        while (parent != null)
        {
            foreach (var component in parent.GetComponents<Component>())
            {
                if (component is T)
                {
                    action((T)(IEventSystemHandler)component);
                }
            }
            parent = parent.parent;
        }
    }

    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        DoForParents<IInitializePotentialDragHandler>(parent => { parent.OnInitializePotentialDrag(eventData); });
        base.OnInitializePotentialDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (RouteToParent)
        {
            DoForParents<IDragHandler>((parent) => { parent.OnDrag(eventData); });
        }
        else
        {
            base.OnDrag(eventData);
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!horizontal && Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
        {
            RouteToParent = true;
        }
        else if (!vertical && Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
        {
            RouteToParent = true;
        }
        else
        {
            RouteToParent = false;
        }

        if (RouteToParent)
        {
            DoForParents<IBeginDragHandler>((parent) => { parent.OnBeginDrag(eventData); });
        }
        else
        {
            base.OnBeginDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (RouteToParent)
        {
            DoForParents<IEndDragHandler>((parent) => { parent.OnEndDrag(eventData); });
        }
        else
        {
            base.OnEndDrag(eventData);
        }
        RouteToParent = false;
    }
}
