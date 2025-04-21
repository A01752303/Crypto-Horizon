using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Nivel3Block;
using Nivel3DropSlot;
using UnityEngine.UI;

namespace Nivel3
{
    public class BlockManager : MonoBehaviour
    {
        public DropSlotUI[] slots;
        public TextMeshProUGUI orderText;
        public GameObject successCanvas;

        private BlockUI[] blocks;
        private int correctCount = 0;

        private void Awake()
        {
            blocks = FindObjectsOfType<BlockUI>();

            if (blocks == null || blocks.Length == 0)
                Debug.LogError("No se han encontrado bloques en la escena.");

            if (slots == null || slots.Length == 0)
                Debug.LogError("No se han asignado slots en el Inspector.");

            List<string> ids = GenerateUniqueIDs(blocks.Length);
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].SetID(ids[i]);
            }

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].expectedID = ids[i];
                slots[i].onCorrectPlacement += OnBlockCorrectlyPlaced;
            }

            if (orderText != null)
            {
                orderText.text = "Order the Blockchain with this hash sequence: " + string.Join("-", ids);
            }
            else
            {
                Debug.LogError("El campo 'orderText' no está asignado en el Inspector.");
            }

            if (successCanvas != null)
                successCanvas.SetActive(false);
        }

        private void Start()
        {
            foreach (var block in blocks)
            {
                if (block == null)
                    Debug.LogError("Un bloque no está asignado correctamente en la escena.");
            }

            foreach (var slot in slots)
            {
                if (slot == null)
                    Debug.LogError("Un slot no está asignado correctamente en el Inspector.");
            }
        }

        private List<string> GenerateUniqueIDs(int count)
        {
            HashSet<string> generated = new HashSet<string>();
            List<string> ids = new List<string>();

            while (ids.Count < count)
            {
                string id = Random.Range(1000, 9999).ToString();
                if (generated.Add(id))
                {
                    ids.Add(id);
                }
            }

            return ids;
        }

        private void OnBlockCorrectlyPlaced()
        {
            correctCount++;

            if (correctCount >= slots.Length)
            {
                successCanvas.SetActive(true);
            }
        }
    }
}
