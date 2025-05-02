using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

namespace Keys
{
    public class enterLevel : MonoBehaviour
    {
        public Animator transition;
        [SerializeField] private GameObject enterButton;
        [SerializeField] private GameObject keysWindows;
        [SerializeField] private GameObject notEnoughKeysWindows;
        private bool playerInTrigger = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                enterButton.SetActive(true);
                playerInTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                enterButton.SetActive(false);
                playerInTrigger = false;
            }
        }

        private void Update()
        {
            if (playerInTrigger && Input.GetKeyDown(KeyCode.T))
            {
                if (gameManager.Instance.llaves == 3)
                {
                    StartCoroutine(Animation());
                }
                else
                {
                    StartCoroutine(windowNotEnoughKeys());
                }
            }
        }

        IEnumerator Animation()
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("final scene");
        }

        IEnumerator windowNotEnoughKeys()
        {
            keysWindows.SetActive(false);
            notEnoughKeysWindows.SetActive(true);

            yield return new WaitForSeconds(2f);

            notEnoughKeysWindows.SetActive(false);
            keysWindows.SetActive(true);
        }
    }
}
