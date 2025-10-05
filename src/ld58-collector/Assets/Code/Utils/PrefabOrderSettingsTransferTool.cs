using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PrefabOrderSettingsTransferTool : MonoBehaviour
{
    public float zStep = 0.1f;
    public float orderLayerMultiplier = 5f;
    public bool IgnoreFaderBlocker = true;
    public bool IgnoreButtonBack = true;
    private List<SpriteRenderer> GetSprites()
    {
        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true).ToList();
        if (IgnoreFaderBlocker)
        {
            var fadeBlocker = spriteRenderers.FirstOrDefault(x => x.gameObject.name == "FaderBlocker");
            spriteRenderers.Remove(fadeBlocker);
        }

        if (IgnoreButtonBack)
        {
            var ButtonBack = spriteRenderers.FirstOrDefault(x => x.gameObject.name == "BtnBack");
            spriteRenderers.Remove(ButtonBack);
        }

        return spriteRenderers;
    }
#if UNITY_EDITOR
    [ContextMenu("Transfer SortingOrders to Positive")]
    public void ChangeOrderLayersToPositive()
    {
        var spriteRenderers = GetSprites();
        if (IgnoreButtonBack)
        {
            var fadeBlocker = spriteRenderers.FirstOrDefault(x => x.gameObject.name == "FaderBlocker");
            spriteRenderers.Remove(fadeBlocker);
        }

        if (IgnoreButtonBack)
        {
            var ButtonBack = spriteRenderers.FirstOrDefault(x => x.gameObject.name == "BtnBack");
            spriteRenderers.Remove(ButtonBack);
        }

        int minOrder = spriteRenderers.Min(x => x.sortingOrder);
        if (minOrder > 0)
        {
            return;
        }

        List<Object> objects = new List<Object>();
        objects.Add(gameObject);
        foreach (var spr in spriteRenderers)
        {
            objects.Add(spr);
            objects.Add(spr.gameObject);
        }

        Undo.RecordObjects(objects.ToArray(), "ChangedOrders");
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder -= minOrder;
        }

        //PrefabUtility.SavePrefabAsset(gameObject);
    }


    [ContextMenu("Convert OrderLayers To Z")]
    public void ConvertOrderLayersToZ()
    {
        var sprites = GetSprites();
        List<Object> savedObjects = new List<Object> { gameObject };
        foreach (var spr in sprites)
        {
            savedObjects.Add(spr);
            savedObjects.Add(spr.transform);
        }

        Undo.RecordObjects(savedObjects.ToArray(), "Change sortingOrders to Layers");
        ChangeOrderLayersToPositive();
        foreach (var spr in sprites)
        {
            spr.transform.position -= new Vector3(0, 0, zStep * spr.sortingOrder);
            spr.sortingOrder = 0;
        }
    }

    [ContextMenu("Convert Z to order layers")]
    public void ConvertZToOrderLayer()
    {
        var sprites = GetSprites();
        List<Object> savedObjects = new List<Object> { gameObject };
        foreach (var spr in sprites)
        {
            savedObjects.Add(spr);
            savedObjects.Add(spr.transform);
        }
        
        Undo.RecordObjects(savedObjects.ToArray(), "Z to Layers");
        foreach (var spr in sprites)
        {
            spr.sortingOrder = (int)(Mathf.Abs(spr.transform.position.z - gameObject.transform.position.z) / zStep * orderLayerMultiplier);
            var localPosition = spr.transform.localPosition;
            localPosition =
                new Vector3(localPosition.x, localPosition.y, 0);
            spr.transform.localPosition = localPosition;
        }
    }

    [ContextMenu("Add sprite renderer to Colliders for raycasting")]
    public void AddSpriteGraphicsToColliders()
    {
        List<Collider2D> colliders = GetComponentsInChildren<Collider2D>(true).ToList();
        colliders.Remove(GetComponent<Collider2D>());
        if (IgnoreFaderBlocker)
        {
            var fadeBlocker = colliders.FirstOrDefault(x => x.name == "FaderBlocker");
            colliders.Remove(fadeBlocker);
        }

        if (IgnoreButtonBack)
        {
            var btnBack = colliders.FirstOrDefault(x => x.name == "BtnBack");
            colliders.Remove(btnBack);
        }

        List<Object> objects = new List<Object> { gameObject };
        foreach (var coll in colliders)
        {
            objects.Add(coll);
            objects.Add(coll.gameObject);
        }
        Undo.RecordObjects(objects.ToArray(),"add SpriteRenderers to colliders");

        foreach (var coll in colliders)
        {
            if (coll.GetComponent<SpriteRenderer>() != null) continue;
            var parentSprites = coll.GetComponentsInParent<SpriteRenderer>(true);
            var parentSprite = parentSprites.Length != 0 ? parentSprites[0] : null;
            SpriteRenderer spr = coll.gameObject.AddComponent<SpriteRenderer>();
            spr.sortingOrder = parentSprite != null ? parentSprite.sortingOrder : 0;
            spr.sprite = null;
        }
    }
#endif
}