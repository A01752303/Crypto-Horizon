using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControl : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 0;
    private float[] pos;
    private int posisi = 0;
    private bool isDragging = false; // Detecta si el usuario está moviendo manualmente

    void Start()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    public void Next()
    {
        if (posisi < pos.Length - 1)
        {
            posisi += 1;
            scrollbar.GetComponent<Scrollbar>().value = pos[posisi];
        }
    }

    public void Prev()
    {
        if (posisi > 0)
        {
            posisi -= 1;
            scrollbar.GetComponent<Scrollbar>().value = pos[posisi];
        }
    }

    void Update()
    {
        scroll_pos = scrollbar.GetComponent<Scrollbar>().value;

        // Si el usuario está presionando el mouse, detectamos que está moviendo manualmente
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
        }
        else if (isDragging) // Cuando suelta el mouse, fijamos la diapositiva más cercana
        {
            isDragging = false;
            SnapToNearestSlide();
        }
    }

    void SnapToNearestSlide()
    {
        float minDistance = Mathf.Abs(scroll_pos - pos[0]);
        int closestIndex = 0;

        // Buscar la posición más cercana
        for (int i = 1; i < pos.Length; i++)
        {
            float distance = Mathf.Abs(scroll_pos - pos[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        // Fijar la diapositiva en la posición más cercana
        posisi = closestIndex;
        scrollbar.GetComponent<Scrollbar>().value = pos[posisi];
    }
}
