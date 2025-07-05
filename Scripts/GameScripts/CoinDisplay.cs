using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI coinText; 

    public void OnNotify(int coins)
    {
        coinText.text = coins.ToString();
    }

    private void Start()
    {
        CoinManager.instance.RegisterObserver(this);
    }
}
