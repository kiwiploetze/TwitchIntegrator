using ICities;

namespace TwitchIntegrator
{
    public class ThreadingExtension : ThreadingExtensionBase
    {
        public override void OnAfterSimulationTick()
        {
            TwitchNames.Update();
        }
    }
}
