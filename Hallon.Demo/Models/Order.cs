﻿using System;

namespace Hallon.Demo.Models
{
    public class Order : Resource
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
    }
}