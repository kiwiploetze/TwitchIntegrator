using ICities;

namespace TwitchIntegrator
{
    public class ThreadingExtension : ThreadingExtensionBase
    {
        public override void OnAfterSimulationFrame()
        {
            TwitchNames.Update();
        }
    }
}
