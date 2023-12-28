using DjStore.Data;
using DjStore.DTOs;
using DjStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DjStore.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController : ControllerBase
    {
        private readonly DjStoreContext _context;

        public BasketController(DjStoreContext context)
        {
            _context = context;
        }
        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDTO>> GetBasket()
        {
            var Basket = await RetrieveBasket();
            if (Basket == null) return NotFound();
            return MapBasketToDTO(Basket);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> AddItemToBasket(int productId, int quantity)
        {
            //get basket
            var basket = await RetrieveBasket();

            //create basket if not exist
            if (basket == null) basket = CreateBasket();
            //get product
            var product = await _context.Products.FindAsync(productId);

            if (product == null) return NotFound();

            //add item
            basket.AddItem(product, quantity);
            //save changes
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetBasket", MapBasketToDTO(basket));

            return BadRequest(new ProblemDetails { Title = "Problem saving items to basket" });
        }
        [HttpDelete]
        public async Task<ActionResult<Basket>> RemoveItemFromBasket(int productId, int quantity)
        {
            //get basket
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();

            //reduce quantity or remove item
            basket.RemoveItem(productId, quantity);

            //save changes
            var result = _context.SaveChanges() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem removing items from basket" });
        }

        private async Task<Basket> RetrieveBasket()
        {
            return await _context.Baskets
                        .Include(i => i.BasketItems)
                        .ThenInclude(p => p.Product)
                        .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
        }

        private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;
        }

        private BasketDTO MapBasketToDTO(Basket Basket)
        {
            return new BasketDTO
            {
                Id = Basket.Id,
                BuyerId = Basket.BuyerId,
                BasketItems = Basket.BasketItems.Select(item => new BasketItemsDTO
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Quantity = item.Quantity,
                    Brand = item.Product.Brand
                }).ToList()
            };
        }
    }
}