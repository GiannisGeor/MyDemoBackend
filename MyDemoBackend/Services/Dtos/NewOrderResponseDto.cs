using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enums;

namespace Services.Dtos
{
    public class NewOrderResponseDto
    {
        public int Id { get; set; }

        /// <summary>
        /// The Id of the Store table
        /// </summary>
        public int StoreId { get; set; }

        public int AddressId { get; set; }

        /// <summary>
        /// The comments of each order
        /// </summary>
        public string OrderComments { get; set; }        

        /// <summary>
        /// 1 to many connection to OrderLines table
        /// </summary>
        public List<NewOrderLinesResponseDto> OrderLines { get; set; } = new List<NewOrderLinesResponseDto>();

    }

    public class NewOrderLinesResponseDto
    {
        /// <summary>
        /// The Id of the Product table
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The quantity of the OrderLines
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The comments of each product
        /// </summary>
        public string Comments { get; set; }

        public List<NewOrderLinesOptionsResponseDto> OrderLinesOptions { get; set; } = new List<NewOrderLinesOptionsResponseDto>();

    }

    public class NewOrderLinesOptionsResponseDto
    {
        /// <summary>
        /// The Id of the Options table
        /// </summary>
        public int OptionsId { get; set; }
    }
}

