﻿using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.HelperModels
{
    class WordParagraph
    {
        public List<(string, WordTextProperties)> Texts { get; set; }
        public WordTextProperties TextProperties { get; set; }
    }
}
