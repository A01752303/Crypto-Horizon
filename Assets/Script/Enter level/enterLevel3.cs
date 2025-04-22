using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

namespace Nivel3
{
    public class enterLevel : MonoBehaviour
    {
        public Animator transition;
        [SerializeField] private GameObject enterButton;
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
            if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Animation());
            }
        }

        IEnumerator Animation()
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(7);
        }
    }
}
