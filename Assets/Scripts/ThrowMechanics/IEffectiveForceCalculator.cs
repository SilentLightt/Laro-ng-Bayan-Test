using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
namespace ThrowMechanics
{
    public interface IEffectiveForceCalculator
    {
        float CalculateForce(float playerForce, float pogPower);
    }

}
