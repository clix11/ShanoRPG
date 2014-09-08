﻿using Engine.Systems;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Engine.Objects
{
    [ProtoContract]
    [ProtoInclude(10, typeof(Hero))]
    [ProtoInclude(11, typeof(Creature))]
    public abstract class Entity
    {
        [ProtoMember(0)]
        public readonly string Name = "Pesho";
        public abstract int Level { get; }

        public double CurrentAttackCooldown
        {
            get { return 1 / CurrentAttacksPerSecond;  }
        }
        

        public double CurrentAttacksPerSecond { get; protected set; }
        public double CurrentMinDamage { get; protected set; }
        public double CurrentDefense { get; protected set; }
        public double CurrentDodge { get; protected set; }
        public double CurrentMaxLife { get; protected set; }
        public double CurrentMaxMana { get; protected set; }
        public double CurrentMaxDamage { get; protected set; }
        public double CurrentMoveSpeed { get; protected set; }

        public double X { get; protected set; }
        public double Y { get; protected set; }

        public List<Buff> Buffs = new List<Buff>();


        [ProtoMember(1)]
        public double CurrentLife;

        [ProtoMember(2)]
        public double CurrentMana;

        [ProtoMember(3)]
        public double BaseLife;

        [ProtoMember(4)]
        public double BaseMana;

        [ProtoMember(5)]
        public double BaseDodge;

        [ProtoMember(6)]
        public double BaseDefense;

        [ProtoMember(7)]
        public double BaseMoveSpeed;

        [ProtoMember(8)]
        public double BaseMinDamage;

        [ProtoMember(9)]
        public double BaseMaxDamage;

        [ProtoMember(10)]
        public double BaseAttacksPerSecond;

        protected Entity()
        {

        }

        public Entity(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Updates the entity's buffs
        /// </summary>
        /// <param name="secondsElapsed">the time elapsed since the last update, in seconds</param>
        public virtual void UpdateBuffs(double secondsElapsed)
        {
            //Here we reset the stats
            CurrentDefense = BaseDefense;
            CurrentDodge = BaseDodge;
            CurrentMaxLife = BaseLife;
            CurrentMaxMana = BaseMana;
            CurrentMinDamage = BaseMinDamage;
            CurrentMaxDamage = BaseMaxDamage; 
            CurrentMoveSpeed = BaseMoveSpeed;
            CurrentAttacksPerSecond = BaseAttacksPerSecond;

            // Here we update the active buffs
           for(int i = 0;i<Buffs.Count;i++)
           {
               Buff b = Buffs[i];
               CurrentDefense += b.Defense;
               CurrentMaxLife += b.Life;
               CurrentMaxMana += b.Mana;
               CurrentMinDamage += b.MinDamage;
               CurrentMaxDamage += b.MaxDamage;
               CurrentMoveSpeed += BaseMoveSpeed * b.MoveSpeedPerc / 100;
               CurrentAttacksPerSecond += BaseAttacksPerSecond * b.AttackSpeedPerc / 100;
           }

        }
    }
     
}