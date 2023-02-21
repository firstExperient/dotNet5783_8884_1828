﻿using BlApi;
namespace BlImplementation;

internal sealed class Bl : IBl
{
    public IProduct Product { get; } = new Product();
    public ICart Cart { get; } = new Cart();
    public IOrder Order { get; } = new Order();
}
