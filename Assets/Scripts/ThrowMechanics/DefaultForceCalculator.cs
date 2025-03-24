namespace ThrowMechanics
{
    public class DefaultForceCalculator : IEffectiveForceCalculator
    {
        private const float minMultiplier = 0.5f;
        private const float maxMultiplier = 1.5f;
        private const float baseline = 100f;

        public float CalculateForce(float playerForce, float pogPower)
        {
            return playerForce * (minMultiplier + (pogPower / baseline) * (maxMultiplier - minMultiplier));
        }
    }
}

