﻿using BlImplementation;
namespace BlApi

{
    public class Factory
    {
        public static IBl Get() {
            BlImplementation.Bl bl = new Bl();
            return bl;
        }
    }
}
