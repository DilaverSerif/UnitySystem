using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static Transform GetClose(this Transform[] transforms, Transform currentTransform)
    {
        if (transforms.Length == 0)
            return null;

        var enemy = transforms.OrderBy(x => Vector3.Distance(currentTransform.position, x.transform.position))?.First();

        return enemy.transform;
    }

    public static Transform GetClose(this Transform[] transforms, Vector3 currentVector)
    {
        if (transforms.Length == 0)
            return null;

        var enemy = transforms.OrderBy(x => Vector3.Distance(currentVector, x.transform.position))?.First();

        return enemy.transform;
    }
    
    public static Vector3 RandomPointInBounds(this Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public static bool CheckNullCoruntiene(this Coroutine coroutine)
    {
        return coroutine == null;
    }

    public static bool CheckLayer(this GameObject go, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << go.layer));
    }

    public static Color SetAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public static float RandomValueVector2(this Vector2 vector)
    {
        return Random.Range(vector.x, vector.y);
    }

    public static Transform[] FindWithLayer(GameObject[] list, LayerMask mask)
    {
        Transform[] enemys = new Transform[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            if (CheckLayer(list[i], mask))
            {
                enemys[i] = list[i].transform;
            }
        }

        return enemys;
    }

    public static bool CheckTag(this GameObject gb, string tag)
    {
        return gb.CompareTag(tag);
    }

    public static string StripPunctuation(this string s)
    {
        var sb = new StringBuilder();
        foreach (char c in s)
        {
            if (!char.IsPunctuation(c))
                sb.Append(c);
        }

        return sb.ToString();
    }

    private static T Raycast<T>(this T gen, Vector3 pos, LayerMask masks) where T : MonoBehaviour
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, masks))
        {
            return hit.transform.GetComponent<T>();
        }

        return default(T);
    }

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }

    public static Vector3 GetMouseWorldPosition(this Camera cam)
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.nearClipPlane;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    private static MonoBehaviour _monoBehaviour;

    public static void SetNullCoroutine(this Coroutine coroutine)
    {
        if (coroutine != null)
        {
            _monoBehaviour.StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public static bool CheckAnimRunning(this Animator anim, string name)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public static List<T> MoveItem<T>(this List<T> theList, T item, int index)
    {
        theList.Remove(item);
        theList.Insert(index, item);
        return theList;
    }

    public static Vector3 GetRandomPointInRadius(this Vector3 rand, ref float radius, ref Vector3 offset)
    {
        var randomPoint = Random.insideUnitCircle * radius;
        return rand + offset + new Vector3(randomPoint.x, 0, randomPoint.y);
    }
    
    public static Vector3 GetRandomPointInRadius(this Vector3 rand, ref float radius,ref float yPos, ref Vector3 offset)
    {
        var randomPoint = Random.insideUnitCircle * radius;
        randomPoint.y = yPos;
        return rand + offset + new Vector3(randomPoint.x, 0, randomPoint.y);
    }
    
    public static T[] InArray<T>(this IEnumerable<T> objects, string tag) where T : Component
    {
        return objects.Where(obj => obj.CompareTag(tag)).ToArray();
    }
    
    public static List<T> FindInListObjectsOfTag<T>(this IEnumerable<T> objects, string tag) where T : Component
    {
        return objects.Where(obj => obj.CompareTag(tag)).ToList();
    }

    public static float GetGroundHeight(ref Vector3 position)
    {
        if (Physics.Raycast(position, Vector3.down, out var hit, 100))
        {
            return hit.point.y;
        }

        return -1;
    }

    public static T[] DeleteNullInArray<T>(this IEnumerable<T> go)
    {
        return go.Where(t => t != null).ToArray();
    }
    
    public static T GetClosest<T>(this List<T> list, Vector3 currentPosition, int count = 1) where T : MonoBehaviour
    {
        return list.OrderBy(x => Vector3.Distance(currentPosition, x.transform.position)).Take(count).ToList().FirstOrDefault();
    }
    
    // public static void TweenBlendShape(this SkinnedMeshRenderer meshRenderer, float initial, float final, float time)
    // {
    //     var blendShapeValue = meshRenderer.GetBlendShapeWeight(0);
    //     DOTween.To(() => blendShapeValue, x => blendShapeValue = x, final, 0.3f).OnUpdate(() =>
    //     {
    //         meshRenderer.SetBlendShapeWeight(0, blendShapeValue);
    //         Debug.Log("SettingValue");
    //     });
    // }
    
    public static float GetDistanceTo(this Vector3 pos, Vector3 target)
    {
        target.y = pos.y;
        return Vector3.Distance(pos, target);
    }
    
    public static Vector3[] GetCirclePosition(Vector3 position,int segment,float radius,float y)
    {
        var points = new Vector3[segment+1];
        
        for (int i = 0; i < segment+1; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segment);
            points[i] = new Vector3(Mathf.Sin(rad)* radius, 0, Mathf.Cos(rad)* radius) ;
            points[i] += position;
            points[i].y = y;
        }
        
        return points;
    }

#if UNITY_EDITOR
    public static void DrawDisc(Vector3 center, float radius, Color color, float thickness = 10f)
    {
        Handles.color = color;
        Handles.DrawWireDisc(center, Vector3.down, radius, thickness);
    }

    public static void DrawText(Vector3 position, string text, Color32 color)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;
        Handles.Label(position, text);
    }
#endif
}