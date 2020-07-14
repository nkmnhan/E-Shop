using EShop.BackEnd.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace EShop.BackEnd.Data.SeedData
{
    [ExcludeFromCodeCoverage]
    public class SeedProductData
    {
        public static void CreateData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (!context.Database.EnsureCreated())
            {
                return;
            }

            var categories = new List<Category>()
            {
                new Category
                {
                    Name = "Mobile"
                },
                new Category
                {
                    Name = "Laptop"
                },
                new Category
                {
                    Name = "TV"
                }
            };
            context.AddRange(categories);

            var brands = new List<Brand>
            {
                new Brand
                {
                    Name = "Apple"
                },
                new Brand
                {
                    Name = "Samsung"
                },
                new Brand
                {
                    Name = "Nokia"
                },
                new Brand
                {
                    Name = "Xiaomi"
                },
                new Brand
                {
                    Name = "Acer"
                },
                new Brand
                {
                    Name = "Msi"
                },
                new Brand
                {
                    Name = "Dell"
                },
                new Brand
                {
                    Name = "Microsoft"
                },
                new Brand
                {
                    Name = "LG"
                },
                new Brand
                {
                    Name = "HKC"
                },
                new Brand
                {
                    Name = "Fujisu"
                },
                new Brand
                {
                    Name = "ViewSonic"
                }
            };
            context.AddRange(brands);

            if (!context.Products.Any())
            {
                var mobiles = new List<Product>{new Product
                    {
                        Name = "Galaxy Note 8",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/5/samsung-galaxy-note-8-8367506699-jpg.jpg",
                        Description = "Samsung Galaxy Note 8 N950",
                        Price = 978,
                        Brand = brands[0]
                    },
                    new Product
                    {
                        Name = "Galaxy Note FE",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/7/samsung-galaxy-note-7-n930-copy-3647061768-jpg.jpg",
                        Description = "Samsung Galaxy Note FE",
                        Price = 497,
                        Brand = brands[1],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[0]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Galaxy S8 Plus",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/1/samsung-galaxy-s8-plus-9521754012-jpg.jpg",
                        Description = "Samsung Galaxy S8 Plus",
                        Price = 630,
                        Brand = brands[1],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[0]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "iPhone X 256Gb",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/1/iphone-x-256gb-8940561428-jpg.jpg",
                        Description = "Apple iPhone X 256Gb",
                        Price = 869,
                        Brand = brands[0],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[0]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "iPhone 8 256Gb",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/2/apple-iphone-8-256gbcopy-2943714186-jpg.jpg",
                        Description = "Apple iPhone 8 256Gb Product Red Special Edition",
                        Price = 834,
                        Brand = brands[0],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[0]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "iPhone 7 Plus 128Gb",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/5/apple-iphone-7-plus-128gb-gold-1657334868-jpg.jpg",
                        Description = "Apple iPhone 7 Plus 128Gb",
                        Price = 782,
                        Brand = brands[0],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[0]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "iPad Pro 9.7 Cellular 32Gb",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/1/apple-ipad-pro-9-7-cellular-32gb-goldcopy-8705219985-jpg.jpg",
                        Description = "Apple iPad Pro 9.7 Cellular 32Gb",
                        Price = 486,
                        Brand = brands[0],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[0]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "iPad Pro 9.7 Cellular 32Gb",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/1/apple-ipad-pro-9-7-cellular-32gb-goldcopy-8705219985-jpg.jpg",
                        Description = "Apple iPad Pro 9.7 Cellular 32Gb",
                        Price = 486,
                        Brand = brands[0],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[0]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Nokia 6 2018",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/6/nokia-6-2018-4526522779-jpg.jpg",
                        Description = "Nokia 6 2018",
                        Price = 208,
                        Brand = brands[2],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[0]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Mi Mix 2 64GB Ram 6GB",
                        ImageUrl = @"https://stcv4.hnammobile.com//uploads/products/colors/1/xiaomi-mi-max-2copy-8807873954-jpg.jpg",
                        Description = "Xiaomi Mi Mix 2 64GB Ram 6GB",
                        Price = 379,
                        Brand = brands[3],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[0]
                            }
                        }
                    }
                };

                var laptops = new List<Product>
                {
                    new Product
                    {
                        Name = "Acer Aspire A315",
                        ImageUrl = @"http://laptopno1.com/Upload/Img/Products/Acer%20A315%2015%20%C4%90en7.png",
                        Description = "Acer Aspire A315 51 37HG (GNPSV.035) Intel® Core™ i3",
                        Price = 408,
                        Brand = brands[4],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Dell Inspiron 3169",
                        ImageUrl = @"http://laptopno1.com/Upload/Img/Products/DELL%203169T%20RED1.jpg",
                        Description = "Dell Inspiron 3169 (70082005) Intel® Core™ i3",
                        Price = 502,
                        Brand = brands[6],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "MSI GL63 8RC 266VN",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2018/05/GL63-.jpg",
                        Description = "MSI GL63 8RC 266VN Intel® Core™ i5",
                        Price = 918,
                        Brand = brands[5],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Apple Macbook 12",
                        ImageUrl = @"http://laptopno1.com/Upload/Img/Products/Macbook%2012%20Gold0.jpeg",
                        Description = "Apple Macbook 12 (MNYK2SA/A) Intel core M3 _8GB _256GB _VGA ITNEL _28717F/FU",
                        Price = 1504,
                        Brand = brands[0],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Apple Macbook Pro 13",
                        ImageUrl = @"https://product.hstatic.net/1000169499/product/macbook_mf839_2_0e5c07befae9427791065873d5a6d723.jpg",
                        Description = "Apple Macbook Pro 13 (MPXR2SA/A) Intel Core i5 _8GB _128GB _VGA INTEL _28717F/FU",
                        Price = 834,
                        Brand = brands[0],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Microsoft Surface Pro 4",
                        ImageUrl = @"http://laptopno1.com/Upload/Img/Products/b582f7f9-93ee-4ce8-971f-aacf5426decb.jpg",
                        Description = "Microsoft Surface Pro 4 - Core i5-6200U_8GB_256GB SSD_12.3″ Full HD_Windows 10",
                        Price = 1304,
                        Brand = brands[7],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Acer Gaming Nitro 5",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2018/05/nitro_5_carbon_1.jpg",
                        Description = "Acer Gaming Nitro 5 AN515 51 5775 (NH.Q2SSV.004) Intel® Kaby Lake Core™ i5",
                        Price = 782,
                        Brand = brands[4],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "LG 14 Z970-G I5-7200U",
                        ImageUrl = @"http://laptopno1.com/Upload/Img/Products/z9702.jpg",
                        Description = "LG 14 Z970-G I5-7200U",
                        Price = 1086,
                        Brand = brands[8],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Acer Swift 5 SF514",
                        ImageUrl = @"http://laptopno1.com/Upload/Img/Products/Acer%20Swift%205%2014%20Xanh.png",
                        Description = "Acer Swift 5 SF514 52T 50G2 (GTMSV.001) Intel® Core™ i5",
                        Price = 208,
                        Brand = brands[4],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Fujitsu LifeBook U938",
                        ImageUrl = @"http://laptopno1.com/Upload/Img/Products/Fujitsu%20LifeBook%20U939.jpg",
                        Description = "Fujitsu LifeBook U938 (L00U938VN00000017) Intel® Core™ i7 _8550U",
                        Price = 1826,
                        Brand = brands[10],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[1]
                            }
                        }
                    }
                };

                var tvSets = new List<Product>
                {
                    new Product
                    {
                        Name = "Samsung LS22F350FHEXXV",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2017/11/LS22F350FHEXXV-1.jpg",
                        Description = "Samsung LS22F350FHEXXV 21.5″",
                        Price = 113,
                        Brand = brands[1],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Samsung C24F390FHE",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2017/07/Samsung-C24F390FHE-01.jpg",
                        Description = "SAMSUNG C24F390FHE 24” FREESYNC",
                        Price = 152,
                        Brand = brands[1],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "VIEWSONIC VA2719-SH",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2017/06/viewsonic-vx2476-1_compressed-1.jpg",
                        Description = "VIEWSONIC VA2719-SH 27” IPS",
                        Price = 199,
                        Brand = brands[11],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Dell 24” IPS",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2017/07/U2414H-sp1.jpg",
                        Description = "Dell 24” IPS UltraSharp Monitor U2414H",
                        Price = 189,
                        Brand = brands[7],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "LCD Dell U2417H",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2017/07/2417_1_compressed.jpg",
                        Description = "LCD Dell U2417H 24” IPS",
                        Price = 226,
                        Brand = brands[7],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Samsung LC27F397FHEXXV",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2018/03/lc27f397fhexxv_1.jpg",
                        Description = "Samsung LC27F397FHEXXV 27″",
                        Price = 230,
                        Brand = brands[1],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "SAMSUNG LC32F391FWEXXV 32″",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2018/01/vn-curved-cf391-lc32f391fwexxv-004-l-perspective-white-568x568.jpg",
                        Description = "SAMSUNG LC32F391FWEXXV 32″",
                        Price = 294,
                        Brand = brands[11],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "Acer LCD XF240H",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2017/12/XF240H_sku_main.png",
                        Description = "Acer LCD XF240H 24” FHD 1ms 144Hz",
                        Price = 302,
                        Brand = brands[5],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "HKC M27G1F 27″ VA 144Hz FHD",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2018/06/M27G1F_1_compressed.jpg",
                        Description = "HKC M27G1F 27″ VA 144Hz FHD",
                        Price = 339,
                        Brand = brands[10],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    },
                    new Product
                    {
                        Name = "HKC M32A7F",
                        ImageUrl = @"https://xgear.vn/wp-content/uploads/2018/06/M32A7F_1_compressed.jpg",
                        Description = "HKC M32A7F 32″ VA 165Hz FHD",
                        Price = 343,
                        Brand = brands[10],
                        ProductCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Category = categories[2]
                            }
                        }
                    }
                };

                context.Products.AddRange(mobiles);
                context.Products.AddRange(laptops);
                context.Products.AddRange(tvSets);
                context.SaveChanges();
            }
        }
    }
}
