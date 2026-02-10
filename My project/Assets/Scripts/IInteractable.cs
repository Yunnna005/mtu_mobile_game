using UnityEngine;

public interface IInteractable 
{
    void YouHaveBeenSelected();
    void YouHaveBeenUnselected();
    void Move(Vector2 position);
    void Rotate();
    void Scale(float delta);
}
