﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Willowcat.Budget.Models
{
    public class BudgetLine
    {
        public DateTime Date { get; set; }
        
        public Dictionary<string, decimal> CategoryAmount { get; set; }

        public string Description { get; set; }

        public Guid Guid { get; private set; }

        public decimal TotalAmount
        {
            get
            {
                return CategoryAmount.Values.Sum();
            }
        }

        public bool ReadOnly { get; set; }

        public BudgetLine()
        {
            Guid = Guid.NewGuid();
        }
    }
}
