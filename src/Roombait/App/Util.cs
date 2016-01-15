namespace Roombait.App
{
    public static class Util
    {
        public static string GetSlug(Models.Residence residence)
        {
            return residence.Name.ToLower().Replace(' ', '-');
        }
    }
}
