# EfCore Multiple Providers

Does is make sense to register `DbContextOptions<TContextService>` in `AddDbContext<TContextService, TContextImplementation>()` when the types differ? ([source](https://github.com/dotnet/efcore/blob/c69926a8cdd05f4427e89ac8bac734e490b3d23a/src/EFCore/Extensions/EntityFrameworkServiceCollectionExtensions.cs#L495))

One approach to supporting multiple database providers with EFCore is to have multiple DbContext's inherit from a base DbContext.

When using `.AddDbContext<TContextService, TContextImplemenation>()` only `DbContextOptions<TContextImplementation>` gets added to DI, so injecting the  TContextService anywhere fails because it can't find ProjectDbContextOptions.

My minimal example shows that is works when manually registering `DbContextOptions<TContextService>`; can this be default behavior?
