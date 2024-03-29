﻿namespace Selu383.SP24.Api
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class HotelCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class HotelUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}