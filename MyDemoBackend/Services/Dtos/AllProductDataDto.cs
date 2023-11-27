using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Services.Dtos
{
    public class AllProductDataDto
    {
        public int Id { get; set; }

        /// <summary>
        /// The names of the products
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the prices of the products
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The descriptions of the products
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Many to many connection from Product to ProductOptionsGroup table
        /// </summary>
        public List<ProductOptionsGroupDto> ProductOptionsGroups { get; set; } = new List<ProductOptionsGroupDto> { };
    }

    public class ProductOptionsGroupDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 1 to many connection from OptionsGroup to ProductOptionsGroup table
        /// </summary>
        public OptionsGroupDto OptionsGroup { get; set; }

        /// <summary>
        /// 1 to many connection from Options to ProductOptionsGroup table
        /// </summary>
        public List<OptionsDto> Options { get; set; } = new List<OptionsDto> { };
    }

    public class OptionsGroupDto
    {
        /// <summary>
        /// The Id of the OptionsGroup
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The names of the Options 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A bool to that indicates if an OptionGroup can be multiselected or not
        /// </summary>
        public bool IsMulti { get; set; }
    }

    public class OptionsDto
    {
        /// <summary>
        /// The Id of the Options
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Extra Cost of each option
        /// </summary>
        public decimal ExtraCost { get; set; }

        /// <summary>
        /// 1 to many connection to BaseOptions table
        /// </summary>
        public BaseOptionsDto BaseOptions { get; set; } = new BaseOptionsDto();
    }

    public class BaseOptionsDto
    {
        /// <summary>
        /// The Id of the BaseOptions
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The names of the BaseOptions 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A bool to that indicates if a BaseOption is available or not
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
