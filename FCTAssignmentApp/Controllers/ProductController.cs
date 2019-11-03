using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FCTAssignmentApp.Helpers;
using FCTAssignmentApp.Models;
using FCTAssignmentApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FCTAssignmentApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductService productService,
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            ILogger<ProductController> logger)
        {
            _productService = productService;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("purchases")]
        public IActionResult GetAllCustomerPurchases()
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                if (userId != null)
                {
                    var purchases = _productService.GetAllPurchases().Where(p => p.User.Id == Convert.ToInt32(userId) 
                    && p.OrderStatus==Status.Active);
                    return Ok(purchases);
                }
                return Unauthorized();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in GetAllCustomerPurchases");
                return BadRequest(e);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetProduct(id);
            return Ok(product);
        }

        [HttpPost("purchase")]
        public IActionResult PurchaseProduct(Product product)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                if (userId != null)
                {
                    var currentCustomer = _userService.GetById(Convert.ToInt32(userId));
                    var currentProduct = _productService.GetProduct(product.Id);
                    Purchase purchase = new Purchase()
                    {
                        User = currentCustomer,
                        Product = currentProduct,
                        OrderStatus = Status.Active
                    };

                    var item = _productService.AddPurchase(purchase);
                    return Ok(item);
                }
                return Unauthorized();

            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error in PurchaseProduct");
                return BadRequest(e);
            }

        }
    }
}