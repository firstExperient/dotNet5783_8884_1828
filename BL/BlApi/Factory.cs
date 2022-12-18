using BlImplementation;
namespace BlApi;

public static class Factory
{
    public static IBl Get() {
        BlImplementation.Bl bl = new Bl();
        return bl;
    }
}
