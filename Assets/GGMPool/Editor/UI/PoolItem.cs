using GGMPool;
using System;
using UnityEngine.UIElements;

public class PoolItem
{
    private Label _nameLabel;
    private Button _deleteBtn;
    private VisualElement _rootElem;

    public event Action<PoolItem> OnDeleteEvent;
    public event Action<PoolItem> OnSelectEvent;

    public string Name
    {
        get => _nameLabel.text;
        set => _nameLabel.text = value;
    }

    public PoolingItemSO itemSO;

    public bool IsActive
    {
        get => _rootElem.ClassListContains("active");
        set
        {
            if (value)
                _rootElem.AddToClassList("active");
            else
                _rootElem.RemoveFromClassList("active");
        }
    }

    public PoolItem(VisualElement root, PoolingItemSO itemSO)
    {
        this.itemSO = itemSO;
        _rootElem = root.Q("PoolItem");
        _nameLabel = _rootElem.Q<Label>("ItemName");
        _deleteBtn = _rootElem.Q<Button>("BtnDelete");
        _deleteBtn.RegisterCallback<ClickEvent>(evt =>
        {
            OnDeleteEvent?.Invoke(this);
            evt.StopPropagation(); // 이벤트 전파를 멈춰서 셀렉트가 안일어나게 한다.
        });

        _rootElem.RegisterCallback<ClickEvent>(evt =>
        {
            OnSelectEvent?.Invoke(this);
            evt.StopPropagation();
        });
    }
}
