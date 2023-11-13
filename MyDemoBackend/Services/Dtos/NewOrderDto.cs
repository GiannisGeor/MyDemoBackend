﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;
using Models.Enums;

namespace Services.Dtos
{
    public class NewOrderDto
    {
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
        public List<NewOrderLinesDto> OrderLines { get; set; } = new List<NewOrderLinesDto>();

    }

    public class NewOrderLinesDto
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
    }
}
