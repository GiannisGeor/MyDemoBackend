using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Models.Projections
{
    public class AllProductDataProjection
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
        public List<ProductOptionsGroupProjection> ProductOptionsGroups { get; set; } = new List<ProductOptionsGroupProjection> { };
    }

    public class ProductOptionsGroupProjection
    {
        public int Id { get; set; }

        /// <summary>
        /// 1 to many connection from OptionsGroup to ProductOptionsGroup table
        /// </summary>
        public OptionsGroupProjection OptionsGroup { get; set; }

        /// <summary>
        /// 1 to many connection from Options to ProductOptionsGroup table
        /// </summary>
        public List<OptionsProjection> Options { get; set; } = new List<OptionsProjection> { };
    }

    public class OptionsGroupProjection
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

    public class OptionsProjection
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
        public BaseOptionsProjection BaseOptions { get; set; }
    }

    public class BaseOptionsProjection
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
