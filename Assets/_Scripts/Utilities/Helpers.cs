using UnityEngine;

/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Random float value
    /// </summary>
    public static float GetRandomFloat
    {
        get
        {
            return Random.Range(-1f, 1f);
        }
    }

    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    /// <summary>
    /// Rotate  object based on relative position
    /// </summary>
    /// <param name="x">Direction</param>
    /// <param name="transform">Transform of object</param>
    public static void Rotate(float x, Transform transform)
    {
        if (x == 0) return;

        float angle = x < 0.0f ? 180f : 0f;
        transform.eulerAngles = new Vector2(0f, angle);
    }

    /// <summary>
    /// Returns a random vector.
    /// </summary>
    public static Vector3 GetRandomVector() => new(GetRandomFloat, GetRandomFloat);

    /// <summary>
    /// Gets the euler angle of the object according to the direction of its movement.
    /// </summary>
    public static Quaternion GetAngle(Vector2 direction)
    {
        float angle = (float)(Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI);

        return Quaternion.Euler(0f, 0f, angle);
    }
}
