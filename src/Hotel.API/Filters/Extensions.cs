namespace Hotel.API.Filters;

public static class Extensions
{
    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add<SampleActionFilter>();
        });
        return services;
    }
}
