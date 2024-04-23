using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleTests
{

    // [SetUp]
    // public void SetUp()
    // {
    //     List<Card> Attacker = new List<Card>();
    //     List<Card> Defender = new List<Card>();
    //     Player playerAttacking = new Player("Bob", null);
    //     Player playerDefending = new Player("John", null);
    //     Battles battles = new Battles();

    // }


    [Test]
    public void CalculateWinner_AttackSideHasMoreCards()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();


        Attacker.Add(new Card(null, "Card1A"));
        Attacker[0].setOffense(6);
        Attacker[0].setDefense(10);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Attacker.Add(new Card(null, "Card2A"));
        Attacker[1].setOffense(3);
        Attacker[1].setDefense(18);
        Attacker[1].setImmunityChance(0.0f);
        Attacker[1].setEfficency(0.0f);


        Attacker.Add(new Card(null, "Card3A"));
        Attacker[2].setOffense(8);
        Attacker[2].setDefense(7);
        Attacker[2].setImmunityChance(0.0f);
        Attacker[2].setEfficency(0.0f);


        Defender.Add(new Card(null, "Card1D"));
        Defender[0].setOffense(6);
        Defender[0].setDefense(10);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Card2D"));
        Defender[1].setOffense(6);
        Defender[1].setDefense(10);
        Defender[1].setImmunityChance(0.0f);
        Defender[1].setEfficency(0.0f);

        Assert.IsTrue(Attacker.Count > Defender.Count, "Attacking players card count is greater so player " + playerAttacking.GetName() + " will attack first.");

        battles.CalculateWinner(Attacker, Defender, playerAttacking, playerDefending);
    }


    [Test]
    public void CalculateWinner_DefenseSideHasMoreCards()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();


        Attacker.Add(new Card(null, "Card1A"));
        Attacker[0].setOffense(12);
        Attacker[0].setDefense(100);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Attacker.Add(new Card(null, "Card2A"));
        Attacker[1].setOffense(10);
        Attacker[1].setDefense(50);
        Attacker[1].setImmunityChance(0.0f);
        Attacker[1].setEfficency(0.0f);


        Defender.Add(new Card(null, "Card1D"));
        Defender[0].setOffense(10);
        Defender[0].setDefense(50);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Card2D"));
        Defender[1].setOffense(2);
        Defender[1].setDefense(25);
        Defender[1].setImmunityChance(0.0f);
        Defender[1].setEfficency(0.0f);

        
        Defender.Add(new Card(null, "Card3D"));
        Defender[2].setOffense(15);
        Defender[2].setDefense(40);
        Defender[2].setImmunityChance(0.0f);
        Defender[2].setEfficency(0.0f);

        Assert.IsTrue(Attacker.Count < Defender.Count, "Defensive players card count is greater so player " + playerDefending.GetName() + " will attack first.");

        battles.CalculateWinner(Attacker, Defender, playerAttacking, playerDefending);
      
    }


    [Test]
    public void CalculateWinner_EachSideHasSameCards()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();
    
        Attacker.Add(new Card(null, "Card1A"));
        Attacker[0].setOffense(6);
        Attacker[0].setDefense(100);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Attacker.Add(new Card(null, "Card2A"));
        Attacker[1].setOffense(3);
        Attacker[1].setDefense(20);
        Attacker[1].setImmunityChance(0.0f);
        Attacker[1].setEfficency(0.0f);


        Attacker.Add(new Card(null, "Card3A"));
        Attacker[2].setOffense(1);
        Attacker[2].setDefense(15);
        Attacker[2].setImmunityChance(0.0f);
        Attacker[2].setEfficency(0.0f);

    
      
        Defender.Add(new Card(null, "Card1D"));
        Defender[0].setOffense(10);
        Defender[0].setDefense(60);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Card2D"));
        Defender[1].setOffense(15);
        Defender[1].setDefense(85);
        Defender[1].setImmunityChance(0.0f);
        Defender[1].setEfficency(0.0f);

        
        Defender.Add(new Card(null, "Card3D"));
        Defender[2].setOffense(25);
        Defender[2].setDefense(140);
        Defender[2].setImmunityChance(0.0f);
        Defender[2].setEfficency(0.0f);

        Assert.AreEqual(Attacker.Count, Defender.Count);

        battles.CalculateWinner(Attacker, Defender, playerAttacking, playerDefending);
    }


    [Test]
    public void CalculateWinner_DefendeHasNoCards()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();
    
        Attacker.Add(new Card(null, "Card1A"));
        Attacker[0].setOffense(6);
        Attacker[0].setDefense(100);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Attacker.Add(new Card(null, "Card2A"));
        Attacker[1].setOffense(3);
        Attacker[1].setDefense(20);
        Attacker[1].setImmunityChance(0.0f);
        Attacker[1].setEfficency(0.0f);


        Attacker.Add(new Card(null, "Card3A"));
        Attacker[2].setOffense(1);
        Attacker[2].setDefense(15);
        Attacker[2].setImmunityChance(0.0f);
        Attacker[2].setEfficency(0.0f);
    
        battles.CalculateWinner(Attacker, Defender, playerAttacking, playerDefending);

        //Debug.Assert(Defender.Count == 0, "Player " + playerDefending.GetName() + " has no defensive cards so " + playerAttacking.GetName() + " has won the battle.");
        Assert.AreEqual(Defender.Count, 0);
    }



    
    [Test]
    public void Swap()
    {

    List<Card> Attacker = new List<Card>();
    List<Card> Defender = new List<Card>();
    Player playerAttacking = new Player("Bob", null);
    Player playerDefending = new Player("John", null);
    Battles battles = new Battles();

    Attacker.Add(new Card(null, "Attacker1"));
    Attacker[0].setOffense(16);
    Attacker[0].setDefense(100);
    Attacker[0].setImmunityChance(0.0f);
    Attacker[0].setEfficency(0.0f);

    Attacker.Add(new Card(null, "Attacker2"));
    Attacker[1].setOffense(13);
    Attacker[1].setDefense(120);
    Attacker[1].setImmunityChance(0.0f);
    Attacker[1].setEfficency(0.0f);

    Defender.Add(new Card(null, "Defender1"));
    Defender[0].setOffense(10);
    Defender[0].setDefense(60);
    Defender[0].setImmunityChance(0.0f);
    Defender[0].setEfficency(0.0f);

    Defender.Add(new Card(null, "Defender2"));
    Defender[1].setOffense(15);
    Defender[1].setDefense(85);
    Defender[1].setImmunityChance(0.0f);
    Defender[1].setEfficency(0.0f);

        
    Defender.Add(new Card(null, "Defender3"));
    Defender[2].setOffense(25);
    Defender[2].setDefense(140);
    Defender[2].setImmunityChance(0.0f);
    Defender[2].setEfficency(0.0f);

    battles.Swap(ref Attacker, ref Defender, ref playerAttacking, ref playerDefending);

    Assert.AreEqual(2, Defender.Count);
    Assert.AreEqual("Attacker2", Defender[0].GetName());
    Assert.AreEqual("Attacker1", Defender[1].GetName());

    Assert.AreEqual(3, Attacker.Count);
    Assert.AreEqual("Defender3", Attacker[0].GetName());
    Assert.AreEqual("Defender2", Attacker[1].GetName());
    Assert.AreEqual("Defender1", Attacker[2].GetName());

    Assert.AreEqual("Bob", playerDefending.GetName());
    Assert.AreEqual("John", playerAttacking.GetName());
      
    }



    [Test]
    public void CalculateWinner_AttackChanceToDodge()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();
    
        Attacker.Add(new Card(null, "Card1A"));
        Attacker[0].setOffense(22);
        Attacker[0].setDefense(100);
        Attacker[0].setImmunityChance(0.4f);
        Attacker[0].setEfficency(0.0f);

      
        Defender.Add(new Card(null, "Card1D"));
        Defender[0].setOffense(25);
        Defender[0].setDefense(90);
        Defender[0].setImmunityChance(0.2f);
        Defender[0].setEfficency(0.0f);

        battles.CalculateWinner(Attacker, Defender, playerAttacking, playerDefending);
      
    }


    [Test]
    public void CalculateWinner_AttackChanceToKeepAttack()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();
    
        Attacker.Add(new Card(null, "Card1A"));
        Attacker[0].setOffense(18);
        Attacker[0].setDefense(100);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.4f);

        Attacker.Add(new Card(null, "Card2A"));
        Attacker[1].setOffense(24);
        Attacker[1].setDefense(70);
        Attacker[1].setImmunityChance(0.0f);
        Attacker[1].setEfficency(0.2f);

      
        Defender.Add(new Card(null, "Card1D"));
        Defender[0].setOffense(20);
        Defender[0].setDefense(90);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.2f);

        Defender.Add(new Card(null, "Card2D"));
        Defender[1].setOffense(18);
        Defender[1].setDefense(85);
        Defender[1].setImmunityChance(0.0f);
        Defender[1].setEfficency(0.2f);


        battles.CalculateWinner(Attacker, Defender, playerAttacking, playerDefending);
      
    }


    [Test]
    public void Attack_OffenseGreaterThanDefense()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();

        Attacker.Add(new Card(null, "Attacker1"));
        Attacker[0].setOffense(16);
        Attacker[0].setDefense(5);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Defender1"));
        Defender[0].setOffense(10);
        Defender[0].setDefense(14);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Assert.IsTrue(Attacker[0].getOffense() >= Defender[0].getDefense());
        battles.Attack(Attacker, Defender);

        Assert.AreEqual(Attacker.Count, 1);
        Assert.AreEqual(Defender.Count, 0);
        Assert.AreEqual(Attacker[0].getOffense(), 17);

    }



    [Test]
    public void Attack_DefenseGreaterThanOffense()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();

        Attacker.Add(new Card(null, "Attacker1"));
        Attacker[0].setOffense(8);
        Attacker[0].setDefense(25);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Defender1"));
        Defender[0].setOffense(10);
        Defender[0].setDefense(75);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Assert.IsTrue(Attacker[0].getOffense() < Defender[0].getDefense());
        battles.Attack(Attacker, Defender);

        Assert.AreEqual(Attacker.Count, 1);
        Assert.AreEqual(Defender.Count, 1);
        Assert.AreEqual(Attacker[0].getOffense(), 7);
        Assert.AreEqual(Defender[0].getDefense(), 67);
    }



    [Test]
    public void Attack_AttackEqualToDefense()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();

        Attacker.Add(new Card(null, "Attacker1"));
        Attacker[0].setOffense(12);
        Attacker[0].setDefense(25);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Defender1"));
        Defender[0].setOffense(30);
        Defender[0].setDefense(12);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Assert.IsTrue(Attacker[0].getOffense() == Defender[0].getDefense());
        battles.Attack(Attacker, Defender);

        Assert.AreEqual(Attacker.Count, 1);
        Assert.AreEqual(Defender.Count, 0);
        Assert.AreEqual(Attacker[0].getOffense(), 13);
    }



     [Test]
    public void Attack_AttackerRunsOutOfAttack()
    {
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();
        Player playerAttacking = new Player("Bob", null);
        Player playerDefending = new Player("John", null);
        Battles battles = new Battles();

        Attacker.Add(new Card(null, "Attacker1"));
        Attacker[0].setOffense(1);
        Attacker[0].setDefense(25);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Defender1"));
        Defender[0].setOffense(10);
        Defender[0].setDefense(75);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Assert.IsTrue(Attacker[0].getOffense() < Defender[0].getDefense());
        battles.Attack(Attacker, Defender);

        Assert.AreEqual(Attacker.Count, 0);
        Assert.AreEqual(Defender.Count, 1);
        Assert.AreEqual(Defender[0].getDefense(), 74);
    }





    // [TearDown]
    // public void TearDown(){
    //    
    // }

}
