using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public struct Card : INetworkSerializeByMemcpy, IEquatable<Card>
{
    public bool Empty;

    public byte Id;
    public byte Side;
    public byte Cost;
    public int Health;
    public int MaxHealth;
    public byte Armor;
    public byte Speed;
    public byte RangedDamage;
    public byte MeleeDamage;
    public byte Regeneration;
    public byte Rage;
    public bool Stun;
    public byte SplashDamage;
    public int Abilities;
    public bool Building;

    private static Card[] CardPrefabs = new Card[50];

    public Card(byte Id)
    {
        Side = 0;
        Cost = 0;
        Health = 0;
        MaxHealth = 0;
        Armor = 0;
        Speed = 0;
        RangedDamage = 0;
        MeleeDamage = 0;
        Regeneration = 0;
        Rage = 0;
        Stun = false;
        SplashDamage = 0;
        Building = false;
        Empty = true;
        if (Id != 0)
        {
            Empty = false;
            this.Id = Id;
            Abilities = 0;
        }
        else
        {
            this.Id = 0;
            Empty = true;
            Abilities = 0;
        }        
    }

    public Card(byte Id, byte Side)
    {
        Empty = true;
        if (CardPrefabs[0].Cost == 0)
        {
            /*1-Солдат*/{
                CardPrefabs[1] = new Card(1);
                CardPrefabs[1].Cost = 1;
                CardPrefabs[1].MaxHealth = 2;
                CardPrefabs[1].Armor = 0;
                CardPrefabs[1].Speed = 1;
                CardPrefabs[1].RangedDamage = 2;
                CardPrefabs[1].MeleeDamage = 1;
                CardPrefabs[1].Regeneration = 0;
                CardPrefabs[1].SplashDamage = 0;
                CardPrefabs[1].Stun = false;
                CardPrefabs[1].Building = false;
            }
            /*2-Барикада*/{
                CardPrefabs[2] = new Card(2);
                CardPrefabs[2].Cost = 1;
                CardPrefabs[2].MaxHealth = 4;
                CardPrefabs[2].Armor = 0;
                CardPrefabs[2].Speed = 0;
                CardPrefabs[2].RangedDamage = 0;
                CardPrefabs[2].MeleeDamage = 0;
                CardPrefabs[2].Regeneration = 0;
                CardPrefabs[2].SplashDamage = 0;
                CardPrefabs[2].Stun = false;
                CardPrefabs[2].Building = true;
                CardPrefabs[2].Abilities= 1;
            }
            /*3-Турель*/{
                CardPrefabs[3] = new Card(3);
                CardPrefabs[3].Cost = 2;
                CardPrefabs[3].MaxHealth = 3;
                CardPrefabs[3].Armor = 0;
                CardPrefabs[3].Speed = 0;
                CardPrefabs[3].RangedDamage = 2;
                CardPrefabs[3].MeleeDamage = 0;
                CardPrefabs[3].Regeneration = 0;
                CardPrefabs[3].SplashDamage = 0;
                CardPrefabs[3].Stun = false;
                CardPrefabs[3].Building = true;
                CardPrefabs[3].Abilities = 1;
            }
            /*4-Диверсантка*/{
                CardPrefabs[4] = new Card(4);
                CardPrefabs[4].Cost = 2;
                CardPrefabs[4].MaxHealth = 1;
                CardPrefabs[4].Armor = 0;
                CardPrefabs[4].Speed = 2;
                CardPrefabs[4].RangedDamage = 1;
                CardPrefabs[4].MeleeDamage = 0;
                CardPrefabs[4].Regeneration = 0;
                CardPrefabs[4].SplashDamage = 0;
                CardPrefabs[4].Stun = false;
                CardPrefabs[4].Building = false;
                CardPrefabs[4].Abilities = 2;
            }
            /*5-Ближник*/{
                CardPrefabs[5] = new Card(5);
                CardPrefabs[5].Cost = 2;
                CardPrefabs[5].MaxHealth = 3;
                CardPrefabs[5].Armor = 0;
                CardPrefabs[5].Speed = 2;
                CardPrefabs[5].RangedDamage = 0;
                CardPrefabs[5].MeleeDamage = 3;
                CardPrefabs[5].Regeneration = 0;
                CardPrefabs[5].SplashDamage = 0;
                CardPrefabs[5].Stun = false;
                CardPrefabs[5].Building = false;
                CardPrefabs[5].Abilities = 3;
            }
            /*6-Дробовик*/{
                CardPrefabs[6] = new Card(6);
                CardPrefabs[6].Cost = 3;
                CardPrefabs[6].MaxHealth = 2;
                CardPrefabs[6].Armor = 0;
                CardPrefabs[6].Speed = 1;
                CardPrefabs[6].RangedDamage = 1;
                CardPrefabs[6].MeleeDamage = 1;
                CardPrefabs[6].Regeneration = 0;
                CardPrefabs[6].SplashDamage = 1;
                CardPrefabs[6].Stun = false;
                CardPrefabs[6].Building = false;
                CardPrefabs[6].Abilities = 3;
            }
            /*7-Радист*/{
                CardPrefabs[7] = new Card(7);
                CardPrefabs[7].Cost = 4;
                CardPrefabs[7].MaxHealth = 2;
                CardPrefabs[7].Armor = 0;
                CardPrefabs[7].Speed = 1;
                CardPrefabs[7].RangedDamage = 2;
                CardPrefabs[7].MeleeDamage = 1;
                CardPrefabs[7].Regeneration = 0;
                CardPrefabs[7].SplashDamage = 0;
                CardPrefabs[7].Stun = false;
                CardPrefabs[7].Building = false;
            }
            /*8-Светошумовой гренадёр*/{
                CardPrefabs[8] = new Card(8);
                CardPrefabs[8].Cost = 2;
                CardPrefabs[8].MaxHealth = 2;
                CardPrefabs[8].Armor = 0;
                CardPrefabs[8].Speed = 1;
                CardPrefabs[8].RangedDamage = 0;
                CardPrefabs[8].MeleeDamage = 1;
                CardPrefabs[8].Regeneration = 0;
                CardPrefabs[8].SplashDamage = 0;
                CardPrefabs[8].Stun = true;
                CardPrefabs[8].Building = false;
            }
            /*9-Гренадёр*/{
                CardPrefabs[9] = new Card(9);
                CardPrefabs[9].Cost = 4;
                CardPrefabs[9].MaxHealth = 2;
                CardPrefabs[9].Armor = 0;
                CardPrefabs[9].Speed = 1;
                CardPrefabs[9].RangedDamage = 3;
                CardPrefabs[9].MeleeDamage = 1;
                CardPrefabs[9].Regeneration = 0;
                CardPrefabs[9].SplashDamage = 1;
                CardPrefabs[9].Stun = true;
                CardPrefabs[9].Building = false;
            }
            /*10-Огнемётчик*/{
                CardPrefabs[10] = new Card(10);
                CardPrefabs[10].Cost = 3;
                CardPrefabs[10].MaxHealth = 2;
                CardPrefabs[10].Armor = 0;
                CardPrefabs[10].Speed = 1;
                CardPrefabs[10].RangedDamage = 3;
                CardPrefabs[10].MeleeDamage = 1;
                CardPrefabs[10].Regeneration = 0;
                CardPrefabs[10].SplashDamage = 0;
                CardPrefabs[10].Stun = false;
                CardPrefabs[10].Building = false;
                CardPrefabs[10].Abilities = 3;
                CardPrefabs[10].Abilities = 3;
            }
            /*11-Снайпер*/{
                CardPrefabs[11] = new Card(11);
                CardPrefabs[11].Cost = 4;
                CardPrefabs[11].MaxHealth = 2;
                CardPrefabs[11].Armor = 0;
                CardPrefabs[11].Speed = 1;
                CardPrefabs[11].RangedDamage = 3;
                CardPrefabs[11].MeleeDamage = 1;
                CardPrefabs[11].Regeneration = 0;
                CardPrefabs[11].SplashDamage = 0;
                CardPrefabs[11].Stun = false;
                CardPrefabs[11].Building = false;
                CardPrefabs[11].Abilities = 3;
            }
            /*12-Пулемётчик*/{
                CardPrefabs[12] = new Card(12);
                CardPrefabs[12].Cost = 4;
                CardPrefabs[12].MaxHealth = 2;
                CardPrefabs[12].Armor = 0;
                CardPrefabs[12].Speed = 1;
                CardPrefabs[12].RangedDamage = 2;
                CardPrefabs[12].MeleeDamage = 1;
                CardPrefabs[12].Regeneration = 0;
                CardPrefabs[12].SplashDamage = 0;
                CardPrefabs[12].Stun = false;
                CardPrefabs[12].Building = false;
                CardPrefabs[12].Abilities = 3;
            }
            /*13-Полевой медик*/{
                CardPrefabs[13] = new Card(13);
                CardPrefabs[13].Cost = 3;
                CardPrefabs[13].MaxHealth = 2;
                CardPrefabs[13].Armor = 0;
                CardPrefabs[13].Speed = 2;
                CardPrefabs[13].RangedDamage = 1;
                CardPrefabs[13].MeleeDamage = 1;
                CardPrefabs[13].Regeneration = 0;
                CardPrefabs[13].SplashDamage = 0;
                CardPrefabs[13].Stun = false;
                CardPrefabs[13].Building = false;
                CardPrefabs[13].Abilities = 2;
                CardPrefabs[13].Abilities = 3;
            }
            /*14-Тяжёлая пехота*/{
                CardPrefabs[14] = new Card(14);
                CardPrefabs[14].Cost = 4;
                CardPrefabs[14].MaxHealth = 3;
                CardPrefabs[14].Armor = 1;
                CardPrefabs[14].Speed = 1;
                CardPrefabs[14].RangedDamage = 2;
                CardPrefabs[14].MeleeDamage = 1;
                CardPrefabs[14].Regeneration = 0;
                CardPrefabs[14].SplashDamage = 0;
                CardPrefabs[14].Stun = false;
                CardPrefabs[14].Building = false;
                CardPrefabs[14].Abilities = 1;
            }
            /*15-Знамя*/{
                CardPrefabs[15] = new Card(15);
                CardPrefabs[15].Cost = 3;
                CardPrefabs[15].MaxHealth = 3;
                CardPrefabs[15].Armor = 0;
                CardPrefabs[15].Speed = 0;
                CardPrefabs[15].RangedDamage = 0;
                CardPrefabs[15].MeleeDamage = 0;
                CardPrefabs[15].Regeneration = 0;
                CardPrefabs[15].SplashDamage = 0;
                CardPrefabs[15].Stun = false;
                CardPrefabs[15].Building = true;
            }
            /*16-Инженер*/{
                CardPrefabs[16] = new Card(16);
                CardPrefabs[16].Cost = 3 ;
                CardPrefabs[16].MaxHealth = 2;
                CardPrefabs[16].Armor = 0;
                CardPrefabs[16].Speed = 1;
                CardPrefabs[16].RangedDamage = 0;
                CardPrefabs[16].MeleeDamage = 1;
                CardPrefabs[16].Regeneration = 0;
                CardPrefabs[16].SplashDamage = 0;
                CardPrefabs[16].Stun = false;
                CardPrefabs[16].Building = false;
                CardPrefabs[16].Abilities = 3;
                CardPrefabs[16].Abilities = 3;
            }
            /*17-Командир*/{
                CardPrefabs[17] = new Card(17);
                CardPrefabs[17].Cost = 5;
                CardPrefabs[17].MaxHealth = 2;
                CardPrefabs[17].Armor = 1;
                CardPrefabs[17].Speed = 1;
                CardPrefabs[17].RangedDamage = 2;
                CardPrefabs[17].MeleeDamage = 1;
                CardPrefabs[17].Regeneration = 1;
                CardPrefabs[17].SplashDamage = 0;
                CardPrefabs[17].Stun = false;
                CardPrefabs[17].Building = false;
                CardPrefabs[17].Abilities = 3;
                CardPrefabs[17].Abilities = 3;   
            }
            /*18-Джаггернаут*/{
                CardPrefabs[18] = new Card(18);
                CardPrefabs[18].Cost = 7;
                CardPrefabs[18].MaxHealth = 5;
                CardPrefabs[18].Armor = 2;
                CardPrefabs[18].Speed = 1;
                CardPrefabs[18].RangedDamage = 1;
                CardPrefabs[18].MeleeDamage = 2;
                CardPrefabs[18].Regeneration = 0;
                CardPrefabs[18].SplashDamage = 0;
                CardPrefabs[18].Stun = false;
                CardPrefabs[18].Building = false;
                CardPrefabs[18].Abilities = 3;
                CardPrefabs[18].Abilities = 3;
            }
            /*19-Химик*/{
                CardPrefabs[19] = new Card(19);
                CardPrefabs[19].Cost = 3;
                CardPrefabs[19].MaxHealth = 2;
                CardPrefabs[19].Armor = 0;
                CardPrefabs[19].Speed = 1;
                CardPrefabs[19].RangedDamage = 1;
                CardPrefabs[19].MeleeDamage = 1;
                CardPrefabs[19].Regeneration = 0;
                CardPrefabs[19].SplashDamage = 0;
                CardPrefabs[19].Stun = false;
                CardPrefabs[19].Building = false;
                CardPrefabs[19].Abilities = 3;
            }

        }
        if (Id != 0)
        {
            this.Id = Id;
            this.Cost = CardPrefabs[Id].Cost;
            this.MaxHealth = CardPrefabs[Id].MaxHealth;
            this.Health = CardPrefabs[Id].MaxHealth;
            this.Armor = CardPrefabs[Id].Armor;
            this.Speed = CardPrefabs[Id].Speed;
            this.RangedDamage = CardPrefabs[Id].RangedDamage;
            this.MeleeDamage = CardPrefabs[Id].MeleeDamage;
            this.Regeneration = CardPrefabs[Id].Regeneration;
            this.SplashDamage = CardPrefabs[Id].SplashDamage;
            this.Stun = CardPrefabs[Id].Stun;
            this.Building = CardPrefabs[Id].Building;
            this.Abilities = CardPrefabs[Id].Abilities;
            this.Rage = 0;
            Empty = false;
        }
        else
        {
            this.Id = Id;
            this.Cost = 0;
            this.MaxHealth = 0;
            this.Health = 0;
            this.Armor = 0;
            this.Speed = 0;
            this.RangedDamage = 0;
            this.MeleeDamage = 0;
            this.Regeneration = 0;
            this.SplashDamage = 0;
            this.Stun = false;
            this.Building = false;
            this.Abilities = 0;
            this.Rage = 0;
            Empty = true;
        }
        this.Side = Side;

    }
    public override string ToString() 
    {
        string str = new("");
        str += this.Id >= 10 ? this.Id.ToString() : "0" + this.Id.ToString();
        str += System.Convert.ToInt32(this.Empty);
        str += this.Side;
        str += this.Cost >= 10 ? this.Cost.ToString() : "0" + this.Cost.ToString();
        str += this.Health >= 10 ? this.Health.ToString() : "0" + this.Health.ToString();
        str += this.MaxHealth >= 10 ? this.MaxHealth.ToString() : "0" + this.MaxHealth.ToString();
        str += this.Armor;
        str += this.Speed;
        str += this.RangedDamage;
        str += this.MeleeDamage;
        str += this.Regeneration;
        str += this.Rage;
        str += System.Convert.ToInt32(this.Stun);
        str += this.SplashDamage;
        str += this.Abilities;
        str += System.Convert.ToInt32(this.Building);
        return str;
    }

    public void FromString(string str)
    {
        this.Id = (byte)Convert.ToInt32(str.Substring(0,2));
        this.Empty = Convert.ToBoolean(Convert.ToInt32(str.Substring(2,1)));
        this.Side = (byte)Convert.ToInt32(str.Substring(3, 1));
        this.Cost = (byte)Convert.ToInt32(str.Substring(4, 2));
        this.Health = (byte)Convert.ToInt32(str.Substring(6, 2));
        this.MaxHealth = (byte)Convert.ToInt32(str.Substring(8, 2));        
        this.Armor = (byte)Convert.ToInt32(str.Substring(10, 1));
        this.Speed = (byte)Convert.ToInt32(str.Substring(11, 1));
        this.RangedDamage = (byte)Convert.ToInt32(str.Substring(12, 1));
        this.MeleeDamage = (byte)Convert.ToInt32(str.Substring(13, 1));
        this.Regeneration = (byte)Convert.ToInt32(str.Substring(14, 1));
        this.Rage = (byte)Convert.ToInt32(str.Substring(15, 1));
        this.Stun = Convert.ToBoolean(Convert.ToInt32(str.Substring(16, 1)));
        this.SplashDamage = (byte)Convert.ToInt32(str.Substring(17, 1)); 
        this.Abilities = (byte)Convert.ToInt32(str.Substring(18, 1));
        this.Building = Convert.ToBoolean(Convert.ToInt32(str.Substring(19, 1)));
    }

    public bool Equals(Card other)
    {
        return (this.Side == other.Side && this.Id == other.Id && this.Abilities == other.Abilities && this.Health == other.Health && this.Rage == other.Rage);
    }
}
