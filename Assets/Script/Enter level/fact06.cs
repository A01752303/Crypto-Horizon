using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

namespace fact06
{
    public class fact : MonoBehaviour
    {
        [SerializeField] private GameObject r;
        private bool playerInTrigger = false;
        [SerializeField] private GameObject banner1;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                r.SetActive(true);
                playerInTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                r.SetActive(false);
                playerInTrigger = false;
            }
        }

        private void Update()
        {
            if (playerInTrigger && Input.GetKeyDown(KeyCode.R))
            {
                banner1.SetActive(true);
            }
        }

        public void exitFact()
        {
            banner1.SetActive(false);
        }
    }
}
