﻿using AutoMapper;
using YourStoreApi.Models;
using YourStoreApi.Models.Dto;

namespace YourStoreApi.Services.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember,
        ResolutionContext context)
        {
            // if (!string.IsNullOrEmpty(source.ProductImages))
            // {
            //     return _config["ApiUrl"] + source.ProductImages;
            // }
            return null;
        }
    }
}
