using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techYard.Repository.Interfaces;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.CategoryServices;
using techYard.Service.Services.featuresServices.Dtos;
using techYard.Service.Services.featuresServices;
using Microsoft.AspNetCore.Authorization;

namespace techYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFeaturesController : ControllerBase
    {
        readonly IProductFeatureServices _featureServices;

        public ProductFeaturesController(IProductFeatureServices featureServices)
        {
            _featureServices = featureServices;
        }

        [HttpGet]
        [Route("GetAllProductsFeatures")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetFeatureDto>>> GetAllProductsFeatures()
        {
            var features = await _featureServices.GetAllProductsFeatures();
            return Ok(features);
        }

        [HttpGet]
        [Route("GetProductFeaturesById/{id}")]
        [Authorize]
        public async Task<ActionResult> GetProductFeaturesById(int id)
        {
            var feature = await _featureServices.GetProductFeaturesById(id);
            if (feature == null)
            {
                return NotFound("invalid Id");
            }
            return Ok(feature);
        }


        [HttpDelete("DeleteProductFeatures/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProductFeatures(int id)
        {
            var feature = await _featureServices.DeleteProductFeatureById(id);
            if (feature == null)
            {
                return NotFound("Invalid Id");
            }
            return Ok();
        }




        [HttpPut]
        [Route("UpdateProductFeatures")]
        [Authorize]
        public async Task<IActionResult> UpdateProductFeatures(int id, AddFeatureDto featureDto)
        {
            if (featureDto == null)
            {
                return BadRequest("Product's Features are Empty");
            }

            var feature = await _featureServices.UpdateProductFeatures(id, featureDto);
            if (feature == null)
            {
                return NotFound("Invalid Id");
            }
            return Ok();
        }


        [HttpPost]
        [Route("AddProductFeatures")]
        [Authorize]
        public async Task<ActionResult<AddFeatureDto>> AddProductFeatures(AddFeatureDto featureDto)
        {  
            if(featureDto == null)
            {
                return BadRequest("Product's features are empty");
            }
            await _featureServices.AddProductFeature(featureDto);
            return Ok(featureDto);
        }
    }
}
