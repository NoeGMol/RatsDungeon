using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    public static CoinManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private int coin;

    public int Coin => coin;

    // Method to add an observer  
    public void RegisterObserver(IObserver observer) // para agregar observador
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }
    public void UnregisterObserver(IObserver observer) //para remover observador
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void UpdateStatus(int totalCoin)
    {
        coin = totalCoin;
        NotifyObservers();
    }

    public void AddCoins(int newCoins)
    {
        coin += newCoins;
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        for(int i = 0; i< observers.Count; i++)
        {
            observers[i].OnNotify(coin);
        }
    }
}
