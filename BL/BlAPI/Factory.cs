

using BlApi;

namespace BlAPI;

public static class Factory
{
    public static IBl Get() => new BlImplementation.Bl(); 
}
