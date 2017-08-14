using Celebscan.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Celebscan.Service.Tests.Controllers
{
    public class ImageControllerTests
    {
        private readonly ImageController _controller;

        public ImageControllerTests()
        {
            _controller = new ImageController();
        }
        
        [Fact]
        public void ImageControllerReturnsImageForValidData()
        {
            var imageData = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACTUlEQVQ4T2NkoBAwYtP/bfYkiz+vXlf9/vhWg/E" +
                            "/AzMTN99jFnHxuTyZhYvR1aMY8H9RN/e7W88P/D151ITh3z8Ms1k1dV4yG+rb8yUX3IRJwg34P2kS+4db5x/+vn" +
                            "5FHK6TnYPhPyvrf8Yvn8Hq/vMJ/ON293Hjycjfi2HA+/bqLb/37PIGS4iI/eG0cWjgYePrYkxP//1pZn/An2vXJ" +
                            "7FpaiXzZBTsRnYaxOSHFYI/H+16/X2lCPPvx3/+cXsGmnNn5J4hJnzBBvy9mVPx7/HMdgYmbobfX1I2c/n1+ME0" +
                            "92372fqf4T8vumG8nEzz0hzZLoAN+HUpfhnjq2WRIDaTbFYus/rEKTANQf2f/3z4xsiMboCXAcuMEm+OTIgB11I" +
                            "WMD5bGA82QCatglljaichAzz1WWaX+nCkgQ34c7ss+f/D/jn32AwZNjMGnCqxrTKHGTB118/I/wyM3CD+8w//Mo" +
                            "7f/mMMYgeashTkunFMhATi/gaWbezPP7Q9/cT99/9fhhAV98wSo+QZyM6eu+ur1N5b/++/+MjAxsny/3+YMY9ov" +
                            "AvjW3g66Dgzq3vNnZ0lIE1szKwMNpKGh8S4RGcwMPz/8PnnF783X5ij71+M5P3xk53BRpXhUFMYjz1ILUpKrDja" +
                            "fWDP4xNgCWxAikuKQel74SsrMUXlMEfGLxgGgARALjny7Fz+i2+vWZEN4WBmZ7CU1DupwqHknm4S9hEjJSIrbti" +
                            "/n4VP8GHs11/fbP/+/cfOzcp+g4uTf3a2dtgLdJdhzY3EpECYGgDdQOARPjPYcwAAAABJRU5ErkJggg==";
            
            var response = _controller.Get(imageData);

            Assert.IsType<FileContentResult>(response);
        }

        [Fact]
        public void ImageControllerReturns404WithoutData()
        {
            var response = _controller.Get(null);
            
            Assert.IsType<NotFoundResult>(response);
        }
    }
}