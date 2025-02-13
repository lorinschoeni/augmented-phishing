using UnityEngine;

namespace PhishAR.Core.Services.Examples
{
    public class DemoServiceLogger : IDemoService
    {
        private IDemoService _wrappedService;

        public DemoServiceLogger(IDemoService demoService)
        {
            _wrappedService = demoService;
        }

        public int ExampleFunction()
        {
            Debug.Log("Generating a random number");
            return _wrappedService.ExampleFunction();
        }
    }
}
