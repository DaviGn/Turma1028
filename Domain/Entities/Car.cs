﻿namespace Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
}
