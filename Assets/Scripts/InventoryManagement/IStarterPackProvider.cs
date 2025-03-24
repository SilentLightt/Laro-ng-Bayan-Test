using System.Collections.Generic;
using UnityEngine;

public interface IStarterPackProvider
{
    List<Pog> GetStarterPack();
    bool HasStarterPackOpened();
    void MarkStarterPackOpened();
}
