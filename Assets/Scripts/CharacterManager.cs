using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public TMP_Text nameText;
    public SpriteRenderer artworkSprite;
    public int selectedOption = 0;
    public GameObject characterSelectScreen;
    void Start()
    {
        if (!PlayerPrefs.HasKey("SelectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption>=characterDB.CharacterCount)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
        Save();
        
    }
    public void Backption()
    {
        selectedOption--;
        if (selectedOption<0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }
        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("SelectedOption");
    }
    private void Save()
    {
        PlayerPrefs.SetInt("SelectedOption",selectedOption);
        Debug.Log(PlayerPrefs.GetInt("SelectedOption")+" sorun");
    }
    public void OpenCharacterSelect()
    {
        characterSelectScreen.SetActive(true);
    }
    public void CloseCharacterSelect()
    {
        characterSelectScreen.SetActive(false);
    }
    
}
