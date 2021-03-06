﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gifts : MonoBehaviour
{

    public GameManager GameManager;
    public PlayerHealth PlayerHealth;
    public PlayerMovement PlayerMovement;
    public PlayerShooting PlayerShooting;

    public enum Buffs {
        moreLife,
        moreSpeed,
        dealMoreDamage

    }

    public enum Debuffs {
        lessLife,
        lessSpeed,
        lessFireRate
        //newEnemy
    }

    public void Start() {
        this.PlayerShooting = (PlayerShooting) FindObjectsOfType(typeof(PlayerShooting))[0];
        this.PlayerMovement = (PlayerMovement) FindObjectsOfType(typeof(PlayerMovement))[0];
        this.PlayerHealth = (PlayerHealth) FindObjectsOfType(typeof(PlayerHealth))[0];
        this.GameManager = (GameManager) FindObjectsOfType(typeof(GameManager))[0];
    }

    public List<Button> choiceButtons;
    Dictionary<Button,Buffs> choiceBuffs = new Dictionary<Button,Buffs>();
    Dictionary<Button,Debuffs> choiceDebuffs = new Dictionary<Button,Debuffs>();

    /// <summary>
    /// Aleatoriza um buff
    /// </summary>
    /// <returns>buff</returns>
    public Buffs RandomizeBuff(){
        int enumLen = Enum.GetNames(typeof(Buffs)).Length;
        int rand = UnityEngine.Random.Range(0,enumLen);

        return (Buffs) rand;
    }

    /// <summary>
    /// Aleatoriza um debuff
    /// </summary>
    /// <returns>debuff</returns>
    public Debuffs RandomizeDebuff(){
        int enumLen = Enum.GetNames(typeof(Debuffs)).Length;
        int rand = UnityEngine.Random.Range(0,enumLen);

        return (Debuffs) rand;
    }

    /// <summary>
    /// Gera strings dos botões
    /// </summary>
    /// <param name="buff">buff que ocorre quando o botão é pressionado</param>
    /// <param name="debuff">debuff que ocorre quando o botão é pressionado</param>
    /// <returns></returns>
    public String GenerateButtonText(Buffs buff, Debuffs debuff){
        string buffStr, debuffStr;

        switch(buff){
            case Buffs.moreLife:
                buffStr = "starting with one more life";
                break;
            case Buffs.moreSpeed:
                buffStr = "faster movement speed";
                break;
            case Buffs.dealMoreDamage:
                buffStr = "higher damage";
                break;
            default:
                Debug.LogError("Buff inválido");
                buffStr = "";
                break;
        }

        switch(debuff){
            case Debuffs.lessLife:
                debuffStr = "Start with one less life";
                break;
            case Debuffs.lessSpeed:
                debuffStr = "Sacrifice movement speed";
                break;
            case Debuffs.lessFireRate:
                debuffStr = "Sacrifice fire rate";
                break;
            default:
                Debug.LogError("Debuff inválido");
                debuffStr = "";
                break;           
        }

        return debuffStr + " for the blessing of " + buffStr;
    }

    /// <summary>
    /// Gera todos os efeitos dos botões
    /// </summary>
    public void GenerateChoices(){
        //define buffs e debuffs
        for(int i=0; i<this.choiceButtons.Count; i++){
            Button btn = this.choiceButtons[i];
            //regera buffs e debuffs até que eles não sejam incompatíveis
            while(true){
                Buffs buff = RandomizeBuff();
                Debuffs debuff = RandomizeDebuff();

                bool isRepeated = false;
                for(int j=0; j<i; j++) {
                    if(this.choiceBuffs[this.choiceButtons[j]] == buff && this.choiceDebuffs[this.choiceButtons[j]] == debuff) {
                        isRepeated = true;
                        break;
                    }
                }
                if(isRepeated) {
                    continue;
                }

                //checa se buff e debuff são incompatíveis
                if(!(
                    (buff == Buffs.moreLife && debuff == Debuffs.lessLife) ||
                    (buff == Buffs.moreSpeed && debuff == Debuffs.lessSpeed)
                )){
                    //atualiza dicionários de efeitos
                    choiceBuffs.Add(btn, buff);
                    choiceDebuffs.Add(btn, debuff);

                    btn.transform.GetComponentInChildren<Text>().text = GenerateButtonText(buff, debuff); //gera texto do botão
                    break; //sai do while
                }
            }
        }
    }

    /// <summary>
    /// Aplica o efeito do buff
    /// </summary>
    /// <param name="effect">buff</param>
    public void ApplyBuff(Buffs effect){
        switch(effect){
            case Buffs.moreLife:
                PlayerHealth.maxHp++;
                PlayerHealth.RegenerateHp(PlayerHealth.maxHp);
                break;
            case Buffs.moreSpeed:
                PlayerMovement.IncreaseSpeed();
                break;
            case Buffs.dealMoreDamage:
                PlayerShooting.damage += 0.5f;
                break;
            default:
                Debug.LogError("Buff inválido");
                break;
        }
    }

    /// <summary>
    /// Aplica o efeito do debuff
    /// </summary>
    /// <param name="effect">debuff</param>
    public void ApplyDebuff(Debuffs effect){
        switch(effect){
            case Debuffs.lessLife:
                PlayerHealth.maxHp--;
                PlayerHealth.RegenerateHp(PlayerHealth.maxHp);
                break;
            case Debuffs.lessSpeed:
                PlayerMovement.DecreaseSpeed();
                break;
            case Debuffs.lessFireRate:
                PlayerShooting.fireRate += 0.1f;
                break;
            default:
                Debug.LogError("Debuff inválido");
                break;
        }
    }

    /// <summary>
    /// OnClick do botão escolhido
    /// </summary>
    /// <param name="btn">referencia para o botão</param>
    public AudioSource gift;
    public void Choose(Button btn){
        gift.Play();
        ApplyBuff(choiceBuffs[btn]);
        ApplyDebuff(choiceDebuffs[btn]);

        GameManager.SpawnNextBoss();

        transform.parent.gameObject.SetActive(false);
    }

    public void OnEnable(){
        Debug.Log("More choices!");
        GenerateChoices();
    }
}
