using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.lightning
{
    public class StaticPointLightningSourceProvider : ILightningSourceProvider
    {
        private ICameraProvider _cameraProvider;

        public StaticPointLightningSourceProvider(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        public ILightningSource Get()
        {
            return new PointLightningSource
            {
                Origin = _cameraProvider.Get().Position
            };
        }
    }
}