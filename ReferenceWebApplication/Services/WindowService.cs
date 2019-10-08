using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ReferenceWebApplication.Services
{
    public class WindowService
    {
        private readonly IJSRuntime _js;

        public WindowService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<Size> GetDimensions()
        {
            return await _js.InvokeAsync<Size>("getDimensions");
        }
    }
}
