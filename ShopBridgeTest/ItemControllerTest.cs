using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.Core.Models;
using System;
using Xunit;
using ShopBridge.Core;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopBridge.Mapping;
using ShopBridge.Controllers.Resources;
using Newtonsoft.Json;

namespace ShopBridgeTest
{
    public class ItemControllerTest
    {
        //ItemController _itemController;
        private readonly Mock<IItemRepository> itemRepo;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly Mock<IUserDetails> userDetails;
        private readonly Mock<IUserRepository> userRepo;

        public ItemControllerTest()
        {
            itemRepo = new Mock<IItemRepository>();
            userDetails = new Mock<IUserDetails>();
            userRepo = new Mock<IUserRepository>();

            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                mapper = mappingConfig.CreateMapper();
                
            }
        }

        private List<InventoryItem> GetSampleItemsList()
        {
            return new List<InventoryItem>()
            {
                 new InventoryItem
                 {
                     Id=1,
                     Name="Google Chromebook",
                     Description="Chromebook by Google",
                     Price=34599
                 },
                 new InventoryItem
                 {
                     Id=2,
                     Name="Apple iPad",
                     Description="iPad by Apple",
                     Price=36599
                 },
                 new InventoryItem
                 {
                     Id=3,
                     Name="Apple Pencil",
                     Description="pencil for tab",
                     Price=3599
                 }
            };
        }

        private async Task<List<InventoryItem>> GetSampleItems()
        {
            return await Task.Run(() => GetSampleItemsList());
        }

        [Fact]
        public void GetItemsTest()
        {
            //_itemController = new ItemController();
            //var items = _itemController.GetItems();
            //Assert.IsType<OkObjectResult>(items.Result);

            var items = GetSampleItems();
            itemRepo.Setup(x => x.GetItems()).Returns(GetSampleItems);
            var controller = new ItemController(mapper,itemRepo.Object,userRepo.Object,unitOfWork,userDetails.Object);

            //act
            var actionResult = controller.GetItems();
            var result = actionResult.Result as OkObjectResult;
            var actual = JsonConvert.DeserializeObject<List<ItemResource>>(result.Value.ToString());

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleItems().Result.Count, actual.Count);
        }

        [Fact]
        public void AddItemTest()
        {
            var itemRes = new ItemResource()
            {
                Name = "Google Home Mini",
                Description = "Smart Speaker by Google",
                Price = -1
            };

            var controller = new ItemController(mapper, itemRepo.Object, userRepo.Object, unitOfWork, userDetails.Object);
            
            var actionResult = controller.AddItem(itemRes);
            var result = actionResult.Result as BadRequestObjectResult;

            Assert.Equal("Please provide positive price for the item", result.Value.ToString());
        }
    }
}
