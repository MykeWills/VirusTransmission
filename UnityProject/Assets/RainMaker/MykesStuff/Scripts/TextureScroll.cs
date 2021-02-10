using UnityEngine;
using System.Collections;

public class TextureScroll : MonoBehaviour {

    public float scrollSpeed = 0.5F;
    public Renderer rend;

    public bool ScrollXRight;
    public bool ScrollXLeft;
    public bool ScrollYUp;
    public bool ScrollYDown;
    public bool ScrollXYPositive;
    public bool ScrollXYNegative;

    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update () {

        float offset = Time.time * scrollSpeed;

        if (ScrollXRight)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        else if (ScrollYUp)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
        else if (ScrollXYPositive)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
        }
        else if (ScrollXLeft)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
        }
        else if (ScrollYDown)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
        }
        else
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, -offset));
        }


    }
}
