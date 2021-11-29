using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopBridge.Controllers.Resources;
using ShopBridge.Core.Models;
using ShopBridge.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net.Http;

namespace ShopBridge.Controllers
{
    [Route("/api/item")]
    public class ItemController : Controller
    {
        private readonly IMapper mapper;
        private readonly IItemRepository repository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserDetails userDetails;

        public ItemController(IMapper mapper, IItemRepository repository, IUserRepository userRepository,IUnitOfWork unitOfWork, IUserDetails userDetails)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.userDetails = userDetails;
        }
        
        //public ItemController()
        //{

        //}
        /// <summary>
        /// Gets all the items from the inventory
        /// </summary>
        /// <returns>list of inventory items</returns>
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var items = await repository.GetItems();
                
                return Ok(JsonConvert.SerializeObject(mapper.Map<List<InventoryItem>, List<ItemResource>>(items)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500,"Some error occured, check the console");
            }
        }

        /// <summary>
        /// Adds an item to the inventory
        /// </summary>
        /// <param name="itemResource">item object with its Name, Description, Price</param>
        /// <returns>id of the item added</returns>
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] ItemResource itemResource)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (itemResource.Price <= 0)
                {
                    return BadRequest("Please provide positive price for the item");
                }

                var currentUser = Convert.ToInt32(this.userDetails.UserId);

                if (await userRepository.CheckIsProductAdmin(currentUser))
                {
                    var item = mapper.Map<ItemResource, InventoryItem>(itemResource);

                    item.LastUpdatedDate = DateTime.Now;
                    item.IsActive = true;
                    item.UpdatedByUserId = currentUser;

                    repository.AddItem(item);

                    await unitOfWork.CompleteAsync();

                    return StatusCode(201, item.Id.ToString());
                }
                else
                    return Unauthorized("The user is not authorized to add items");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Some error occured, check the console");
            }
        }

        /// <summary>
        /// Edit/update an item of the inventory
        /// </summary>
        /// <param name="id">id of the item to be edited/updated</param>
        /// <param name="itemResource">item object with its Name, Description, Price</param>
        /// <returns>id of the item</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditItem(int id, [FromBody] ItemResource itemResource)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (itemResource.Price <= 0)
                {
                    return BadRequest("Please provide positive price for the item");
                }

                var currentUser = Convert.ToInt32(this.userDetails.UserId);
                if (await userRepository.CheckIsProductAdmin(currentUser))
                {
                    var item = await repository.GetItem(id);
                    if (item != null)
                    {
                        mapper.Map<ItemResource, InventoryItem>(itemResource, item);

                        //The above statement can also be written as below:
                        //var item = mapper.Map<InventoryItem>(inventoryItemResource);
                        item.LastUpdatedDate = DateTime.Now;
                        item.UpdatedByUserId = currentUser;
                        await unitOfWork.CompleteAsync();

                        return Ok(item.Id);
                    }
                    else
                        return NotFound("The item id is invalid");
                }
                else
                    return Unauthorized("The user is not authorized to edit items");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Some error occured, check the console");
            }
        }

        /// <summary>
        /// Delete an item from the inventory
        /// </summary>
        /// <param name="id">id of the item to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var currentUser = Convert.ToInt32(this.userDetails.UserId);
                if (await userRepository.CheckIsProductAdmin(currentUser).ConfigureAwait(false))
                {
                    var item = await repository.GetItem(id);
                    if (item != null)
                    {
                        item.LastUpdatedDate = DateTime.Now;
                        item.IsActive = false;
                        item.UpdatedByUserId = currentUser;

                        await unitOfWork.CompleteAsync();

                        return NoContent();
                    }
                    else
                        return NotFound("The item id is invalid");
                }
                else
                    return Unauthorized("The user is not authorized to add items");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Some error occured, check the console");
            }
        }

        //[HttpGet("{id}")]
        //public async Task<bool> CheckIsProductAdmin(int userId)
        //{
        //    var user = await context.Users.Include(z => z.UserType)
        //                            .Where(x => x.Id == userId && x.UserType.Type == "ProductAdmin")
        //                            .Select(x => x.Id).FirstOrDefaultAsync();
        //    //var admins = await context.UserTypes.Where(x => x.Type == "ProductAdmin").Select(y=>y.Id).ToListAsync();

        //    return user > 0;

        //}
    }
}
