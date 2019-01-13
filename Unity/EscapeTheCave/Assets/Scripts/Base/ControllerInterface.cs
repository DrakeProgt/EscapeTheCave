internal interface ControllerInterface
{
    bool isEnabled { get; set; }
    void Enable();
    ItemType GetItemType();
}