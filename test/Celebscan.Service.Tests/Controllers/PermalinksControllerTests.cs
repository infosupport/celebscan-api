using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Celebscan.Service.Controllers;
using Celebscan.Service.Models;
using Celebscan.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using Xunit;

namespace Celebscan.Service.Tests.Controllers
{
    public class PermalinksControllerTests
    {
        private readonly Mock<IPermalinkUrlTranslator> _permalinkUrlTranslator;
        private readonly Mock<IPermalinkGenerator> _permalinkGenerator;
        private readonly Mock<IPermalinkStorage> _permalinkStorage;
        private readonly Mock<IObjectModelValidator> _validator;
        private readonly PermalinksController _controller;

        public PermalinksControllerTests()
        {
            _validator = new Mock<IObjectModelValidator>();
            _permalinkGenerator = new Mock<IPermalinkGenerator>();
            _permalinkStorage = new Mock<IPermalinkStorage>();
            _permalinkUrlTranslator = new Mock<IPermalinkUrlTranslator>();
            
            _controller = new PermalinksController(
                _permalinkGenerator.Object,
                _permalinkUrlTranslator.Object,
                _permalinkStorage.Object);

            _controller.ObjectValidator = _validator.Object;
            
            _permalinkUrlTranslator
                .Setup(mock => mock.Translate(It.IsAny<IUrlHelper>(), It.IsAny<string>()))
                .Returns((IUrlHelper urlHelper, string code) => $"http://localhost:8080/api/permalinks/{code}");

            _permalinkGenerator
                .Setup(mock => mock.Generate(It.IsAny<PermalinkGeneratorParameters>()))
                .ReturnsAsync("bladiebla");
        }

        [Fact]
        public async Task GeneratePermalinkReturnsPermalinkUrl()
        {
            SetupValidator(shouldInvalidateModelState: false);
            
            var result = await _controller.CreatePermalink(new PermalinkGeneratorParameters()
            {
                ImageData = "bla",
                Label = "bla",
                Score = 0.1,
                Scores = new List<ScanResult>()
            });

            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);

            var jsonResult = (JsonResult) result;
            var url = ((PermalinkUrlData) jsonResult.Value).Url;

            Assert.Equal(url, "http://localhost:8080/api/permalinks/bladiebla");
        }

        [Fact]
        public async Task GeneratePermalinkForInvalidInputReturnsBadRequest()
        {
            SetupValidator(shouldInvalidateModelState: true);

            var result = await _controller.CreatePermalink(new PermalinkGeneratorParameters());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetPermalinkReturnsUrl()
        {
            _permalinkStorage
                .Setup(mock => mock.FindByCode(It.IsAny<string>()))
                .ReturnsAsync(new Permalink());
            
            SetupValidator(shouldInvalidateModelState: false);

            var result = await _controller.GetPermalink("bladiebla");

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task GetPermalinkForNonexistingCodeReturnsNotFound()
        {
            _permalinkStorage
                .Setup(mock => mock.FindByCode(It.IsAny<string>()))
                .ReturnsAsync((Permalink)null);
            
            SetupValidator(shouldInvalidateModelState: false);

            var result = await _controller.GetPermalink("bladiebla");

            Assert.IsType<NotFoundResult>(result);
        }
        
        private void SetupValidator(bool shouldInvalidateModelState)
        {
            void ValidationCallback(
                ActionContext actionContext, 
                ValidationStateDictionary validationState, 
                string prefix, 
                object data)
            {
                // Add a model error when the model state is expected to be invalid.
                // This will automatically propagate back to the modelstate property in the controller.
                if (shouldInvalidateModelState)
                {
                    actionContext.ModelState.AddModelError("", "Oops");
                }
            }

            _validator
                .Setup(mock => mock.Validate(It.IsAny<ActionContext>(),
                    It.IsAny<ValidationStateDictionary>(),
                    It.IsAny<string>(), It.IsAny<object>()))
                .Callback((Action<ActionContext, ValidationStateDictionary, string, object>) ValidationCallback);
        }
    }
}