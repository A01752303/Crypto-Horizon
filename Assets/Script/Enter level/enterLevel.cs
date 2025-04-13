using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace Nivel1
{
    public class enterLevel : MonoBehaviour
    {
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
                SceneControl.instance.NextLevel();
            }
        }
    }
}
