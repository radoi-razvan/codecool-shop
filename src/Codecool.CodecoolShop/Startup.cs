using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codecool.CodecoolShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance();
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoMemory.GetInstance();
            ISupplierDao supplierDataStore = SupplierDaoMemory.GetInstance();

            // Generate supplieres
            Supplier amazon = new Supplier{Name = "Amazon", Description = "Digital content and services"};
            supplierDataStore.Add(amazon);
            Supplier lenovo = new Supplier{Name = "Lenovo", Description = "Computers"};
            supplierDataStore.Add(lenovo);
            Supplier asus= new Supplier { Name = "Asus", Description = "Computers" };
            supplierDataStore.Add(asus);
            Supplier alienware = new Supplier { Name = "Alienware", Description = "Computers" };
            supplierDataStore.Add(alienware);
            Supplier apple = new Supplier { Name = "Apple", Description = "Devices" };
            supplierDataStore.Add(apple);
            Supplier samsung = new Supplier { Name = "Samsung", Description = "Devices" };
            supplierDataStore.Add(samsung);
            Supplier sony = new Supplier { Name = "Sony", Description = "Devices" };
            supplierDataStore.Add(sony);
            Supplier miele = new Supplier { Name = "Miele", Description = "Refrigerators" };
            supplierDataStore.Add(miele);
            Supplier liebherr = new Supplier { Name = "Liebherr", Description = "Refrigerators" };
            supplierDataStore.Add(liebherr);
            Supplier bosch = new Supplier { Name = "Bosch", Description = "Different types of devices" };
            supplierDataStore.Add(bosch);
            Supplier primer = new Supplier { Name = "Primer", Description = "Washing machines" };
            supplierDataStore.Add(primer);
            Supplier moratti = new Supplier { Name = "Moratti", Description = "Stoves" };
            supplierDataStore.Add(moratti);

            // Generate product category & products
            ProductCategory tablet = new ProductCategory {Name = "Tablet", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDataStore.Add(tablet);
            productDataStore.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "USD", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support", ProductCategory = tablet, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "USD", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand", ProductCategory = tablet, Supplier = lenovo });
            productDataStore.Add(new Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "USD", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption", ProductCategory = tablet, Supplier = amazon });

            ProductCategory laptop = new ProductCategory { Name = "Laptop", Department = "Hardware", Description = "A small portable personal computer with a screen and alphanumeric keyboard." };
            productCategoryDataStore.Add(laptop);
            productDataStore.Add(new Product { Name = "Laptop Gaming ASUS TUF A17 FA706IH", DefaultPrice = 989.9m, Currency = "USD", Description = "Ready for very serious gaming and extreme durability, a high-performance gaming laptop", ProductCategory = laptop, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Laptop Lenovo IdeaPad Gaming 3", DefaultPrice = 575.0m, Currency = "USD", Description = "Smoothly play your favorite games with NVIDIA GeForce GTX graphics", ProductCategory = laptop, Supplier = lenovo });
            productDataStore.Add(new Product { Name = "Laptop ASUS ProArt StudioBook One W590G6T", DefaultPrice = 8000.0m, Currency = "USD", Description = "The first laptop equipped with the NVIDIA Quadro RTX 6000 graphics solution, one of the most powerful StudioBook model ever created", ProductCategory = laptop, Supplier = asus });
            productDataStore.Add(new Product { Name = "Laptop Apple MacBook Pro 15", DefaultPrice = 5999.9m, Currency = "USD", Description = "Stronger. More efficient. Better experience", ProductCategory = laptop, Supplier = apple });
            productDataStore.Add(new Product { Name = "Laptop Gaming Alienware Area 51M R2", DefaultPrice = 5898.0m, Currency = "USD", Description = "With extraordinary desktop processing power", ProductCategory = laptop, Supplier = alienware });

            ProductCategory phone = new ProductCategory { Name = "Phone", Department = "Hardware", Description = "A telecommunications device that permits two or more users to conduct a conversation when they are too far apart to be heard directly." };
            productCategoryDataStore.Add(phone);
            productDataStore.Add(new Product { Name = "Iphone 12 PRO mobile phone", DefaultPrice = 3050.9m, Currency = "USD", Description = "Customized by platinum plating, made by Vip Touch Design Dubai, a manufacture specialized in producing exclusive gadgets", ProductCategory = phone, Supplier = apple });
            productDataStore.Add(new Product { Name = "Samsung Galaxy S9 mobile phone", DefaultPrice = 2680.0m, Currency = "USD", Description = "Discover a new world with the Samsung Galaxy S9", ProductCategory = phone, Supplier = samsung });
            productDataStore.Add(new Product { Name = "Sony Xperia 1 III mobile phone", DefaultPrice = 1180.0m, Currency = "USD", Description = "Xperia 1 III is a sign of effort in the Sony brand team", ProductCategory = phone, Supplier = sony });

            ProductCategory tv = new ProductCategory { Name = "Tv", Department = "Electonics", Description = "A telecommunication medium used for transmitting moving images in monochrome (black and white), or in color, and in two or three dimensions and sound." };
            productCategoryDataStore.Add(tv);
            productDataStore.Add(new Product { Name = "Samsung 98Q950RB TV", DefaultPrice = 6239.9m, Currency = "USD", Description = "Samsung QLED 8K Q950R will take you into the depths of reality to a higher level", ProductCategory = tv, Supplier = samsung });
            productDataStore.Add(new Product { Name = "Sony Bravia FW-100BZ40J Professional TV", DefaultPrice = 1519.8m, Currency = "USD", Description = "100 inch, 4K Ultra HD, LED", ProductCategory = tv, Supplier = sony });
            productDataStore.Add(new Product { Name = "Sony 85Z9J TV", DefaultPrice = 999.8m, Currency = "USD", Description = "The BRAVIA XR TV takes the picture and sound to the next level with the ingenious Cognitive Processor XR", ProductCategory = tv, Supplier = sony });

            ProductCategory refrigerator = new ProductCategory { Name = "Refrigerator", Department = "Appliances", Description = "A commercial and home appliance consisting of a thermally insulated compartment and a heat pump (mechanical, electronic or chemical) that transfers heat from its inside to its external environment so that its inside is cooled to a temperature below the room temperature." };
            productCategoryDataStore.Add(refrigerator);
            productDataStore.Add(new Product { Name = "Refrigerator with one door MIELE KS 28463 D BB", DefaultPrice = 3236.8m, Currency = "USD", Description = "PerfectFresh Pro, FlexiLight, DynaCool, ComfortClean", ProductCategory = refrigerator, Supplier = miele });
            productDataStore.Add(new Product { Name = "Refrigerator LIEBHERR Kef 4330 Comfort", DefaultPrice = 2760.0m, Currency = "USD", Description = "More volume inside for food, spacious and energy efficient during operation", ProductCategory = refrigerator, Supplier = liebherr });
            productDataStore.Add(new Product { Name = "Refrigerator with a door Bosch KSV36AI3P", DefaultPrice = 1428.0m, Currency = "USD", Description = "LED lighting: keep the contents of your refrigerator in the right light", ProductCategory = refrigerator, Supplier = bosch });

            ProductCategory washingMachine = new ProductCategory { Name = "Washing Machine", Department = "Appliances", Description = "A home appliance used to wash laundry." };
            productCategoryDataStore.Add(washingMachine);
            productDataStore.Add(new Product { Name = "Professional washing machine Primer LP-10T2", DefaultPrice = 3659.2m, Currency = "USD", Description = "Possibility to program, export and import unlimited programs through the USB port", ProductCategory = washingMachine, Supplier = primer });
            productDataStore.Add(new Product { Name = "Front washing machine MIELE WEI 875 WPS", DefaultPrice = 1598.0m, Currency = "USD", Description = "QuickPowerWash washing programs, Automatic Plus, Cotton, Minimal ironing", ProductCategory = washingMachine, Supplier = miele });
            productDataStore.Add(new Product { Name = "Miele WEG675 WPS washing machine", DefaultPrice = 1269.8m, Currency = "USD", Description = "Delay start, Timer, Automatic load recognition, Self-dosing", ProductCategory = washingMachine, Supplier = miele });

            ProductCategory stove = new ProductCategory { Name = "Stove", Department = "Appliances", Description = "A device that burns fuel or uses electricity to generate heat inside or on top of the apparatus." };
            productCategoryDataStore.Add(stove);
            productDataStore.Add(new Product { Name = "Moratti professional stove, 6 mesh 1200x900x850 mm", DefaultPrice = 3630.4m, Currency = "USD", Description = "Gas supply, Power 39 kW, Weight 205 kg", ProductCategory = stove, Supplier = moratti });
            productDataStore.Add(new Product { Name = "4-burner cooking stove, gas, 900x900x850 mm Moratti", DefaultPrice = 828.4m, Currency = "USD", Description = "Stainless steel body with cast iron grills, Safety thermocouple", ProductCategory = stove, Supplier = moratti });
            productDataStore.Add(new Product { Name = "Bosch HKS59D250 cooker", DefaultPrice = 579.8m, Currency = "USD", Description = "With AutoPilot automatic programs you automatically get the best results", ProductCategory = stove, Supplier = bosch });
        }
    }
}
