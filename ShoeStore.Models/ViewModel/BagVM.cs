﻿namespace ShoeStore.Models.ViewModel
{
    public class BagVM
    {
        public List<KeyValuePair<Bag, bool>> Bags { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
