using AutoMapper;
using ShopBridge.Controllers.Resources;
using ShopBridge.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to Business
            CreateMap<InventoryItem, ItemResource>();
                //.ForMember(ir => ir.Name, opt => opt.MapFrom(i => i.Name))
                //.ForMember(ir => ir.Description, opt => opt.MapFrom(i => i.Description))
                //.ForMember(ir => ir.Price, opt => opt.MapFrom(i => i.Price))
                //.ForMember(ir => ir.LastUpdatedDate, opt => opt.MapFrom(i => i.LastUpdatedDate));

            //Business to Domain
            CreateMap<ItemResource, InventoryItem>()
                .ForMember(i=>i.Id, opt=>opt.Ignore());
        }
    }
}
