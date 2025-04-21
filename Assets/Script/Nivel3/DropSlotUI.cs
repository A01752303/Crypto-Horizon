using UnityEngine;
using System.Collections;
using Nivel3Block;

namespace Nivel3DropSlot
{
    public class DropSlotUI : MonoBehaviour
    {
        public string expectedID;
        public System.Action onCorrectPlacement;

        private void OnTriggerEnter2D(Collider2D other)
        {
            BlockUI block = other.GetComponent<BlockUI>();

            if (block != null)
            {
                Animator animator = block.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.ResetTrigger("Green");
                    animator.ResetTrigger("Red");

                    if (block.blockID == expectedID)
                    {
                        block.transform.SetParent(transform);

                        RectTransform rt = block.GetComponent<RectTransform>();
                        rt.anchoredPosition = Vector2.zero;

                        block.DisableDrag();

                        animator.SetTrigger("Green");

                        StartCoroutine(ReturnToIdle(animator));

                        onCorrectPlacement?.Invoke();
                    }
                    else
                    {
                        block.ReturnToOriginal();

                        animator.SetTrigger("Red");

                        StartCoroutine(ReturnToIdle(animator));
                    }
                }
            }
        }

        private IEnumerator ReturnToIdle(Animator animator)
        {
            yield return new WaitForSeconds(1f);
        }
    }
}
