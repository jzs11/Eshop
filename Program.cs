using System.Text.RegularExpressions;

namespace Eshop
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         // Add services to the container.
         builder.Services.AddRazorPages();
         builder.Services.AddMvc();
         builder.Services.AddRouting(options =>
         {
            options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            options.LowercaseUrls = true;
         });

         var app = builder.Build();

         // Configure the HTTP request pipeline.
         if (!app.Environment.IsDevelopment())
         {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseHttpsRedirection();
         app.UseStaticFiles();

         app.UseRouting();

         app.UseAuthorization();
         app.MapRazorPages();

         app.MapControllerRoute(
             name: "area-sl",
             pattern: "{area:slugify}/{controller:slugify}/{action=Index}/{id?}");

         app.MapControllerRoute(
             name: "default-ls",
             pattern: "{controller:slugify}/{action=Index}/{id?}");

         app.MapControllerRoute(
             name: "area",
             pattern: "{area}/{controller}/{action=Index}/{id?}");

         app.MapControllerRoute(
             name: "default",
             pattern: "{controller}/{action=Index}/{id?}");

         app.Run();
      }
   }

   public class SlugifyParameterTransformer : IOutboundParameterTransformer
   {
      public string? TransformOutbound(object? value)
      {
         if (value == null) { return null; }

         return Regex.Replace(value.ToString()!,
                              "([a-z])([A-Z])",
                              "$1-$2",
                              RegexOptions.CultureInvariant,
                              TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
      }
   }
}