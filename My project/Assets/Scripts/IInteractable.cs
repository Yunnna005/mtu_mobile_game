using UnityEngine;

public interface IInteractable 
{
    void YouHaveBeenSelected();
    void YouHaveBeenUnselected();
    void Scale(float delta);
    void Rotate(float angle);
    void Drag(Ray ray);
}
