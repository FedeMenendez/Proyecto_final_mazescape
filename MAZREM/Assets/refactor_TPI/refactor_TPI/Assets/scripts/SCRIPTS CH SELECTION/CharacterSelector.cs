using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters;
    private int selectedCharacterIndex = -1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    if (hit.collider.gameObject == characters[i])
                    {
                        selectedCharacterIndex = i;
                        Debug.Log("Selected character: " + characters[i].name);
                        break;
                    }
                }
            }
        }
    }

    public void ConfirmSelection()
    {
        if (selectedCharacterIndex != -1)
        {
            PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex);
            Debug.Log("Character selection confirmed: " + characters[selectedCharacterIndex].name);
        }
        else
        {
            Debug.LogError("Error: No character selected!");
        }
    }
}
