using System;
using Foundations.Scripts.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace GameAssets.Scripts.UI.PlayerStorageView
{
#if TM_PRO
    using TMPro;
#endif
    public class DynamicPlayerStorageItem : MonoBehaviour
    {
#if TM_PRO
        [SerializeField] private TextMeshProUGUI productName;
        [SerializeField] private TextMeshProUGUI productAmount;
#endif
        [SerializeField] private Image icon;

        public void Setup(ResourceSetup resourceSetup, IObservable<int> reactiveAmount)
        {
#if TM_PRO
            productName.text = productName;
            reactiveAmount
                .SubscribeWithState(productAmount, (amount, amountText) => amountText.text = amount.ToString())
                .AddTo(gameObject);
#endif

            if (resourceSetup.Icon)
                icon.sprite = resourceSetup.Icon;
        }
    }
}