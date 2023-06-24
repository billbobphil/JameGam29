using UnityEngine;


namespace Helpers
{
    public static class RoundVector2
    {
        public static Vector2 Round(Vector2 v)
        {
            return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
        }
    }
}